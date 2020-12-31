
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
            this.stripMenuIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.logFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteItemItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllDisabled = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableComboBox = new System.Windows.Forms.ComboBox();
            this.itemFilterCombo = new System.Windows.Forms.ComboBox();
            this.addItemButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabView = new System.Windows.Forms.TabControl();
            this.Control = new System.Windows.Forms.TabPage();
            this.controlDataGridView = new System.Windows.Forms.DataGridView();
            this.enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triggerString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commnet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triggers = new System.Windows.Forms.TabPage();
            this.autoAddItemsCheckbox = new System.Windows.Forms.CheckBox();
            this.ignoreDuplicatesCheckbox = new System.Windows.Forms.CheckBox();
            this.disableButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logListViews = new System.Windows.Forms.ListBox();
            this.logMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.logginStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.autoAddEnabledLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.stripMenuIgnore,
            this.logFilter,
            this.duplicate,
            this.toolStripSeparator1,
            this.deleteItemItem,
            this.deleteAllDisabled});
            this.itemMenuStrip.Name = "contextMenuStrip1";
            this.itemMenuStrip.Size = new System.Drawing.Size(173, 120);
            this.itemMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.itemMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // stripMenuIgnore
            // 
            this.stripMenuIgnore.Enabled = false;
            this.stripMenuIgnore.Name = "stripMenuIgnore";
            this.stripMenuIgnore.Size = new System.Drawing.Size(172, 22);
            this.stripMenuIgnore.Text = "Ignore";
            // 
            // logFilter
            // 
            this.logFilter.Name = "logFilter";
            this.logFilter.Size = new System.Drawing.Size(172, 22);
            this.logFilter.Text = "Set as log filter";
            // 
            // duplicate
            // 
            this.duplicate.Name = "duplicate";
            this.duplicate.Size = new System.Drawing.Size(172, 22);
            this.duplicate.Text = "Duplicate Item";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // deleteItemItem
            // 
            this.deleteItemItem.Name = "deleteItemItem";
            this.deleteItemItem.Size = new System.Drawing.Size(172, 22);
            this.deleteItemItem.Text = "Delete Item";
            // 
            // deleteAllDisabled
            // 
            this.deleteAllDisabled.Name = "deleteAllDisabled";
            this.deleteAllDisabled.Size = new System.Drawing.Size(172, 22);
            this.deleteAllDisabled.Text = "Delete All Disabled";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableComboBox);
            this.panel1.Controls.Add(this.itemFilterCombo);
            this.panel1.Controls.Add(this.addItemButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.tableNameLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 46);
            this.panel1.TabIndex = 7;
            // 
            // tableComboBox
            // 
            this.tableComboBox.FormattingEnabled = true;
            this.tableComboBox.Location = new System.Drawing.Point(53, 13);
            this.tableComboBox.Name = "tableComboBox";
            this.tableComboBox.Size = new System.Drawing.Size(289, 21);
            this.tableComboBox.TabIndex = 8;
            this.tableComboBox.TextChanged += new System.EventHandler(this.tableComboBox_TextChanged);
            // 
            // itemFilterCombo
            // 
            this.itemFilterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemFilterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemFilterCombo.Enabled = false;
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
            this.addItemButton.Text = "Add Item";
            this.addItemButton.UseVisualStyleBackColor = true;
            this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(760, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ignoreDuplicatesCheckbox);
            this.splitContainer1.Panel2.Controls.Add(this.disableButton);
            this.splitContainer1.Panel2.Controls.Add(this.clearLogButton);
            this.splitContainer1.Panel2.Controls.Add(this.filterTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.logListViews);
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
            this.tabView.Padding = new System.Drawing.Point(15, 4);
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(839, 294);
            this.tabView.TabIndex = 6;
            // 
            // Control
            // 
            this.Control.Controls.Add(this.controlDataGridView);
            this.Control.Location = new System.Drawing.Point(4, 24);
            this.Control.Name = "Control";
            this.Control.Padding = new System.Windows.Forms.Padding(3);
            this.Control.Size = new System.Drawing.Size(831, 266);
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
            this.controlDataGridView.Name = "controlDataGridView";
            this.controlDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.controlDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.controlDataGridView.Size = new System.Drawing.Size(819, 254);
            this.controlDataGridView.TabIndex = 2;
            this.controlDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.controlDataGridView_CellContentClick);
            this.controlDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.controlDataGridView_CellFormatting);
            this.controlDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.controlDataGridView_CellMouseDown);
            this.controlDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.controlDataGridView_CellValueChanged);
            this.controlDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.controlDataGridView_MouseDoubleClick);
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
            this.triggerString.FillWeight = 120F;
            this.triggerString.HeaderText = "Trigger";
            this.triggerString.Name = "triggerString";
            this.triggerString.ReadOnly = true;
            this.triggerString.Width = 65;
            // 
            // valueString
            // 
            this.valueString.DataPropertyName = "value";
            this.valueString.FillWeight = 120F;
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
            this.targetString.FillWeight = 120F;
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
            // Triggers
            // 
            this.Triggers.Controls.Add(this.autoAddItemsCheckbox);
            this.Triggers.Location = new System.Drawing.Point(4, 24);
            this.Triggers.Name = "Triggers";
            this.Triggers.Padding = new System.Windows.Forms.Padding(3);
            this.Triggers.Size = new System.Drawing.Size(831, 266);
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
            // ignoreDuplicatesCheckbox
            // 
            this.ignoreDuplicatesCheckbox.AutoSize = true;
            this.ignoreDuplicatesCheckbox.Enabled = false;
            this.ignoreDuplicatesCheckbox.Location = new System.Drawing.Point(301, 8);
            this.ignoreDuplicatesCheckbox.Name = "ignoreDuplicatesCheckbox";
            this.ignoreDuplicatesCheckbox.Size = new System.Drawing.Size(109, 17);
            this.ignoreDuplicatesCheckbox.TabIndex = 7;
            this.ignoreDuplicatesCheckbox.Text = "Ignore Duplicates";
            this.ignoreDuplicatesCheckbox.UseVisualStyleBackColor = true;
            this.ignoreDuplicatesCheckbox.CheckedChanged += new System.EventHandler(this.ignoreDuplicatesCheckbox_CheckedChanged);
            // 
            // disableButton
            // 
            this.disableButton.Enabled = false;
            this.disableButton.Location = new System.Drawing.Point(220, 5);
            this.disableButton.Name = "disableButton";
            this.disableButton.Size = new System.Drawing.Size(75, 23);
            this.disableButton.TabIndex = 6;
            this.disableButton.Text = "Disable";
            this.disableButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(144, 4);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 5;
            this.clearLogButton.Text = "Clear";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(37, 5);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(100, 20);
            this.filterTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter";
            // 
            // logListViews
            // 
            this.logListViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logListViews.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logListViews.ContextMenuStrip = this.logMenuStrip;
            this.logListViews.FormattingEnabled = true;
            this.logListViews.Location = new System.Drawing.Point(5, 31);
            this.logListViews.Margin = new System.Windows.Forms.Padding(1);
            this.logListViews.Name = "logListViews";
            this.logListViews.Size = new System.Drawing.Size(837, 171);
            this.logListViews.TabIndex = 2;
            // 
            // logMenuStrip
            // 
            this.logMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogItem,
            this.disableLogItem,
            this.addAsItem});
            this.logMenuStrip.Name = "logMenuStrip";
            this.logMenuStrip.Size = new System.Drawing.Size(138, 70);
            this.logMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.logMenuStrip_ItemClicked);
            // 
            // clearLogItem
            // 
            this.clearLogItem.Name = "clearLogItem";
            this.clearLogItem.Size = new System.Drawing.Size(137, 22);
            this.clearLogItem.Text = "Clear Log";
            // 
            // disableLogItem
            // 
            this.disableLogItem.Name = "disableLogItem";
            this.disableLogItem.Size = new System.Drawing.Size(137, 22);
            this.disableLogItem.Tag = "disableLogItem";
            this.disableLogItem.Text = "Disable Log";
            // 
            // addAsItem
            // 
            this.addAsItem.Name = "addAsItem";
            this.addAsItem.Size = new System.Drawing.Size(137, 22);
            this.addAsItem.Text = "Add as Item";
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
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.SizeChanged += new System.EventHandler(this.Form1_Move);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Move);
            this.itemMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.ContextMenuStrip itemMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem stripMenuIgnore;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox logListViews;
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
        private System.Windows.Forms.ComboBox tableComboBox;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem logFilter;
        private System.Windows.Forms.ToolStripMenuItem deleteAllDisabled;
        private System.Windows.Forms.ToolStripMenuItem duplicate;
        private System.Windows.Forms.ToolStripMenuItem addAsItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerString;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueString;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionString;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetString;
        private System.Windows.Forms.DataGridViewTextBoxColumn commnet;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.CheckBox ignoreDuplicatesCheckbox;
        private System.Windows.Forms.Button disableButton;
    }
}

