﻿using Newtonsoft.Json;
using Pin80Server.Models;
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

        public BindingList<ControlItem> controllerData = new BindingList<ControlItem>();

        public Dictionary<string, Trigger> triggersDict = new Dictionary<string, Trigger>();
        public Dictionary<string, Target> targetsDict = new Dictionary<string, Target>();
        public Dictionary<string, IAction> actionsDict = new Dictionary<string, IAction>();

        public bool ContainsListCollection => throw new System.NotImplementedException();

        public DataProcessor()
        {
            autoAddItems = Settings.ReadBoolSetting(Constants.SettingAutoAddItems);

            bSource = new BindingSource();
            bSource.DataSource = controllerData;

            controllerData.ListChanged += ControllerData_ListChanged;
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
            controllerData.Add(item);
        }

        public void deleteControlItem(ControlItem item)
        {
            controllerData.Remove(item);
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

        public IAction getAction(string actionId)
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
                switch (action.kind)
                {
                    case "ONOFF":
                        actionsDict[action.id] = new OnOffAction(action);
                        break;
                    case "BLINK":
                        actionsDict[action.id] = new BlinkAction(action);
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

            var ldata = LoadFile(fullPath);

            var sortedListInstance = new BindingList<ControlItem>(ldata.OrderBy(x => x.triggerString).ToList());

            // Add each item on the UI thread
            if (mainForm.IsHandleCreated)
            {
                mainForm.BeginInvoke((MethodInvoker)delegate ()
                {
                    foreach (var d in sortedListInstance)
                    {
                        controllerData.Add(d);
                    }
                    unsavedChanges = false;
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
            catch (FileNotFoundException e)
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

        private List<Models.JSONSerializer.Action> LoadActions(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Models.JSONSerializer.Action>>(json);
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
            catch (FileNotFoundException e)
            {
                return new List<ControlItem>();
            }
        }
    }
}
