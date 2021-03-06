﻿using Pin80Server.CommandProcessors;
using Pin80Server.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Pin80Server
{
    internal static class Server
    {
        private static MainForm mainForm;
        private static TcpListener server = null;
        private static HttpListener listener = null;

        private static BlockingCollection<string> commandQueue = new BlockingCollection<string>();

        private static readonly SerialPort serial = new SerialPort("COM3"); // TODO make this a setting

        private static readonly DataProcessor dataProcessor = new DataProcessor();

        private static readonly VPXProcessor vpxProcessor = new VPXProcessor(dataProcessor, serial);
        private static readonly PBYProcessor vbyProcessor = new PBYProcessor(dataProcessor, serial);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string ip = "127.0.0.1";

            int tcpPort = 2012;
            int httpPort = 8012;

            string url = string.Format("http://{0}:{1}/", ip, httpPort);
            IPAddress localAddr = IPAddress.Parse(ip);

            //TODO what happens if this fails, should we try again?
            server = new TcpListener(localAddr, tcpPort);
            server.Start();
            Debug.WriteLine("Listening for TCP connections on {0}", tcpPort);

            /* Start http server too */
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Debug.WriteLine("Listening for http connections on {0}", url);

            // TCP Connection Listener
            Thread tcpthread = new Thread(HandleIncomingTCPConnections)
            {
                IsBackground = true
            };
            tcpthread.Start();

            // HTTP Connection Listener
            Thread httpthread = new Thread(HandleIncomingHttpConnections)
            {
                IsBackground = true
            };
            httpthread.Start();

            // Command Processor Thread
            Thread p = new Thread(HandleCommands)
            {
                IsBackground = true
            };
            p.Start();

            try
            {
                // TODO close this port when the windows closes
                serial.Open();
            }
            catch (IOException ex)
            {
                // TODO handle this, what now?
                Debug.WriteLine(ex);
            }

            mainForm = new MainForm();
            //mainForm.setDataProcessor(dataProcessor);
            mainForm.setQueueRef(ref commandQueue);

            dataProcessor.SetMainForm(mainForm);
            dataProcessor.SetQueueRef(ref commandQueue);

            vpxProcessor.setMainForm(mainForm);
            vbyProcessor.setMainForm(mainForm);

            mainForm.Shown += MainForm_Shown;
            Application.Run(mainForm);
        }

        private static void MainForm_Shown(object sender, EventArgs e)
        {
            mainForm.setDataProcessor(dataProcessor);
        }

        public static void HandleIncomingHttpConnections()
        {
            //TODO add try block around this
            while (true)
            {
                // Will wait here until we hear from a connection
                Debug.WriteLine("Waiting for http connection...");
                HttpListenerContext ctx = listener.GetContext();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Debug.WriteLine(req.Url.ToString());

                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/"))
                {
                    var command = req.QueryString.Get("cmd");
                    if (command != null)
                    {
                        commandQueue.Add(command);
                        Console.WriteLine(string.Format("Got command {0}", command));
                    }
                }
                // TODO add json as a valid input to open up more options

                byte[] data = Encoding.UTF8.GetBytes("ok");
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                resp.OutputStream.Write(data, 0, data.Length);
                resp.Close();
            }
        }

        public static void HandleIncomingTCPConnections()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("TCP client connected!");
                    Thread tcpClientThread = new Thread(new ParameterizedThreadStart(HandleTCPClint))
                    {
                        IsBackground = true
                    };
                    tcpClientThread.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                //server.Stop(); //TODO What do we do in this case?
            }
        }

        public static void HandleTCPClint(object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string data;
            byte[] bytes = new byte[256];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i).Trim();

                    if (data.Length > 0)
                    {
                        // Split the data by newline since multiple commands can come in at once
                        string[] lines = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            commandQueue.Add(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("TCP Client Exception: {0}", e.ToString());
                client.Close();
            }
        }

        public static void HandleCommands()
        {

            List<EffectInstance> runningActions = new List<EffectInstance>();
            long now;

            while (true)
            {
                now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (commandQueue.TryTake(out string cmd))
                {
                    string[] commandParts = cmd.Split(' ');
                    string source = commandParts[0];

                    if (mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke((MethodInvoker)delegate ()
                        {
                            mainForm.addLogEntry(cmd);
                        });
                    }

                    // PinballY 
                    if (source.StartsWith("PBY"))
                    {
                        vbyProcessor.processCommand(cmd, now);
                    }
                    // Virutal Pinball X
                    else if (source == "VPX")
                    {
                        string command = string.Join(" ", commandParts.Skip(1)); // Don't care about the source
                        var list = vpxProcessor.processCommand(command, now);
                        if (list != null)
                        {
                            runningActions.AddRange(list);
                        }
                    }
                    else
                    {
                        if (mainForm.IsHandleCreated)
                        {
                            mainForm.BeginInvoke((MethodInvoker)delegate ()
                            {
                                mainForm.addLogEntry(string.Format("ERR Unknown command: {0}", cmd));
                            });
                        }
                    }
                }

                // Process targets
                foreach (EffectInstance ta in runningActions.ToList())
                {
                    if (now >= ta.nextUpdate)
                    {
                        if (!ta.effect.Tick(ta, now))
                        {
                            runningActions.Remove(ta);
                        }
                    }
                }

                foreach (Models.Target target in dataProcessor.targetsDict.Values.ToList())
                {
                    if (target.hasUpdate)
                    {
                        target.Run(serial);
                    }
                }
            }
        }
    }
}
