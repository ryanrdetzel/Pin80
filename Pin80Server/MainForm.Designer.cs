
namespace Pin80Server
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.controlDataGridView = new System.Windows.Forms.DataGridView();
            this.romNameLabel = new System.Windows.Forms.Label();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.tabView = new System.Windows.Forms.TabControl();
            this.Control = new System.Windows.Forms.TabPage();
            this.Triggers = new System.Windows.Forms.TabPage();
            this.Actions = new System.Windows.Forms.TabPage();
            this.LogPanel = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.jsonTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.triggerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).BeginInit();
            this.tabView.SuspendLayout();
            this.Control.SuspendLayout();
            this.LogPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jsonTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // controlDataGridView
            // 
            this.controlDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDataGridView.AutoGenerateColumns = false;
            this.controlDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.controlDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.controlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.controlDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.triggerDataGridViewTextBoxColumn,
            this.actionDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.controlDataGridView.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.jsonTableBindingSource, "Enabled", true));
            this.controlDataGridView.DataBindings.Add(new System.Windows.Forms.Binding("CellBorderStyle", this.jsonTableBindingSource, "Trigger", true));
            this.controlDataGridView.DataSource = this.jsonTableBindingSource;
            this.controlDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.controlDataGridView.Location = new System.Drawing.Point(6, 6);
            this.controlDataGridView.Name = "controlDataGridView";
            this.controlDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.controlDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.controlDataGridView.Size = new System.Drawing.Size(825, 383);
            this.controlDataGridView.TabIndex = 2;
            this.controlDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // romNameLabel
            // 
            this.romNameLabel.AutoSize = true;
            this.romNameLabel.Location = new System.Drawing.Point(13, 11);
            this.romNameLabel.Name = "romNameLabel";
            this.romNameLabel.Size = new System.Drawing.Size(35, 13);
            this.romNameLabel.TabIndex = 3;
            this.romNameLabel.Text = "label1";
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(13, 25);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(35, 13);
            this.tableNameLabel.TabIndex = 4;
            this.tableNameLabel.Text = "label2";
            // 
            // tabView
            // 
            this.tabView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabView.Controls.Add(this.Control);
            this.tabView.Controls.Add(this.Triggers);
            this.tabView.Controls.Add(this.Actions);
            this.tabView.Controls.Add(this.LogPanel);
            this.tabView.Location = new System.Drawing.Point(12, 64);
            this.tabView.Name = "tabView";
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(845, 421);
            this.tabView.TabIndex = 6;
            // 
            // Control
            // 
            this.Control.Controls.Add(this.controlDataGridView);
            this.Control.Location = new System.Drawing.Point(4, 22);
            this.Control.Name = "Control";
            this.Control.Padding = new System.Windows.Forms.Padding(3);
            this.Control.Size = new System.Drawing.Size(837, 395);
            this.Control.TabIndex = 0;
            this.Control.Text = "Control";
            this.Control.UseVisualStyleBackColor = true;
            // 
            // Triggers
            // 
            this.Triggers.Location = new System.Drawing.Point(4, 22);
            this.Triggers.Name = "Triggers";
            this.Triggers.Padding = new System.Windows.Forms.Padding(3);
            this.Triggers.Size = new System.Drawing.Size(837, 395);
            this.Triggers.TabIndex = 1;
            this.Triggers.Text = "Triggers";
            this.Triggers.UseVisualStyleBackColor = true;
            // 
            // Actions
            // 
            this.Actions.Location = new System.Drawing.Point(4, 22);
            this.Actions.Name = "Actions";
            this.Actions.Size = new System.Drawing.Size(837, 395);
            this.Actions.TabIndex = 2;
            this.Actions.Text = "Actions";
            this.Actions.UseVisualStyleBackColor = true;
            // 
            // LogPanel
            // 
            this.LogPanel.Controls.Add(this.listBox1);
            this.LogPanel.Controls.Add(this.panel2);
            this.LogPanel.Location = new System.Drawing.Point(4, 22);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Padding = new System.Windows.Forms.Padding(3);
            this.LogPanel.Size = new System.Drawing.Size(837, 395);
            this.LogPanel.TabIndex = 3;
            this.LogPanel.Text = "Logs";
            this.LogPanel.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "dfg"});
            this.listBox1.Location = new System.Drawing.Point(6, 73);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(825, 316);
            this.listBox1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(834, 64);
            this.panel2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(204, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.romNameLabel);
            this.panel1.Controls.Add(this.tableNameLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 46);
            this.panel1.TabIndex = 7;
            // 
            // jsonTableBindingSource
            // 
            this.jsonTableBindingSource.DataSource = typeof(Pin80Server.ControlItem);
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.Width = 51;
            // 
            // triggerDataGridViewTextBoxColumn
            // 
            this.triggerDataGridViewTextBoxColumn.DataPropertyName = "trigger";
            this.triggerDataGridViewTextBoxColumn.HeaderText = "trigger";
            this.triggerDataGridViewTextBoxColumn.Name = "triggerDataGridViewTextBoxColumn";
            this.triggerDataGridViewTextBoxColumn.Width = 61;
            // 
            // actionDataGridViewTextBoxColumn
            // 
            this.actionDataGridViewTextBoxColumn.DataPropertyName = "action";
            this.actionDataGridViewTextBoxColumn.HeaderText = "action";
            this.actionDataGridViewTextBoxColumn.Name = "actionDataGridViewTextBoxColumn";
            this.actionDataGridViewTextBoxColumn.Width = 61;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 497);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabView);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).EndInit();
            this.tabView.ResumeLayout(false);
            this.Control.ResumeLayout(false);
            this.LogPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jsonTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView controlDataGridView;
        private System.Windows.Forms.Label romNameLabel;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.BindingSource jsonTableBindingSource;
        private System.Windows.Forms.TabControl tabView;
        private System.Windows.Forms.TabPage Control;
        private System.Windows.Forms.TabPage Triggers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage Actions;
        private System.Windows.Forms.TabPage LogPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}

