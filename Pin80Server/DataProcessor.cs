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

namespace Pin80Server
{
    public class DataProcessor : IListSource
    {
        private readonly BindingList<ControlItem> controllerData = new BindingList<ControlItem>();
        public Dictionary<string, Trigger> triggers = new Dictionary<string, Trigger>();
        public Dictionary<string, Target> targets = new Dictionary<string, Target>();

        public Dictionary<string, IAction> actionMap = new Dictionary<string, IAction>();

        public bool ContainsListCollection => throw new System.NotImplementedException();

        public IList GetList()
        {
            return controllerData;
        }

        public IAction getAction(string actionId)
        {
            return actionMap[actionId];
        }

        public Trigger getTrigger(string command)
        {
            return triggers[command];
        }

        public Target getTarget(string id)
        {
            return targets[id];
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
                        actionMap[action.id] = new OnOffAction(action);
                        break;
                    case "BLINK":
                        actionMap[action.id] = new BlinkAction(action);
                        break;
                    default:
                        throw new Exception("Not a valid action");
                }
            });
        }

        private void populateTriggers(string Romname)
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

        public void LoadTableInformation(string Romname)
        {
            // Search for various combinations of files.
            // actions and triggers and default

            //Can't continue unless we have actions and triggers.
            populateActions();
            populateTriggers(Romname);
            populateTargets();

            // Exact match, remove version, default?
            var fullPath = Path.Combine(@"Data", $"{Romname}.json");
            var ldata = LoadFile(fullPath);

            foreach (var d in ldata)
            {
                controllerData.Add(d);
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
