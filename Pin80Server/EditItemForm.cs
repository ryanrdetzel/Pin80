using Pin80Server.Models.Effects;
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
        private DataProcessor dataProcessor;
        private BlockingCollection<string> commandQueue;

        public EditItemForm()
        {
            InitializeComponent();
        }

        private void EditItemForm_Load(object sender, EventArgs e)
        {
            // Make sure a valid target is selected?
            UpdateEffectsDropdown();
        }

        public void SetQueueRef(ref BlockingCollection<string> cq)
        {
            commandQueue = cq;
        }

        public void SetControlItem(DataProcessor dp, ControlItem item)
        {
            this.item = item;
            dataProcessor = dp;
            trigger = dp.GetTrigger(item.triggerString);

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
        }

        private void UpdateEffectsDropdown()
        {
            if (targetsComboBox.SelectedItem == null)
            {
                return;
            }

            Debug.WriteLine("Target combo changed");

            var targetId = targetsComboBox.SelectedItem.ToString();
            var target = dataProcessor.GetTarget(targetId);

            if (target != null)
            {
                var validEffectNames = target.validEffects();

                var validEffects = dataProcessor.effectsDict.Where(a => validEffectNames.Contains(a.Value.kind)).ToDictionary(p => p.Key, p => p.Value);

                var effectValues = validEffects.Keys.ToArray();
                effectComboBox.Items.Clear();
                effectComboBox.Items.AddRange(effectValues);
                effectComboBox.SelectedItem = item.effectString;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Update the actual data item
            // TODO Validate this is okay

            // TODO - Update the triggers file

            item.effectString = effectComboBox.SelectedItem?.ToString();
            item.targetString = targetsComboBox.SelectedItem?.ToString();

            item.comment = commentTextBox.Text;
            item.enabled = enabledCheckbox.Checked; //TODO only allow editing if it passes validation
            item.value = valueTextBox.Text;
            item.triggerString = triggerTextBox.Text;

            dataProcessor.UpdateControlItem(item);

            //Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TargetsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var targetId = e.ListItem.ToString();
            var target = dataProcessor.GetTarget(targetId);

            var value = (target == null) ? targetId : string.Format("{0} ({1}) Port: {2}", target.name, targetId, target.port);
            e.Value = value;
        }

        private void EffectComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var effectId = e.ListItem.ToString();
            var effect = dataProcessor.GetEffect(effectId);

            var value = (effect == null) ? effectId : effect.ToString();
            e.Value = value;
        }


        private void Button1_Click(object sender, EventArgs e)
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

        private void TargetsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateEffectsDropdown();
        }
    }
}
