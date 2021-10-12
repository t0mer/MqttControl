using MessageBoxExample;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttControl
{
    public partial class Form1 : Form
    {


        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        Timer updateTimer = new Timer();
        MqttClient client = new MqttClient(ConfigurationManager.AppSettings["mqttserver"]);
        string DeviceName = ConfigurationManager.AppSettings["devicename"];
        string ShutdownTopic, RestartTopic, LogoffTopic, ExecuteTopic, TelemetryTopic, CancelTopic, MessageTopic, SnapshotTopic, LockTopic, TestTopic;
        string TempDir = Path.Combine(Application.StartupPath, "Temp");
        bool autoSnapshot = Convert.ToBoolean(ConfigurationManager.AppSettings["autoSnapshot"]);
        bool publishDriveInfo = Convert.ToBoolean(ConfigurationManager.AppSettings["publishDriveInfo"]);
        bool debugMode = Convert.ToBoolean(ConfigurationManager.AppSettings["debugMode"]);
        string debugFile = Path.Combine(Application.StartupPath, "debug.json");


        public Form1()
        {
            if (!Directory.Exists(TempDir))
                Directory.CreateDirectory(TempDir);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Interval = int.Parse(ConfigurationManager.AppSettings["updateinterval"]) * 1000; ;
            updateTimer.Enabled = true;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.ConnectionClosed += Client_ConnectionClosed;
            this.ShutdownTopic = "/" + DeviceName + "/shutdown/";
            this.RestartTopic = "/" + DeviceName + "/restart/";
            this.LogoffTopic = "/" + DeviceName + "/logoff/";
            this.LockTopic = "/" + DeviceName + "/lock/";
            this.TestTopic = "/" + DeviceName + "/test/";
            this.CancelTopic = "/" + DeviceName + "/cancel/";
            this.ExecuteTopic = "/" + DeviceName + "/execute/";
            this.TelemetryTopic = "/" + DeviceName + "/telemetry";
            this.MessageTopic = "/" + DeviceName + "/message/";
            this.SnapshotTopic = "/" + DeviceName + "/snapshot/";
            //UpdateTimer_Tick(this, new EventArgs());
          
            Connect();

            this.HeartBeat();

            InitializeComponent();
            try
            {

            }
            catch (Exception ex)
            {
                var m = ex;
            }

        }

        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            try
            {
                byte code = client.Connect(Guid.NewGuid().ToString(), ConfigurationManager.AppSettings["mqttuser"], ConfigurationManager.AppSettings["mqttpass"]);
                Subscribe();
            }
            catch (Exception ex)
            {

            }
        }

        private void Subscribe()
        {
           
            ushort msgId = client.Subscribe(new string[] { ShutdownTopic, RestartTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            msgId = client.Subscribe(new string[] { LogoffTopic, ExecuteTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            msgId = client.Subscribe(new string[] { LockTopic, TestTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            msgId = client.Subscribe(new string[] { MessageTopic, "#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
          

        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {


                if (e.Topic == ShutdownTopic)
                    this.Shutdown(Encoding.Default.GetString(e.Message));

                if (e.Topic == RestartTopic)
                    this.Restart(Encoding.Default.GetString(e.Message));

                if (e.Topic == LogoffTopic)
                    this.Logoff(Encoding.Default.GetString(e.Message));

                if (e.Topic == CancelTopic)
                    this.Cancel(Encoding.Default.GetString(e.Message));

                if (e.Topic == ExecuteTopic)
                    this.ShowMessage(Encoding.Default.GetString(e.Message));

                if (e.Topic == MessageTopic)
                    this.ShowMessage(Encoding.Default.GetString(e.Message));

                if (e.Topic == SnapshotTopic)
                    this.TakeSnaspshot();

                if (e.Topic == LockTopic)
                    this.LockComputer();
            }
            catch
            {

            }
        }

        private void Cancel(string args)
        {
            //this.ExecuteShutDown("-a");
        }

        private void Shutdown(string args)
        {
            string cmdline = "-s " + args;
            ExecuteShutDown(cmdline);
        }

        private void Restart(string args)
        {
            string cmdline = "-r " + args;
            ExecuteShutDown(cmdline);
        }

        private void Logoff(string args)
        {
            string cmdline = "-l " + args;
            //ExecuteShutDown(cmdline);
        }
        private void LockComputer()
        {
            try
            {
               // LockWorkStation();
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            }
            catch(Exception ex)
            {

            }
        }
        private void restartServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            updateTimer.Enabled = false;
            this.StatusIcon.Dispose();
            this.Dispose();
            Application.Restart();

        }

        private void exitServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Unsubscribe(new string[] { ShutdownTopic, RestartTopic });
            client.Unsubscribe(new string[] { LogoffTopic, ExecuteTopic });
            client.Unsubscribe(new string[] { MessageTopic, SnapshotTopic });
            client.Disconnect();
            updateTimer.Enabled = false;
            this.StatusIcon.Dispose();
            this.Dispose();
            Application.ExitThread();
            Application.Exit();
            Environment.Exit(0);


        }

        private void runAtStarupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("MQTTControl", System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        private void disableRunAnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.DeleteValue("MQTTControl", false);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.IsConnected)
                this.client.Disconnect();
        }

        private void MQTTControlMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SysInfo i = new SysInfo();
            i.Test();
        }

      

        private void ShowMessage(string message)
        {
            //this.StatusIcon.Text = "";
            this.StatusIcon.BalloonTipText = "HA";
            this.StatusIcon.ShowBalloonTip(5000, "Home Assistant", message, ToolTipIcon.Info);
            MyMessageBox.ShowBox(message, "Message From Home Assistant");
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {

        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {

            SysInfo sysinfo = new SysInfo();
            sysinfo.publishDriveInfo = this.publishDriveInfo;
            Telemetry telemetry = sysinfo.Get();

            string Telemetry = JsonConvert.SerializeObject(telemetry).ToLower();
            if (debugMode)
            {
                StreamWriter debugger = new StreamWriter(debugFile, false, Encoding.UTF8);
                debugger.Write(Telemetry);
                debugger.Flush();
                debugger.Dispose();
            }
            this.publish(TelemetryTopic, Telemetry);
            if (autoSnapshot)
                TakeSnaspshot();
        }

        private void HeartBeat()
        {
            SysInfo sysinfo = new SysInfo();
            sysinfo.publishDriveInfo = this.publishDriveInfo;
            Telemetry telemetry = sysinfo.Get();

            string Telemetry = JsonConvert.SerializeObject(telemetry).ToLower();
            if (debugMode)
            {
                StreamWriter debugger = new StreamWriter(debugFile, false, Encoding.UTF8);
                debugger.Write(Telemetry);
                debugger.Flush();
                debugger.Dispose();
            }
            this.publish(TelemetryTopic, Telemetry);
            TakeSnaspshot();
        }

        private void publish(string Topic, string Data)
        {
            if (!client.IsConnected)
                Connect();
            ushort msgId = client.Publish(Topic, Encoding.UTF8.GetBytes(Data), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        private void publishImage(string Topic, byte[] Data)
        {
            if (!client.IsConnected)
                Connect();
            ushort msgId = client.Publish(Topic, Data, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        private void ExecuteShutDown(string arguments)
        {
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo();
            proc.StartInfo.FileName = "shutdown.exe";
            proc.StartInfo.Arguments = arguments;
            proc.Start();
        }


        private void TakeSnaspshot()
        {
            Console.WriteLine("Initializing the variables...");
            Console.WriteLine();
            Bitmap memoryImage;
            memoryImage = new Bitmap(1920, 1080);
            Size s = new Size(memoryImage.Width, memoryImage.Height);

            // Create graphics 
            Console.WriteLine("Creating Graphics...");
            Console.WriteLine();
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);

            // Copy data from screen 
            Console.WriteLine("Copying data from screen...");
            Console.WriteLine();
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            //That's it! Save the image in the directory and this will work like charm. 
            string str = "";
            try
            {
                str = Path.Combine(TempDir, "Screenshot.png");
            }
            catch (Exception er)
            {
            }

            // Save it! 
            memoryImage.Save(str);
            publishImage(SnapshotTopic + "image/", File.ReadAllBytes(str));
            // Write the message, 

        }

    }



}
