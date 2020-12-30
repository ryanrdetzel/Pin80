using Pin80Server.Models;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pin80Server
{
    public partial class EditItemForm : Form
    {
        private ControlItem item;
        private Trigger trigger;
        private IAction action;
        private Target target;
        private DataProcessor dataProcessor;

        public EditItemForm()
        {
            InitializeComponent();
        }

        public void setControlItem(DataProcessor dp, ControlItem item)
        {
            this.dataProcessor = dp;
            this.item = item;
            this.trigger = dp.getTrigger(item.triggerString);
            this.action = dp.getAction(item.actionString);
            this.target = dp.getTarget(item.targetString);

            // TODO we should check if any of these are null

            enabledCheckbox.Checked = item.enabled;
            commentTextBox.Text = item.comment;
            valueTextBox.Text = item.value;

            triggerLabel.Text = item.triggerString;
            triggerNameField.Text = trigger?.name ?? "";

            var targetValues = dp.targets.Keys.ToArray();
            targetsComboBox.Items.AddRange(targetValues);
            targetsComboBox.SelectedItem = item.targetString;

            var actionValues = dp.actions.Keys.ToArray();
            actionComboBox.Items.AddRange(actionValues);
            actionComboBox.SelectedItem = item.actionString;


            // Populate the dropdowns
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Saving");

            // Update the actual data item
            // TODO Validate this is okay

            //TODO - Update the triggers file

            item.actionString = actionComboBox.SelectedItem.ToString();
            item.targetString = targetsComboBox.SelectedItem.ToString();
            
            item.comment = commentTextBox.Text;
            item.enabled = enabledCheckbox.Checked; //TODO only allow editing if it passes validation
            item.value = valueTextBox.Text;

            dataProcessor.updateControlItem(item);

            Close();
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

            var value = (action == null) ? actionId : string.Format("{0} ({1})", action.name, actionId);
            e.Value = value;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
