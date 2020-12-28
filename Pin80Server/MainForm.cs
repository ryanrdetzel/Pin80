using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Pin80Server
{

    public partial class MainForm : Form
    {
        private DataGridView songsDataGridView;

        static int maxLogLength = 1000;
        string filterValue = "";

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
            if (filterValue == "" || entry.StartsWith(filterValue))
            {
                listBox1.Items.Insert(0, entry);

                if (listBox1.Items.Count > maxLogLength)
                {
                    listBox1.Items.RemoveAt(maxLogLength);
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            this.songsDataGridView = dataGridView1;
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }

        private void songsDataGridView_CellFormatting(object sender,
       System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            //if (e != null)
            //{
            //    if (this.songsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
            //    {
            //        if (e.Value != null)
            //        {
            //            try
            //            {
            //                e.Value = DateTime.Parse(e.Value.ToString())
            //                    .ToLongDateString();
            //                e.FormattingApplied = true;
            //            }
            //            catch (FormatException)
            //            {
            //                Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
            //            }
            //        }
            //    }
            //}
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Console.WriteLine("Closing");
            saveWindowState();
            Environment.Exit(Environment.ExitCode);
        }

        //private void addNewRowButton_Click(object sender, EventArgs e)
        //{
        //    this.songsDataGridView.Rows.Add();
        //}

        //private void deleteRowButton_Click(object sender, EventArgs e)
        //{
        //    if (this.songsDataGridView.SelectedRows.Count > 0 &&
        //        this.songsDataGridView.SelectedRows[0].Index !=
        //        this.songsDataGridView.Rows.Count - 1)
        //    {
        //        this.songsDataGridView.Rows.RemoveAt(
        //            this.songsDataGridView.SelectedRows[0].Index);
        //    }
        //}

        private void SetupLayout()
        {
            //this.Size = new Size(600, 500);

            //addNewRowButton.Text = "Add Row";
            //addNewRowButton.Location = new Point(10, 10);
            //addNewRowButton.Click += new EventHandler(addNewRowButton_Click);

            //deleteRowButton.Text = "Delete Row";
            //deleteRowButton.Location = new Point(100, 10);
            //deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            //buttonPanel.Controls.Add(addNewRowButton);
            //buttonPanel.Controls.Add(deleteRowButton);
            //buttonPanel.Height = 50;
            //buttonPanel.Dock = DockStyle.Bottom;

            //this.Controls.Add(this.buttonPanel);
        }

        private void SetupDataGridView()
        {
            //this.Controls.Add(songsDataGridView);

            //songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            //songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //songsDataGridView.ColumnHeadersDefaultCellStyle.Font =
            //    new Font(songsDataGridView.Font, FontStyle.Bold);

            //songsDataGridView.Name = "songsDataGridView";
            //songsDataGridView.Location = new Point(8, 8);
            //songsDataGridView.Size = new Size(500, 250);
            //songsDataGridView.AutoSizeRowsMode =
            //    DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //songsDataGridView.ColumnHeadersBorderStyle =
            //    DataGridViewHeaderBorderStyle.Single;
            //songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //songsDataGridView.GridColor = Color.Black;
            songsDataGridView.RowHeadersVisible = false;

            //songsDataGridView.ColumnCount = 2;
            //songsDataGridView.Columns[0].Name = "Trigger";
            //songsDataGridView.Columns[1].Name = "Action";

            //DataGridViewComboBoxColumn comboboxColumn = CreateComboBoxColumn();
            //SetAlternateChoicesUsingItems(comboboxColumn);
            //comboboxColumn.HeaderText = "TitleOfCourtesy (via Items property)";
            // Tack this example column onto the end.
            //DataGridView1.Columns.Add(comboboxColumn);

            //songsDataGridView.Columns[2].Name = "Title";
            //songsDataGridView.Columns[3].Name = "Artist";
            //songsDataGridView.Columns[4].Name = "Album";
            //songsDataGridView.Columns[4].DefaultCellStyle.Font =
            //    new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);
            //songsDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var input = "[{\"Enabled\":true,\"Trigger\":\"One\",\"Action\":\"Turn on lights\"},{\"Enabled\":true,\"Trigger\":\"Two\",\"Action\":\"Turn offlights\"},{\"Enabled\":true,\"Trigger\":\"Three\",\"Action\":\"Fire Solenoi 2\"}]";
            var result = JsonConvert.DeserializeObject<List<JsonTable>>(input);
            Debug.WriteLine("{0}", result[0]);

            songsDataGridView.AutoGenerateColumns = false;
            songsDataGridView.DataSource = result;
            songsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            songsDataGridView.MultiSelect = false;
            //songsDataGridView.Dock = DockStyle.Fill;

            songsDataGridView.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                songsDataGridView_CellFormatting);

            var row = songsDataGridView.Rows[0];
           // row.DefaultCellStyle.BackColor = Color.Red;
           // row.DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0, 0);
        }

        private void PopulateDataGridView()
        {

            string[] row0 = { "One", "" };

            string[] row1 = { "1960", "6", "Fools Rush In",
            "Frank Sinatra", "Nice 'N' Easy" };
            string[] row2 = { "11/11/1971", "1", "One of These Days",
            "Pink Floyd", "Meddle" };
            string[] row3 = { "1988", "7", "Where Is My Mind?",
            "Pixies", "Surfer Rosa" };
            string[] row4 = { "5/1981", "9", "Can't Find My Mind",
            "Cramps", "Psychedelic Jungle" };
            string[] row5 = { "6/10/2003", "13",
            "Scatterbrain. (As Dead As Leaves.)",
            "Radiohead", "Hail to the Thief" };
            string[] row6 = { "6/30/1992", "3", "Dress", "P J Harvey", "Dry" };

            //songsDataGridView.Rows.Add(row0);
            //songsDataGridView.Rows.Add(row1);
            //songsDataGridView.Rows.Add(row2);
            //songsDataGridView.Rows.Add(row3);
            //songsDataGridView.Rows.Add(row4);
            //songsDataGridView.Rows.Add(row5);
            //songsDataGridView.Rows.Add(row6);

            //songsDataGridView.Columns[0].DisplayIndex = 3;
            //songsDataGridView.Columns[1].DisplayIndex = 4;
            //songsDataGridView.Columns[2].DisplayIndex = 0;
            //songsDataGridView.Columns[3].DisplayIndex = 1;
            //songsDataGridView.Columns[4].DisplayIndex = 2;
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.F1Size.Width == 0 || Properties.Settings.Default.F1Size.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.F1State;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                //Debug.WriteLine("Size loaded: {0}", Properties.Settings.Default.F1Size);
            
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
            //Debug.WriteLine("Saving size {0}", this.Size);
        
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

            // don't forget to save the settings
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
            //Debug.WriteLine("Size changed: {0}", this.Size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ts = new TriggerSettings();
            ts.Show();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Row {0}", e.RowIndex);
        }

        private void jsonTableBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
