using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class MainForm : Form
    {
        private const int maxLogLength = 1000;
        private readonly string filterValue = "";

        private bool loggingEnabled = true;

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

            var itemFilter = Settings.ReadSetting(Constants.SettingItemFilter);
            if (itemFilter == "")
            {
                itemFilter = itemFilterCombo.Items[0].ToString();
            }
            itemFilterCombo.SelectedItem = itemFilter;
        }

        public void setQueueRef(ref BlockingCollection<string> cq)
        {
            commandQueue = cq;
        }

        public void setDataProcessor(DataProcessor dp)
        {
            dataProcessor = dp;
            controlDataGridView.DataSource = dp.bSource;

            statusStrip1.Items[1].Text = (dataProcessor.autoAddItems) ? "Auto add items enabled" : "Auto add items disabled";
            autoAddItemsCheckbox.Checked = dataProcessor.autoAddItems;
        }

        public void setRomName(string name)
        {

            tableComboBox.Text = name;
        }

        public void addLogEntry(string entry)
        {
            if (loggingEnabled)
            {
                if (filterValue == "" || entry.StartsWith(filterValue))
                {
                    listBox1.Items.Insert(0, entry);

                    if (listBox1.Items.Count > maxLogLength)
                    {
                        listBox1.Items.RemoveAt(maxLogLength);
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
                    dataProcessor.saveControllerData();

                    //Application.Exit();
                    Environment.Exit(Environment.ExitCode);
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

        private void Form1_Load_1(object sender, EventArgs e)
        {
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

        private void Form1_Move(object sender, EventArgs e)
        {
            saveWindowState();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Row Click {0}", e.RowIndex);
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
            if (controlDataGridView.Columns[e.ColumnIndex].Name == "actionString")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    string friendlyName = dataProcessor.getAction(actionId).ToString();
                    e.Value = friendlyName;
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "triggerString")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    var trigger = dataProcessor.getTrigger(actionId);
                    if (trigger != null)
                    {
                        e.Value = dataProcessor.getTrigger(actionId).ToString();
                        cell.ToolTipText = dataProcessor.getTrigger(actionId).name;
                    }
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "targetString")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    string friendlyName = dataProcessor.getTarget(actionId).ToString();
                    e.Value = friendlyName;

                    cell.ToolTipText = dataProcessor.getTarget(actionId).id;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            dataProcessor.saveControllerData();
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            ControlItem item = new ControlItem("T00", "0");
            dataProcessor.addControlItem(item);
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

            if (e.ClickedItem.Name == "stripMenuTest")
            {
                var trigger = item.triggerString;
                var value = 0; // item.value;

                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                string cmd = string.Format("VPX {0} {1} {2}", trigger, value, now);
                commandQueue.Add(cmd);
            }
            else if (e.ClickedItem.Name == "deleteItemItem")
            {
                dataProcessor.deleteControlItem(item);
            }
        }

        private void controlDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (editForm.IsDisposed)
            {
                editForm = new EditItemForm();
            }
            var mainLocation = this.Location;

            editForm.setQueueRef(ref commandQueue);
            editForm.Show();
            editForm.Location = new Point(mainLocation.X - editForm.Size.Width, mainLocation.Y);

            var row = controlDataGridView.CurrentCell.RowIndex;
            var item = dataProcessor.controllerData[row];

            editForm.setControlItem(dataProcessor, item);
        }

        private void logMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "clearLogItem")
            {
                listBox1.Items.Clear();
            }
            else if (e.ClickedItem.Tag.ToString() == "disableLogItem")
            {
                if (e.ClickedItem.Text == "Disable Log")
                {
                    e.ClickedItem.Text = "Enable Log";
                    statusStrip1.Items[0].Text = "Logging is disabled";
                    loggingEnabled = false;
                }
                else
                {
                    e.ClickedItem.Text = "Disable Log";
                    statusStrip1.Items[0].Text = "Logging is enabled";
                    loggingEnabled = true;
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
            var Romname = tableComboBox.Text;

            if (dataProcessor.unsavedChanges)
            {
                if (MessageBox.Show("Do you want to save your changes before switching tables?", "Unsaved Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataProcessor.saveControllerData();
                }
            }
            dataProcessor.LoadTableInformation(Romname);
        }
    }
}
