using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class MainForm : Form
    {
        const int maxLogLength = 1000;
        string filterValue = "";

        private DataProcessor dataProcessor;

        public MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
        }

        public void setDataProcessor(DataProcessor dp)
        {
            dataProcessor = dp;
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

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            SetupDataGridView();
            PopulateDataGridView();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            saveWindowState();
            Environment.Exit(Environment.ExitCode);
        }

        private void SetupDataGridView()
        {
            controlDataGridView.RowHeadersVisible = false;
            controlDataGridView.AutoGenerateColumns = false;
            controlDataGridView.SelectionMode =DataGridViewSelectionMode.FullRowSelect;
            controlDataGridView.MultiSelect = false;
        }

        private void PopulateDataGridView()
        {
            controlDataGridView.DataSource = dataProcessor.getProcessedData();
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
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            
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
    }
}
