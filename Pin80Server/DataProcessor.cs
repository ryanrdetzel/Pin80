using Library.Forms;
using Newtonsoft.Json;
using Pin80Server.Models;
using Pin80Server.Models.Effects;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Concurrent;
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
        private BlockingCollection<string> commandQueue;

        public bool autoAddItems = false;
        public bool unsavedChanges = false;

        public BindingSource bSource;

        public SortableBindingList<ControlItem> controllerData = new SortableBindingList<ControlItem>();

        public Dictionary<string, Trigger> triggersDict = new Dictionary<string, Trigger>();
        public Dictionary<string, Models.Target> targetsDict = new Dictionary<string, Models.Target>();
        public Dictionary<string, Effect> effectsDict = new Dictionary<string, Effect>();

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

        public void setQueueRef(ref BlockingCollection<string> cq)
        {
            commandQueue = cq;
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
            newItem.effectString = item.effectString;
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

        public Effect getEffect(string effectId)
        {
            return effectId != null && effectsDict.ContainsKey(effectId) ? effectsDict[effectId] : null;
        }

        /* Always returns a trigger */
        public Trigger getTrigger(string command)
        {
            return command != null && triggersDict.ContainsKey(command) ? triggersDict[command] : new Trigger(command);
        }

        public Models.Target getTarget(string id)
        {
            return id != null && targetsDict.ContainsKey(id) ? targetsDict[id] : null;
        }

        private void populateEffects()
        {
            var effectFullPath = Path.Combine(@"Data", $"effects.json");
            var effectList = LoadEffect(effectFullPath);
            //Convert the json to actual effects
            effectList.ForEach(effect =>
            {
                /**
                 * Make sure to add these to the target validEffects
                 */
                switch (effect.kind)
                {
                    case "ONOFF":
                        effectsDict[effect.id] = new OnOffEffect(effect);
                        break;
                    case "PIXEL":
                        effectsDict[effect.id] = new PixelEffect(effect);
                        break;
                    case "PIXELRUN":
                        effectsDict[effect.id] = new PixelRunEffect(effect);
                        break;
                    case "PIXELCOMIT":
                        effectsDict[effect.id] = new PixelComitEffect(effect);
                        break;
                    default:
                        throw new Exception("Not a valid effect");
                }
            });
        }

        private void populateTriggers()
        {
            var fullPath = Path.Combine(@"Data", "Tables", $"{Romname}-triggers.json");
            var triggerList = LoadTriggers(fullPath);

            //Convert the json to actual effects
            triggerList.ForEach(trigger =>
            {
                triggersDict[trigger.command] = trigger;
            });
        }

        private void populateTargets()
        {
            var fullPath = Path.Combine(@"Data", $"targets.json");
            var targetList = LoadTargets(fullPath);

            //Convert the json to actual effects
            targetList.ForEach(target =>
            {
                switch (target.kind)
                {
                    case "LED":
                        targetsDict[target.id] = new LEDTarget(target);
                        break;
                    case "PIXEL":
                        targetsDict[target.id] = new PixelTarget(target);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
        }

        public void LoadTableInformation(string Romname)
        {
            this.Romname = Romname;
            var fullPath = Path.Combine(@"Data", "Tables", $"{Romname}.json");

            effectsDict.Clear();
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

            // TODO Can't continue unless we have effects and triggers.
            populateEffects();
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
                        // Make sure effects and targets still exist for all the controlItems.
                        // If they don't, clear it and disable the item
                        Effect effect = getEffect(item.effectString);
                        Models.Target target = getTarget(item.targetString);

                        if (effect == null)
                        {
                            item.effectString = null;
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

                    var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    commandQueue.Add(string.Format("VPX START 1 {0}", now));

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

        private List<TargetSerializer> LoadTargets(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TargetSerializer>>(json);
            }
        }

        private List<Models.JSONSerializer.EffectSerializer> LoadEffect(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Models.JSONSerializer.EffectSerializer>>(json);
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
