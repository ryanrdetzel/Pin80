﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pin80Plugin
{
    public partial class Form1 : Form
    {
        public void updateLog(string log)
        {
            this.pluginLog.Text = log;
        }

        public Form1()
        {
            InitializeComponent();
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Plugin");
            LoggingEnabled.Checked = bool.Parse(key.GetValue("loggingEnabled", "false").ToString());
            key.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox loggingEnabledCheckbox = sender as CheckBox;
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Plugin");

            key.SetValue("loggingEnabled", loggingEnabledCheckbox.Checked);
            key.Close();
        }
    }
}
