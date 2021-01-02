using Pin80Server.CommandProcessors;
using Pin80Server.Models.Effects;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class MainForm : Form
    {
        private const int maxLogLength = 1000;

        private bool loggingEnabled = true;
        private bool ignoreDuplicates = false;

        private readonly List<string> recentLogEntries = new List<string>();

        private BlockingCollection<string> commandQueue;
        private DataProcessor dataProcessor;
        private EditItemForm editForm = new EditItemForm();

        public MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            SetupDataGridView();

            loadAvailableTables();

            var itemFilter = Settings.ReadSetting(Constants.SettingItemFilter);
            if (itemFilter == "")
            {
                itemFilter = itemFilterCombo.Items[0].ToString();
            }
            itemFilterCombo.SelectedItem = itemFilter;

            ignoreDuplicates = Settings.ReadBoolSetting(Constants.SettingLogBlockDuplicates);
            ignoreDuplicatesCheckbox.Checked = false; // ignoreDuplicates;

            logListViews.LostFocus += (s, fe) => logListViews.SelectedIndices.Clear();
            controlDataGridView.LostFocus += (s, fe) => controlDataGridView.ClearSelection();

            if (Properties.Settings.Default.F1Size.Width == 0 || Properties.Settings.Default.F1Size.Height == 0)
            {

            }
            else
            {
                this.WindowState = Properties.Settings.Default.F1State;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                this.Location = Properties.Settings.Default.F1Location;
                this.Size = Properties.Settings.Default.F1Size;
            }
        }

        public void addItemsToDataGridView(List<ControlItem> controlItems)
        {
            controlDataGridView.SuspendLayout();

            dataProcessor.controllerData.Clear();

            foreach (ControlItem item in controlItems)
            {
                // Make sure effects and targets still exist for all the controlItems.
                // If they don't, clear it and disable the item

                Effect effect = dataProcessor.GetEffect(item.effectString);
                Models.Target target = dataProcessor.GetTarget(item.targetString);

                if (effect == null)
                {
                    item.effectString = null;
                    item.enabled = false;
                }

                if (target == null)
                {
                    item.targetString = null;
                    item.enabled = false;
                }

                dataProcessor.controllerData.Add(item);
            }
            controlDataGridView.ResumeLayout();
        }

        public void sortRefresh()
        {
            if (controlDataGridView.SortOrder != SortOrder.None && controlDataGridView.SortedColumn != null)
            {
                ListSortDirection dir = ListSortDirection.Ascending;
                if (controlDataGridView.SortOrder == SortOrder.Descending)
                {
                    dir = ListSortDirection.Descending;
                }

                controlDataGridView.Sort(controlDataGridView.SortedColumn, dir);
            }
        }

        public void setQueueRef(ref BlockingCollection<string> cq)
        {
            commandQueue = cq;
        }

        public void setDataProcessor(DataProcessor dp)
        {
            dataProcessor = dp;
            controlDataGridView.DataSource = dp.bSource;

            tableComboBox.SelectedItem = Settings.ReadSetting(Constants.SettingLastTable);

            statusStrip1.Items[1].Text = (dataProcessor.autoAddItems) ? "Auto add items enabled" : "Auto add items disabled";
            autoAddItemsCheckbox.Checked = dataProcessor.autoAddItems;
        }

        public void setRomName(string name)
        {
            tableComboBox.Text = name;
        }

        public void addLogEntry(string entry)
        {
            if (entry == null)
            {
                return;
            }

            if (loggingEnabled)
            {
                if (filterTextBox.Text == "" || entry.StartsWith(filterTextBox.Text))
                {
                    if (!ignoreDuplicates || !recentLogEntries.Contains(entry))
                    {
                        recentLogEntries.Add(entry);
                        logListViews.Items.Insert(0, entry);
                    }

                    if (logListViews.Items.Count > maxLogLength)
                    {
                        logListViews.Items.RemoveAt(maxLogLength);
                    }
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            saveWindowState();

            if (dataProcessor.unsavedChanges)
            {
                if (MessageBox.Show("Do you want to save your changes?", "Unsaved Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    dataProcessor.SaveControllerData();

                    Application.Exit();
                }
            }
        }

        private void SetupDataGridView()
        {
            controlDataGridView.RowHeadersVisible = false;
            controlDataGridView.AutoGenerateColumns = false;
            controlDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            controlDataGridView.MultiSelect = false;
            controlDataGridView.AllowUserToAddRows = false;
        }


        private void saveWindowState()
        {
            Properties.Settings.Default.F1State = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.F1Location = this.Location;
                Properties.Settings.Default.F1Size = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.F1Location = this.RestoreBounds.Location;
                Properties.Settings.Default.F1Size = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.Save();
        }


        private void controlDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell cell = controlDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var controlItem = dataProcessor.controllerData[e.RowIndex];

            if (!controlItem.enabled)
            {
                e.CellStyle.BackColor = SystemColors.Control;
            }
            else
            {
                e.CellStyle.BackColor = SystemColors.Window;
            }

            // Disable if it fails validation
            if (e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cell;

                if (!controlItem.validate())
                {
                    checkCell.ReadOnly = true;
                }
                else
                {
                    checkCell.ReadOnly = false;
                }
            }

            // Render the cell so it's readable
            if (controlDataGridView.Columns[e.ColumnIndex].Name == "effectString")
            {
                if (e.Value != null)
                {
                    string effectId = (string)e.Value;
                    string friendlyName = dataProcessor.GetEffect(effectId).ToString();
                    e.Value = string.Format("  {0}  ", friendlyName);
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "triggerString")
            {
                if (e.Value != null)
                {
                    string effectId = (string)e.Value;
                    var trigger = dataProcessor.GetTrigger(effectId);
                    if (trigger != null)
                    {
                        e.Value = dataProcessor.GetTrigger(effectId).ToString();
                        cell.ToolTipText = dataProcessor.GetTrigger(effectId).name;
                    }
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "targetString")
            {
                if (e.Value != null)
                {
                    string effectId = (string)e.Value;
                    string friendlyName = dataProcessor.GetTarget(effectId).ToString();
                    e.Value = friendlyName;

                    cell.ToolTipText = dataProcessor.GetTarget(effectId).id;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            dataProcessor.SaveControllerData();
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            ControlItem item = new ControlItem("T00", "0");
            dataProcessor.AddControlItem(item);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void controlDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var row = controlDataGridView.CurrentCell.RowIndex;
            var item = dataProcessor.controllerData[row];

            if (e.ClickedItem.Name == "deleteItemItem")
            {
                dataProcessor.DeleteControlItem(item);
            }
            else if (e.ClickedItem.Name == "logFilter")
            {
                filterTextBox.Text = string.Format("VPX {0}", item.triggerString);
            }
            else if (e.ClickedItem.Name == "deleteAllDisabled")
            {
                dataProcessor.DeleteAllDisabled();
            }
            else if (e.ClickedItem.Name == "duplicate")
            {
                dataProcessor.DuplicateItem(item);
            }
        }

        private void controlDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (editForm.IsDisposed)
            {
                editForm = new EditItemForm();
            }
            var mainLocation = this.Location;

            editForm.SetQueueRef(ref commandQueue);

            var row = controlDataGridView.CurrentCell.RowIndex;
            var item = dataProcessor.controllerData[row];

            editForm.Show();
            editForm.Location = new Point(mainLocation.X - editForm.Size.Width, mainLocation.Y);
            editForm.SetControlItem(dataProcessor, item);
        }

        private void disableLog()
        {
            if (loggingEnabled)
            {
                statusStrip1.Items[0].Text = "Logging is disabled";
                loggingEnabled = false;

                disableButton.Text = "Enable Logging";
            }
            else
            {
                statusStrip1.Items[0].Text = "Logging is enabled";
                loggingEnabled = true;
                disableButton.Text = "Disable Logging";
            }
        }

        private void logMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "addAsItem")
            {
                if (logListViews.SelectedItem == null)
                {
                    return;
                }
                var logValue = logListViews.SelectedItem.ToString();
                if (logValue.StartsWith("VPX"))
                {
                    var (success, triggerString, valueString, _) = VPXProcessor.splitCommandString(logValue.Replace("VPX ", ""));
                    if (success)
                    {
                        ControlItem newItem = new ControlItem(triggerString, valueString);
                        dataProcessor.AddControlItem(newItem);
                    }
                }
            }
        }

        private void autoAddItemsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.SaveBoolSetting(Constants.SettingAutoAddItems, autoAddItemsCheckbox.Checked);
            dataProcessor.autoAddItems = autoAddItemsCheckbox.Checked;

            statusStrip1.Items[1].Text = (dataProcessor.autoAddItems) ? "Auto add items enabled" : "Auto add items disabled";
        }

        private void itemFilterCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            Settings.SaveSetting(Constants.SettingItemFilter, itemFilterCombo.Text);
        }

        private void controlDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (controlDataGridView.DataSource != null)
            {
                dataProcessor.unsavedChanges = true;
            }
        }

        private void controlDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                controlDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dataProcessor.unsavedChanges = true;
            }
        }

        public void loadAvailableTables()
        {
            var fullPath = Path.Combine(@"Data", "Tables");
            var files = Directory.GetFiles(fullPath, "*", SearchOption.TopDirectoryOnly)
                .Where(f => !f.Contains("-triggers"))
                .Select(f => Path.GetFileName(f).Replace(".json", ""))
                .ToList();

            tableComboBox.Items.Clear();
            tableComboBox.Items.AddRange(files.ToArray());
        }

        private void tableComboBox_TextChanged(object sender, EventArgs e)
        {
            if (dataProcessor == null)
            {
                return;
            }

            var Romname = tableComboBox.Text;
            Settings.SaveSetting(Constants.SettingLastTable, Romname);

            if (dataProcessor.unsavedChanges)
            {
                if (MessageBox.Show("Do you want to save your changes before switching tables?", "Unsaved Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataProcessor.SaveControllerData();
                }
            }

            dataProcessor.LoadTableInformation(Romname);
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logListViews.Items.Clear();
        }

        private void ignoreDuplicatesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.SaveBoolSetting(Constants.SettingLogBlockDuplicates, ignoreDuplicatesCheckbox.Checked);
        }

        private void disableButton_Click(object sender, EventArgs e)
        {
            disableLog();
        }

        private void logMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
