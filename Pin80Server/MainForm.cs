using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class MainForm : Form
    {
        private const int maxLogLength = 1000;
        private string filterValue = "";

        public MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
        }

        public void setDataProcessor(DataProcessor dp)
        {
            Debug.WriteLine("Setting new data source");
            controlDataGridView.DataSource = dp;
        }

        public void setTableName(string name)
        {
            tableNameLabel.Text = name;
        }
        public void setRomName(string name)
        {
            romNameLabel.Text = name;
        }

        public void addLogEntry(string entry)
        {
            if (tabView.SelectedIndex == 3)
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
            Environment.Exit(Environment.ExitCode);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            SetupDataGridView();
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveWindowState();
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            saveWindowState();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            saveWindowState();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Row {0}", e.RowIndex);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // Filter
            filterValue = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void controlDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dp = controlDataGridView.DataSource as DataProcessor;
            DataGridViewCell cell = controlDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Render the cell so it's readable
            if (controlDataGridView.Columns[e.ColumnIndex].Name == "actionColumn")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    string friendlyName = dp.getAction(actionId).ToString();
                    e.Value = friendlyName;
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "triggerColumn")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    string friendlyName = dp.getTrigger(actionId).ToString();
                    e.Value = friendlyName;

                    cell.ToolTipText = dp.getTrigger(actionId).name;
                }
            }
            else if (controlDataGridView.Columns[e.ColumnIndex].Name == "targetColumn")
            {
                if (e.Value != null)
                {
                    string actionId = (string)e.Value;
                    string friendlyName = dp.getTarget(actionId).ToString();
                    e.Value = friendlyName;

                    cell.ToolTipText = dp.getTarget(actionId).id;
                }
            }
        }

        private void jsonTableBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
