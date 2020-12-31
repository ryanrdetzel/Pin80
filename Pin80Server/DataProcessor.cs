using Library.Forms;
using Newtonsoft.Json;
using Pin80Server.Models.Actions;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server
{
    public class DataProcessor
    {
        private string Romname;
        private MainForm mainForm;

        public bool autoAddItems = false;
        public bool unsavedChanges = false;

        public BindingSource bSource;

        public SortableBindingList<ControlItem> controllerData = new SortableBindingList<ControlItem>();

        public Dictionary<string, Trigger> triggersDict = new Dictionary<string, Trigger>();
        public Dictionary<string, Target> targetsDict = new Dictionary<string, Target>();
        public Dictionary<string, Models.Action> actionsDict = new Dictionary<string, Models.Action>();

        public bool ContainsListCollection => throw new System.NotImplementedException();

        public DataProcessor()
        {
            autoAddItems = Settings.ReadBoolSetting(Constants.SettingAutoAddItems);

            bSource = new BindingSource();
            bSource.DataSource = controllerData;

            controllerData.ListChanged += ControllerData_ListChanged;
        }

        public void SortList()
        {
            //var sortedListInstance = new BindingList<ControlItem>(controllerData
            //    .OrderBy(x => !x.enabled)
            //    .ThenBy(x => x.triggerString).ToList());
            //var sortedList = controllerData.OrderBy(x => !x.enabled).ToList();
            //controllerData = new BindingList<ControlItem>(sortedList);
            ////bSource.DataSource = sortedListInstance;
            //controllerData.ResetBindings();
        }

        private void ControllerData_ListChanged(object sender, ListChangedEventArgs e)
        {
            unsavedChanges = true;
        }

        public void setMainForm(MainForm mf)
        {
            mainForm = mf;
        }

        public void addControlItem(ControlItem item)
        {
            controllerData.Insert(0, item);
        }

        public void deleteControlItem(ControlItem item)
        {
            controllerData.Remove(item);
        }

        public void duplicateItem(ControlItem item)
        {
            ControlItem newItem = new ControlItem(item.triggerString, item.value);
            newItem.actionString = item.actionString;
            newItem.targetString = item.targetString;
            newItem.comment = item.comment;
            addControlItem(newItem);
        }

        public void deleteAllDisabled()
        {
            var co = controllerData.Where(item => !item.enabled).ToList();
            foreach (ControlItem item in co)
            {
                controllerData.Remove(item);
            }
        }

        public void updateControlItem(ControlItem NewItem)
        {
            var index = controllerData.ToList().FindIndex(Item => Item.id == NewItem.id);
            controllerData[index] = NewItem;
        }

        public void saveControllerData()
        {
            var fullPath = Path.Combine(@"Data", "Tables", $"{Romname}.json");
            Debug.WriteLine("Saving to " + fullPath);

            using (StreamWriter file = File.CreateText(fullPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, controllerData);
            }
            unsavedChanges = false;

            mainForm.loadAvailableTables();
        }

        /* For this table see if there is a control item for this trigger */
        public List<ControlItem> getControlItems(string trigger)
        {
            return controllerData.Where(item => item.triggerString == trigger).ToList();
        }

        public Models.Action getAction(string actionId)
        {
            return actionId != null && actionsDict.ContainsKey(actionId) ? actionsDict[actionId] : null;
        }

        /* Always returns a trigger */
        public Trigger getTrigger(string command)
        {
            return command != null && triggersDict.ContainsKey(command) ? triggersDict[command] : new Trigger(command);
        }

        public Target getTarget(string id)
        {
            return id != null && targetsDict.ContainsKey(id) ? targetsDict[id] : null;
        }

        private void populateActions()
        {
            var actionFullPath = Path.Combine(@"Data", $"actions.json");
            var actionList = LoadActions(actionFullPath);
            //Convert the json to actual actions
            actionList.ForEach(action =>
            {
                /**
                 * Make sure to add these to the target validActions
                 */
                switch (action.kind)
                {
                    case "ONOFF":
                        actionsDict[action.id] = new OnOffAction(action);
                        break;
                    case "PIXEL":
                        actionsDict[action.id] = new PixelAction(action);
                        break;
                    case "PIXELRUN":
                        actionsDict[action.id] = new PixelRunAction(action);
                        break;
                    default:
                        throw new Exception("Not a valid action");
                }
            });
        }

        private void populateTriggers()
        {
            var fullPath = Path.Combine(@"Data", "Tables", $"{Romname}-triggers.json");
            var triggerList = LoadTriggers(fullPath);

            //Convert the json to actual actions
            triggerList.ForEach(trigger =>
            {
                triggersDict[trigger.command] = trigger;
            });
        }

        private void populateTargets()
        {
            var fullPath = Path.Combine(@"Data", $"targets.json");
            var targetList = LoadTargets(fullPath);

            //Convert the json to actual actions
            targetList.ForEach(target =>
            {
                targetsDict[target.id] = target;
            });
        }

        public void LoadTableInformation(string Romname)
        {
            this.Romname = Romname;
            var fullPath = Path.Combine(@"Data", "Tables", $"{Romname}.json");

            actionsDict.Clear();
            targetsDict.Clear();
            triggersDict.Clear();

            // This has to be handled on the main ui thread since it's binding
            if (mainForm.IsHandleCreated)
            {
                mainForm.BeginInvoke((MethodInvoker)delegate ()
                {
                    controllerData.Clear();
                });
            }

            // TODO Search for various combinations of files.

            // TODO Can't continue unless we have actions and triggers.
            populateActions();
            populateTriggers();
            populateTargets();

            var controlItems = LoadFile(fullPath);

            //var sortedListInstance = new BindingList<ControlItem>(ldata.OrderBy(x => !x.enabled).ThenBy(x => x.triggerString).ToList());

            // Add each item on the UI thread
            if (mainForm.IsHandleCreated)
            {
                mainForm.BeginInvoke((MethodInvoker)delegate ()
                {
                    foreach (var item in controlItems)
                    {
                        // Make sure actions and targets still exist for all the controlItems.
                        // If they don't, clear it and disable the item
                        Models.Action action = getAction(item.actionString);
                        Target target = getTarget(item.targetString);

                        if (action == null)
                        {
                            item.actionString = null;
                            item.enabled = false;
                        }

                        if (target == null)
                        {
                            item.targetString = null;
                            item.enabled = false;
                        }

                        controllerData.Add(item);
                    }
                    unsavedChanges = false;
                    //SortList();
                });
            }

            // TODO handle exceptions here like failing to open a file.
            Debug.WriteLine("Loading table for: " + fullPath);
        }

        private List<Trigger> LoadTriggers(string fullPath)
        {
            try
            {
                using (StreamReader r = new StreamReader(fullPath))
                {

                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<Trigger>>(json);
                }
            }
            catch (FileNotFoundException)
            {
                return new List<Trigger>();
            }
        }

        private List<Target> LoadTargets(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Target>>(json);
            }
        }

        private List<Models.JSONSerializer.ActionSerializer> LoadActions(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Models.JSONSerializer.ActionSerializer>>(json);
            }
        }

        private List<ControlItem> LoadFile(string fullPath)
        {
            try
            {
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<ControlItem>>(json);
                }
            }
            catch (FileNotFoundException)
            {
                return new List<ControlItem>();
            }
        }
    }
}
