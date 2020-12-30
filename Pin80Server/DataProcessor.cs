using Newtonsoft.Json;
using Pin80Server.Models;
using Pin80Server.Models.Actions;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server
{
    public class DataProcessor : IListSource
    {
        private string Romname;

        private readonly BindingList<ControlItem> controllerData = new BindingList<ControlItem>();
        public Dictionary<string, Trigger> triggers = new Dictionary<string, Trigger>();

        public Dictionary<string, Target> targets = new Dictionary<string, Target>();
        public Dictionary<string, IAction> actions = new Dictionary<string, IAction>();

        public bool ContainsListCollection => throw new System.NotImplementedException();

        public DataProcessor()
        {
            controllerData.ListChanged += listOfParts_ListChanged;
        }

        void listOfParts_ListChanged(object sender, ListChangedEventArgs e)
        {
            //MessageBox.Show(e.ListChangedType.ToString());
            Debug.WriteLine("Data changed " + e.ListChangedType.ToString());
        }

        public IList GetList()
        {
            return controllerData;
        }

        public void addControlItem(ControlItem item)
        {
            controllerData.Add(item);
        }

        public void updateControlItem(ControlItem NewItem)
        {
            var index = controllerData.ToList().FindIndex(Item => Item.id == NewItem.id);
            controllerData[index] = NewItem;
        }

        public void saveControllerData()
        {
            var fullPath = Path.Combine(@"Data", $"{Romname}.json");
            Debug.WriteLine("Saving to " + fullPath);

            using (StreamWriter file = File.CreateText(fullPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, controllerData);
            }
        }

        /* For this table see if there is a control item for this trigger */
        public List<ControlItem> getControlItems(string trigger)
        {
            return controllerData.Where(item => item.triggerString == trigger).ToList();
        }

        public IAction getAction(string actionId)
        {
            return actionId != null && actions.ContainsKey(actionId) ? actions[actionId] : null;
        }

        public Trigger getTrigger(string command)
        {
            return command != null && triggers.ContainsKey(command) ? triggers[command] : null;
        }

        public Target getTarget(string id)
        {
            return id != null && targets.ContainsKey(id) ? targets[id] : null;
        }

        private void populateActions()
        {
            var actionFullPath = Path.Combine(@"Data", $"actions.json");
            var actionList = LoadActions(actionFullPath);
            //Convert the json to actual actions
            actionList.ForEach(action =>
            {
                Debug.WriteLine(action.id);
                switch (action.kind)
                {
                    case "ONOFF":
                        actions[action.id] = new OnOffAction(action);
                        break;
                    case "BLINK":
                        actions[action.id] = new BlinkAction(action);
                        break;
                    default:
                        throw new Exception("Not a valid action");
                }
            });
        }

        private void populateTriggers()
        {
            var fullPath = Path.Combine(@"Data", $"{Romname}-triggers.json");
            var triggerList = LoadTriggers(fullPath);

            //Convert the json to actual actions
            triggerList.ForEach(trigger =>
            {
                triggers[trigger.command] = trigger;
            });
        }

        private void populateTargets()
        {
            var fullPath = Path.Combine(@"Data", $"targets.json");
            var targetList = LoadTargets(fullPath);

            //Convert the json to actual actions
            targetList.ForEach(target =>
            {
                targets[target.id] = target;
            });
        }

        public void LoadTableInformation(string Romname, MainForm mainForm)
        {
            this.Romname = Romname;

            actions.Clear();
            targets.Clear();
            triggers.Clear();
            if (mainForm.IsHandleCreated)
            {
                mainForm.BeginInvoke((MethodInvoker)delegate ()
                {
                    controllerData.Clear();
                });
            }
            // TODO Need this refresh but it breaks threads
            // controllerData.Clear();


            // Search for various combinations of files.
            // actions and triggers and default

            //Can't continue unless we have actions and triggers.
            populateActions();
            populateTriggers();
            populateTargets();

            // Exact match, remove version, default?
            var fullPath = Path.Combine(@"Data", $"{Romname}.json");
            var ldata = LoadFile(fullPath);

            foreach (var d in ldata)
            {
                if (mainForm.IsHandleCreated)
                {
                    mainForm.BeginInvoke((MethodInvoker)delegate ()
                    {
                        controllerData.Add(d);
                    });
                }
                
            }

            // TODO handle exceptions here.
            Debug.WriteLine("Loading table for: " + fullPath);
            Debug.WriteLine(controllerData.Count);
        }
        private List<Trigger> LoadTriggers(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Trigger>>(json);
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

        private BindingList<ControlItem> LoadFile(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ControlItem>>(json);
            }
        }
    }
}
