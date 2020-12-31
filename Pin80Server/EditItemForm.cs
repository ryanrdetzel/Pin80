using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class EditItemForm : Form
    {
        private ControlItem item;
        private Trigger trigger;
        private Models.Action action;
        private Target target;
        private DataProcessor dataProcessor;
        private BlockingCollection<string> commandQueue;

        public EditItemForm()
        {
            InitializeComponent();
        }

        private void EditItemForm_Load(object sender, EventArgs e)
        {
            // Make sure a valid target is selected?
            updateActionsDropdown();
        }

        public void setQueueRef(ref BlockingCollection<string> cq)
        {
            commandQueue = cq;
        }

        public void setControlItem(DataProcessor dp, ControlItem item)
        {
            this.item = item;
            dataProcessor = dp;
            trigger = dp.getTrigger(item.triggerString);
            action = dp.getAction(item.actionString);
            target = dp.getTarget(item.targetString);

            // TODO we should check if any of these are null

            enabledCheckbox.Checked = item.enabled;
            commentTextBox.Text = item.comment;
            valueTextBox.Text = item.value;

            triggerTextBox.Text = item.triggerString;
            triggerNameField.Text = trigger?.name ?? "";

            var targetValues = dp.targetsDict.Keys.ToArray();
            targetsComboBox.Items.Clear();
            targetsComboBox.Items.AddRange(targetValues);
            targetsComboBox.SelectedItem = item.targetString;

            //var actionValues = dp.actionsDict.Keys.ToArray();
            //actionComboBox.Items.Clear();
            //actionComboBox.Items.AddRange(actionValues);
            //actionComboBox.SelectedItem = item.actionString;
        }

        private void updateActionsDropdown()
        {
            if (targetsComboBox.SelectedItem == null)
            {
                return;
            }

            Debug.WriteLine("Target combo changed");

            var targetId = targetsComboBox.SelectedItem.ToString();
            var target = dataProcessor.getTarget(targetId);

            if (target != null)
            {
                var validActionNames = target.validActions();

                var validActions = dataProcessor.actionsDict.Where(a => validActionNames.Contains(a.Value.kind)).ToDictionary(p => p.Key, p => p.Value);

                var actionValues = validActions.Keys.ToArray();
                actionComboBox.Items.Clear();
                actionComboBox.Items.AddRange(actionValues);
                actionComboBox.SelectedItem = item.actionString;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Update the actual data item
            // TODO Validate this is okay

            // TODO - Update the triggers file

            item.actionString = actionComboBox.SelectedItem?.ToString();
            item.targetString = targetsComboBox.SelectedItem?.ToString();

            item.comment = commentTextBox.Text;
            item.enabled = enabledCheckbox.Checked; //TODO only allow editing if it passes validation
            item.value = valueTextBox.Text;
            item.triggerString = triggerTextBox.Text;

            dataProcessor.updateControlItem(item);

            //Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void targetsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var targetId = e.ListItem.ToString();
            var target = dataProcessor.getTarget(targetId);

            var value = (target == null) ? targetId : string.Format("{0} ({1}) Port: {2}", target.name, targetId, target.port);
            e.Value = value;
        }

        private void actionComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var actionId = e.ListItem.ToString();
            var action = dataProcessor.getAction(actionId);

            var value = (action == null) ? actionId : action.ToString();
            e.Value = value;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var trigger = item.triggerString;
            if (testTextBox.Text == "")
            {
                testTextBox.Text = "1";
            }
            var value = testTextBox.Text;

            var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string cmd = string.Format("VPX {0} {1} {2}", trigger, value, now);
            commandQueue.Add(cmd);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void targetsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            updateActionsDropdown();
        }
    }
}
