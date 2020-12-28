using System;
using System.Collections.Generic;
using B2SServerPluginInterface;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Timers;

namespace Pin80Plugin
{
    [Export(typeof(IDirectPlugin))]

    public class Plugin : IDirectPlugin, IDirectPluginFrontend, IDirectPluginPinMame
    {
        TcpClient client;
        NetworkStream stream;
        private System.Timers.Timer aTimer;

        List<string> log = new List<string>();

        const bool DEBUG = false;

        string romName = null;
        string tableFilename;

        #region IDirectPlugin Members

        /// <summary>
        /// Gets the name of the plugin.<br/>
        /// When implmenting this property it is recommended to add the version of the plugin to the name as well.<br/> 
        /// The IDirectPlugin interface requires the implementation of the property.<br/>
        /// \remark If the code of this implementation of the property is reused, be sure to set the versioning information in AssemblyInfo.cs to something like [assembly: AssemblyVersion("1.0.*")]. Otherwise the BuildDate will not be correct.<br/>
        /// </summary>
        /// <value>
        /// The name of the IDirectPlugin.
        /// </value>
        public string Name
        {
            get
            {
                //Get the version of the assembly
                Version V = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //Calculate the BuildDate based in the build and revsion number of the project.
                DateTime BuildDate = new DateTime(2000, 1, 1).AddDays(V.Build).AddSeconds(V.Revision * 2);
                //Format and return the name string.
                return string.Format("Pin80Plugin (V: {0} as of {1})", V.ToString(), BuildDate.ToString("yyyy.MM.dd hh:mm"));
            }
        }

        /// <summary>
        /// Initializes the Plugin.<br/>
        /// This is the first method, which is called after the plugin has been instanciated by the B2S.Server.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        /// <param name="TableFilename">The table filename.</param>
        /// <param name="RomName">Name of the rom.</param>
        public void PluginInit(string TableFilename, string RomName)
        {
            logMessage("Plugin Init");
            romName = RomName;
            tableFilename = TableFilename;

            connect();

            if (client == null)
            {
                logMessage("Connect failed on startup");
            }
            SetTimer();
        }

        private  void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private  void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            logMessage("Timer Trigger");
            if (client == null || stream == null)
            {
                logMessage("Not connected, trying to connect...");
                connect();
            }
            else
            {
                logMessage(string.Format("Client connected {0}", client.Connected));
            }
        }

        private void logMessage(string msg)
        {
            if (DEBUG)
            {
                log.Add(msg);
            }
        }

        public bool connect()
        {
            logMessage("Connect called");

            try
            {
                //client = new TcpClient("127.0.0.1", 2012);
                //var client = new TcpClient();
                //if (!client.ConnectAsync("127.0.0.1", 2012).Wait(1000))
                //{
                //    return false;
                //}
                client = new TcpClient();
                var result = client.BeginConnect("127.0.0.1", 2012, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                if (!success)
                {
                    //throw new Exception("Failed to connect.");
                    return false;
                }

                stream = client.GetStream();

                // send info about table
                sendTableInformation();

                return true;
            }
            catch (SocketException e)
            {
                logMessage(String.Format("Connect failed {0}", e));
            }

            return false;
        }

        /// <summary>
        /// Finishes the plugin.<br />
        /// This is the last method called, before a plugin is discared. This method is also called, after a undhandled exception has occured in a plugin.<br/>
        /// PluginFinish must do all nessecary clean up work for the plugin (e.g. release resources).<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        public void PluginFinish()
        {
            stream.Close();
            client.Close();
        }

        /// <summary>
        /// This method is called, when new data from PinMame becomes available.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// \remark The special care when implementing to keep this method very fast! Slow implementations will slow down Visual Pinball, Pinmame, the B2S.Server as well as all other plugins. 
        /// \remark The best solution is to put the data in a queue and process the data in a separate thread.
        /// </summary>
        /// <param name="TableElementTypeChar">Char representing the table element type (S=Solenoid, W=Switch, L=Lamp, M=Mech, G=GI, E=EMTable, ?=Unknown table element).</param>
        /// <param name="Number">The number of the table element.</param>
        /// <param name="Value">The value of the table element.</param>
        public void DataReceive(char TableElementTypeChar, int Number, int Value)
        {
            if (client == null || stream == null)
            {
                //TODO - Limit how often we do this?
               
                logMessage("No connection, skipping");
                //connect();
                return;
            }

            // Ignore some codes we don't care about
            if (TableElementTypeChar.Equals('N') || Number < 0)
            {
                return;
            }

            var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string message = string.Format("VPX {0}{1} {2} {3}\n", TableElementTypeChar, Number, Value, now);

            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (SocketException e)
            {
                logMessage(String.Format("SocketException {0}", e));
                connect();
            }
            catch (Exception e)
            {
                logMessage(String.Format("Something is wrong {0}", e));
                connect();
            }
        }
        #endregion

        private void sendTableInformation()
        {
            try
            {
                string message = string.Format("VPX INFO ROM {0}\nVPX INFO TABLE {1}", romName, tableFilename);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (SocketException e)
            {
                logMessage(String.Format("SocketException {0}", e));
                connect();
            }
            catch (Exception e)
            {
                logMessage(String.Format("Something is wrong {0}", e));
                connect();
            }
        }

        #region IDirectPluginPinMame Members

        /// <summary>
        /// This method is called by the B2S.Server, when the Run method of PinMame gets called.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        public void PinMameRun()
        {
        }

        /// <summary>
        /// This method is called, when the property Pause of Pinmame gets set to true.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        public void PinMamePause()
        {
        }

        /// <summary>
        /// This method is called by the B2S.Server, when the property Pause of Pinmame gets set to false.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        public void PinMameContinue() { }


        /// <summary>
        /// This method is called by the B2S.Server, when the Stop method of Pinmame is called.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        public void PinMameStop() { }

        #endregion

        #region IDirectPluginFrontend Members

        /// <summary>
        /// PluginShowFrontend is called by the B2S.Server if a plugin has to show its frontend.<br />
        /// The IDirectPluginFrontend interface requires the implementation of this method.
        /// </summary>
        /// <param name="Owner">(optional) The owner window of the frontend to be opend.<br/>Make sure that your plugin does also support null for this parameter.</param>
        public void PluginShowFrontend(Form Owner = null)
        {
            //Open the frontend in this method, e.g. as demonstrated below.

            //Check if the frontend window is already open
            //If yes, bring it to the front and set focus
            foreach (Form F in Application.OpenForms)
            {
                if (F.GetType() == typeof(Form1))
                {
                    F.BringToFront();
                    F.Focus();

                    return;
                }
            }

            //If the frontend is not yet open, create a new instance and show it
            Form1 FE = new Form1();
            if (Owner == null)
            {
                //Owner para is not set.
                FE.Show();
            }
            else
            {
                //Owner para set set. Show frontend and pass owner para.
                FE.Show(Owner);
            }

            // TODO: This only shows up the first time the window is shown.
            string allLogs = String.Join("\n", log);
            (FE as Form1).updateLog(allLogs);
            log.Clear();
        }
        #endregion
    }
}
