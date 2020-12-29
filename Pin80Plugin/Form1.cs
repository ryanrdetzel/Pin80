using Microsoft.Win32;
using System;
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
            sendLCheckbox.Checked = bool.Parse(key.GetValue(sendLCheckbox.Name, "true").ToString());
            sendNCheckbox.Checked = bool.Parse(key.GetValue(sendNCheckbox.Name, "true").ToString());

            key.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox loggingEnabledCheckbox = sender as CheckBox;

            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Plugin");
            key.SetValue("loggingEnabled", loggingEnabledCheckbox.Checked);
            key.Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Plugin");
            key.SetValue(box.Name, box.Checked);
            key.Close();
        }
    }
}
