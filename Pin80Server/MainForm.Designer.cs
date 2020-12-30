
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
            this.itemMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripMenuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteItemItem = new System.Windows.Forms.ToolStripMenuItem();
            this.romNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.itemFilterCombo = new System.Windows.Forms.ComboBox();
            this.addItemButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabView = new System.Windows.Forms.TabControl();
            this.Control = new System.Windows.Forms.TabPage();
            this.controlDataGridView = new System.Windows.Forms.DataGridView();
            this.Triggers = new System.Windows.Forms.TabPage();
            this.autoAddItemsCheckbox = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.logMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.logginStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.autoAddEnabledLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triggerString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commnet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabView.SuspendLayout();
            this.Control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).BeginInit();
            this.Triggers.SuspendLayout();
            this.logMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemMenuStrip
            // 
            this.itemMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuTest,
            this.stripMenuIgnore,
            this.toolStripSeparator1,
            this.deleteItemItem});
            this.itemMenuStrip.Name = "contextMenuStrip1";
            this.itemMenuStrip.Size = new System.Drawing.Size(135, 76);
            this.itemMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.itemMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // stripMenuTest
            // 
            this.stripMenuTest.Name = "stripMenuTest";
            this.stripMenuTest.Size = new System.Drawing.Size(134, 22);
            this.stripMenuTest.Text = "Test";
            // 
            // stripMenuIgnore
            // 
            this.stripMenuIgnore.Name = "stripMenuIgnore";
            this.stripMenuIgnore.Size = new System.Drawing.Size(134, 22);
            this.stripMenuIgnore.Text = "Ignore";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // deleteItemItem
            // 
            this.deleteItemItem.Name = "deleteItemItem";
            this.deleteItemItem.Size = new System.Drawing.Size(134, 22);
            this.deleteItemItem.Text = "Delete Item";
            // 
            // romNameLabel
            // 
            this.romNameLabel.AutoSize = true;
            this.romNameLabel.Location = new System.Drawing.Point(49, 16);
            this.romNameLabel.Name = "romNameLabel";
            this.romNameLabel.Size = new System.Drawing.Size(33, 13);
            this.romNameLabel.TabIndex = 3;
            this.romNameLabel.Text = "None";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.itemFilterCombo);
            this.panel1.Controls.Add(this.addItemButton);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.romNameLabel);
            this.panel1.Controls.Add(this.tableNameLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 46);
            this.panel1.TabIndex = 7;
            // 
            // itemFilterCombo
            // 
            this.itemFilterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemFilterCombo.FormattingEnabled = true;
            this.itemFilterCombo.Items.AddRange(new object[] {
            "Show All",
            "Show All But Hidden",
            "Show Enabled Only",
            "Show Hidden Only"});
            this.itemFilterCombo.Location = new System.Drawing.Point(521, 11);
            this.itemFilterCombo.Name = "itemFilterCombo";
            this.itemFilterCombo.Size = new System.Drawing.Size(141, 21);
            this.itemFilterCombo.TabIndex = 7;
            this.itemFilterCombo.SelectedValueChanged += new System.EventHandler(this.itemFilterCombo_SelectedValueChanged);
            // 
            // addItemButton
            // 
            this.addItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addItemButton.Location = new System.Drawing.Point(679, 11);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(75, 23);
            this.addItemButton.TabIndex = 6;
            this.addItemButton.Text = "New Item";
            this.addItemButton.UseVisualStyleBackColor = true;
            this.addItemButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(760, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(13, 16);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(34, 13);
            this.tableNameLabel.TabIndex = 4;
            this.tableNameLabel.Text = "Table";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabView);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(845, 511);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 8;
            // 
            // tabView
            // 
            this.tabView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabView.Controls.Add(this.Control);
            this.tabView.Controls.Add(this.Triggers);
            this.tabView.Location = new System.Drawing.Point(3, 3);
            this.tabView.Name = "tabView";
            this.tabView.Padding = new System.Drawing.Point(8, 2);
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(839, 294);
            this.tabView.TabIndex = 6;
            // 
            // Control
            // 
            this.Control.Controls.Add(this.controlDataGridView);
            this.Control.Location = new System.Drawing.Point(4, 20);
            this.Control.Name = "Control";
            this.Control.Padding = new System.Windows.Forms.Padding(3);
            this.Control.Size = new System.Drawing.Size(831, 270);
            this.Control.TabIndex = 0;
            this.Control.Text = "Control";
            this.Control.UseVisualStyleBackColor = true;
            // 
            // controlDataGridView
            // 
            this.controlDataGridView.AllowUserToAddRows = false;
            this.controlDataGridView.AllowUserToDeleteRows = false;
            this.controlDataGridView.AllowUserToResizeRows = false;
            this.controlDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.controlDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.controlDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.controlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.controlDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabled,
            this.Id,
            this.triggerString,
            this.valueString,
            this.actionString,
            this.targetString,
            this.commnet});
            this.controlDataGridView.ContextMenuStrip = this.itemMenuStrip;
            this.controlDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.controlDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.controlDataGridView.Location = new System.Drawing.Point(6, 6);
            this.controlDataGridView.MultiSelect = false;
            this.controlDataGridView.Name = "controlDataGridView";
            this.controlDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.controlDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.controlDataGridView.Size = new System.Drawing.Size(819, 258);
            this.controlDataGridView.TabIndex = 2;
            this.controlDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.controlDataGridView_CellFormatting);
            this.controlDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.controlDataGridView_CellMouseDown);
            this.controlDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.controlDataGridView_ColumnHeaderMouseClick);
            this.controlDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.controlDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.controlDataGridView_MouseDoubleClick);
            // 
            // Triggers
            // 
            this.Triggers.Controls.Add(this.autoAddItemsCheckbox);
            this.Triggers.Location = new System.Drawing.Point(4, 20);
            this.Triggers.Name = "Triggers";
            this.Triggers.Padding = new System.Windows.Forms.Padding(3);
            this.Triggers.Size = new System.Drawing.Size(831, 270);
            this.Triggers.TabIndex = 1;
            this.Triggers.Text = "Settings";
            this.Triggers.UseVisualStyleBackColor = true;
            // 
            // autoAddItemsCheckbox
            // 
            this.autoAddItemsCheckbox.AutoSize = true;
            this.autoAddItemsCheckbox.Location = new System.Drawing.Point(18, 18);
            this.autoAddItemsCheckbox.Name = "autoAddItemsCheckbox";
            this.autoAddItemsCheckbox.Size = new System.Drawing.Size(98, 17);
            this.autoAddItemsCheckbox.TabIndex = 0;
            this.autoAddItemsCheckbox.Text = "Auto Add Items";
            this.autoAddItemsCheckbox.UseVisualStyleBackColor = true;
            this.autoAddItemsCheckbox.CheckedChanged += new System.EventHandler(this.autoAddItemsCheckbox_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.ContextMenuStrip = this.logMenuStrip;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(5, 5);
            this.listBox1.Margin = new System.Windows.Forms.Padding(1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(837, 197);
            this.listBox1.TabIndex = 2;
            // 
            // logMenuStrip
            // 
            this.logMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogItem,
            this.disableLogItem});
            this.logMenuStrip.Name = "logMenuStrip";
            this.logMenuStrip.Size = new System.Drawing.Size(136, 48);
            this.logMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.logMenuStrip_ItemClicked);
            // 
            // clearLogItem
            // 
            this.clearLogItem.Name = "clearLogItem";
            this.clearLogItem.Size = new System.Drawing.Size(135, 22);
            this.clearLogItem.Text = "Clear Log";
            // 
            // disableLogItem
            // 
            this.disableLogItem.Name = "disableLogItem";
            this.disableLogItem.Size = new System.Drawing.Size(135, 22);
            this.disableLogItem.Tag = "disableLogItem";
            this.disableLogItem.Text = "Disable Log";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logginStatusLabel,
            this.autoAddEnabledLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(869, 24);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // logginStatusLabel
            // 
            this.logginStatusLabel.Name = "logginStatusLabel";
            this.logginStatusLabel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.logginStatusLabel.Size = new System.Drawing.Size(147, 19);
            this.logginStatusLabel.Text = "Logging is enabled";
            // 
            // autoAddEnabledLabel
            // 
            this.autoAddEnabledLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.autoAddEnabledLabel.Name = "autoAddEnabledLabel";
            this.autoAddEnabledLabel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.autoAddEnabledLabel.Size = new System.Drawing.Size(179, 19);
            this.autoAddEnabledLabel.Text = "Auto Add Items Enabled";
            // 
            // enabled
            // 
            this.enabled.DataPropertyName = "enabled";
            this.enabled.HeaderText = "Enabled";
            this.enabled.Name = "enabled";
            this.enabled.Width = 52;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 43;
            // 
            // triggerString
            // 
            this.triggerString.DataPropertyName = "triggerString";
            this.triggerString.HeaderText = "Trigger";
            this.triggerString.Name = "triggerString";
            this.triggerString.ReadOnly = true;
            this.triggerString.Width = 65;
            // 
            // valueString
            // 
            this.valueString.DataPropertyName = "value";
            this.valueString.HeaderText = "Value";
            this.valueString.Name = "valueString";
            this.valueString.ReadOnly = true;
            this.valueString.Width = 59;
            // 
            // actionString
            // 
            this.actionString.DataPropertyName = "actionString";
            this.actionString.FillWeight = 120F;
            this.actionString.HeaderText = "Action";
            this.actionString.Name = "actionString";
            this.actionString.ReadOnly = true;
            this.actionString.Width = 62;
            // 
            // targetString
            // 
            this.targetString.DataPropertyName = "targetString";
            this.targetString.HeaderText = "Target";
            this.targetString.Name = "targetString";
            this.targetString.ReadOnly = true;
            this.targetString.Width = 63;
            // 
            // commnet
            // 
            this.commnet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commnet.DataPropertyName = "comment";
            this.commnet.HeaderText = "Comment";
            this.commnet.Name = "commnet";
            this.commnet.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 602);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.itemMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabView.ResumeLayout(false);
            this.Control.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).EndInit();
            this.Triggers.ResumeLayout(false);
            this.Triggers.PerformLayout();
            this.logMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label romNameLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.ContextMenuStrip itemMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem stripMenuTest;
        private System.Windows.Forms.ToolStripMenuItem stripMenuIgnore;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabView;
        private System.Windows.Forms.TabPage Control;
        private System.Windows.Forms.DataGridView controlDataGridView;
        private System.Windows.Forms.TabPage Triggers;
        private System.Windows.Forms.ContextMenuStrip logMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearLogItem;
        private System.Windows.Forms.ToolStripMenuItem disableLogItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel logginStatusLabel;
        private System.Windows.Forms.CheckBox autoAddItemsCheckbox;
        private System.Windows.Forms.ToolStripStatusLabel autoAddEnabledLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteItemItem;
        private System.Windows.Forms.ComboBox itemFilterCombo;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerString;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueString;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionString;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetString;
        private System.Windows.Forms.DataGridViewTextBoxColumn commnet;
    }
}

