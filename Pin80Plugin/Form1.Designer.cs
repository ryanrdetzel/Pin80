﻿
namespace Pin80Plugin
{
    partial class Form1
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
            this.pluginLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sendLCheckbox = new System.Windows.Forms.CheckBox();
            this.sendNCheckbox = new System.Windows.Forms.CheckBox();
            this.LoggingEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginLog
            // 
            this.pluginLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginLog.Location = new System.Drawing.Point(12, 110);
            this.pluginLog.Name = "pluginLog";
            this.pluginLog.Size = new System.Drawing.Size(673, 370);
            this.pluginLog.TabIndex = 0;
            this.pluginLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.sendLCheckbox);
            this.panel1.Controls.Add(this.sendNCheckbox);
            this.panel1.Controls.Add(this.LoggingEnabled);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 92);
            this.panel1.TabIndex = 1;
            // 
            // sendLCheckbox
            // 
            this.sendLCheckbox.AutoSize = true;
            this.sendLCheckbox.Location = new System.Drawing.Point(489, 44);
            this.sendLCheckbox.Name = "sendLCheckbox";
            this.sendLCheckbox.Size = new System.Drawing.Size(173, 17);
            this.sendLCheckbox.TabIndex = 3;
            this.sendLCheckbox.Text = "Send commands starting with L";
            this.sendLCheckbox.UseVisualStyleBackColor = true;
            this.sendLCheckbox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // sendNCheckbox
            // 
            this.sendNCheckbox.AutoSize = true;
            this.sendNCheckbox.Location = new System.Drawing.Point(489, 21);
            this.sendNCheckbox.Name = "sendNCheckbox";
            this.sendNCheckbox.Size = new System.Drawing.Size(175, 17);
            this.sendNCheckbox.TabIndex = 2;
            this.sendNCheckbox.Text = "Send commands starting with N";
            this.sendNCheckbox.UseVisualStyleBackColor = true;
            this.sendNCheckbox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // LoggingEnabled
            // 
            this.LoggingEnabled.AutoSize = true;
            this.LoggingEnabled.Location = new System.Drawing.Point(25, 21);
            this.LoggingEnabled.Name = "LoggingEnabled";
            this.LoggingEnabled.Size = new System.Drawing.Size(135, 17);
            this.LoggingEnabled.TabIndex = 1;
            this.LoggingEnabled.Text = "Enable Debug Logging";
            this.LoggingEnabled.UseVisualStyleBackColor = true;
            this.LoggingEnabled.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Only enable this if you\'re having issues";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 492);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pluginLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox pluginLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox LoggingEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox sendLCheckbox;
        private System.Windows.Forms.CheckBox sendNCheckbox;
    }
}