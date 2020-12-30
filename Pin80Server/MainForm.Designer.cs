
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripMenuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.romNameLabel = new System.Windows.Forms.Label();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addItemButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Targets = new System.Windows.Forms.TabPage();
            this.Actions = new System.Windows.Forms.TabPage();
            this.Triggers = new System.Windows.Forms.TabPage();
            this.Control = new System.Windows.Forms.TabPage();
            this.controlDataGridView = new System.Windows.Forms.DataGridView();
            this.targetString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabView = new System.Windows.Forms.TabControl();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.triggerStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlItemSource = new System.Windows.Forms.BindingSource(this.components);
            this.logMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).BeginInit();
            this.tabView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlItemSource)).BeginInit();
            this.logMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuTest,
            this.stripMenuIgnore});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // stripMenuTest
            // 
            this.stripMenuTest.Name = "stripMenuTest";
            this.stripMenuTest.Size = new System.Drawing.Size(108, 22);
            this.stripMenuTest.Text = "Test";
            // 
            // stripMenuIgnore
            // 
            this.stripMenuIgnore.Name = "stripMenuIgnore";
            this.stripMenuIgnore.Size = new System.Drawing.Size(108, 22);
            this.stripMenuIgnore.Text = "Ignore";
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.addItemButton);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.romNameLabel);
            this.panel1.Controls.Add(this.tableNameLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 46);
            this.panel1.TabIndex = 7;
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
            this.splitContainer1.Size = new System.Drawing.Size(845, 526);
            this.splitContainer1.SplitterDistance = 280;
            this.splitContainer1.TabIndex = 8;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ContextMenuStrip = this.logMenuStrip;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 11);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(825, 212);
            this.listBox1.TabIndex = 2;
            // 
            // Targets
            // 
            this.Targets.Location = new System.Drawing.Point(4, 22);
            this.Targets.Name = "Targets";
            this.Targets.Padding = new System.Windows.Forms.Padding(3);
            this.Targets.Size = new System.Drawing.Size(831, 286);
            this.Targets.TabIndex = 4;
            this.Targets.Text = "Targets";
            this.Targets.UseVisualStyleBackColor = true;
            // 
            // Actions
            // 
            this.Actions.Location = new System.Drawing.Point(4, 22);
            this.Actions.Name = "Actions";
            this.Actions.Size = new System.Drawing.Size(831, 286);
            this.Actions.TabIndex = 2;
            this.Actions.Text = "Actions";
            this.Actions.UseVisualStyleBackColor = true;
            // 
            // Triggers
            // 
            this.Triggers.Location = new System.Drawing.Point(4, 22);
            this.Triggers.Name = "Triggers";
            this.Triggers.Padding = new System.Windows.Forms.Padding(3);
            this.Triggers.Size = new System.Drawing.Size(831, 286);
            this.Triggers.TabIndex = 1;
            this.Triggers.Text = "Triggers";
            this.Triggers.UseVisualStyleBackColor = true;
            // 
            // Control
            // 
            this.Control.Controls.Add(this.controlDataGridView);
            this.Control.Location = new System.Drawing.Point(4, 22);
            this.Control.Name = "Control";
            this.Control.Padding = new System.Windows.Forms.Padding(3);
            this.Control.Size = new System.Drawing.Size(831, 286);
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
            this.controlDataGridView.AutoGenerateColumns = false;
            this.controlDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.controlDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.controlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.controlDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.triggerStringDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.actionString,
            this.targetString,
            this.commentDataGridViewTextBoxColumn});
            this.controlDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.controlDataGridView.DataSource = this.controlItemSource;
            this.controlDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.controlDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.controlDataGridView.Location = new System.Drawing.Point(6, 6);
            this.controlDataGridView.MultiSelect = false;
            this.controlDataGridView.Name = "controlDataGridView";
            this.controlDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.controlDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.controlDataGridView.Size = new System.Drawing.Size(819, 236);
            this.controlDataGridView.TabIndex = 2;
            this.controlDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.controlDataGridView_CellFormatting);
            this.controlDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.controlDataGridView_CellMouseDown);
            this.controlDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.controlDataGridView_ColumnHeaderMouseClick);
            this.controlDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.controlDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.controlDataGridView_MouseDoubleClick);
            // 
            // targetString
            // 
            this.targetString.DataPropertyName = "targetString";
            this.targetString.HeaderText = "Target";
            this.targetString.Name = "targetString";
            this.targetString.Width = 63;
            // 
            // actionString
            // 
            this.actionString.DataPropertyName = "actionString";
            this.actionString.HeaderText = "Action";
            this.actionString.Name = "actionString";
            this.actionString.Width = 62;
            // 
            // tabView
            // 
            this.tabView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabView.Controls.Add(this.Control);
            this.tabView.Controls.Add(this.Triggers);
            this.tabView.Controls.Add(this.Actions);
            this.tabView.Controls.Add(this.Targets);
            this.tabView.Location = new System.Drawing.Point(3, 3);
            this.tabView.Name = "tabView";
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(839, 312);
            this.tabView.TabIndex = 6;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.Width = 51;
            // 
            // triggerStringDataGridViewTextBoxColumn
            // 
            this.triggerStringDataGridViewTextBoxColumn.DataPropertyName = "triggerString";
            this.triggerStringDataGridViewTextBoxColumn.HeaderText = "Trigger";
            this.triggerStringDataGridViewTextBoxColumn.Name = "triggerStringDataGridViewTextBoxColumn";
            this.triggerStringDataGridViewTextBoxColumn.Width = 65;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 59;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 76;
            // 
            // controlItemSource
            // 
            this.controlItemSource.DataSource = typeof(Pin80Server.Models.JSONSerializer.ControlItem);
            this.controlItemSource.Sort = "";
            this.controlItemSource.CurrentChanged += new System.EventHandler(this.jsonTableBindingSource_CurrentChanged);
            // 
            // logMenuStrip
            // 
            this.logMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogItem,
            this.disableLogItem});
            this.logMenuStrip.Name = "logMenuStrip";
            this.logMenuStrip.Size = new System.Drawing.Size(136, 48);
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
            this.disableLogItem.Tag = "disableItem";
            this.disableLogItem.Text = "Disable Log";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 602);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Control.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlDataGridView)).EndInit();
            this.tabView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlItemSource)).EndInit();
            this.logMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label romNameLabel;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.BindingSource controlItemSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stripMenuTest;
        private System.Windows.Forms.ToolStripMenuItem stripMenuIgnore;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabView;
        private System.Windows.Forms.TabPage Control;
        private System.Windows.Forms.DataGridView controlDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionString;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetString;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage Triggers;
        private System.Windows.Forms.TabPage Actions;
        private System.Windows.Forms.TabPage Targets;
        private System.Windows.Forms.ContextMenuStrip logMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearLogItem;
        private System.Windows.Forms.ToolStripMenuItem disableLogItem;
    }
}

