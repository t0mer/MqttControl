using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace MqttControl
{
    public class SysInfo
    {
        public string CpuTemp { get; private set; }
        public string CpuUsage { get; private set; }

        public string UsedMemory { get; private set; }
        public string FreeMemory { get; set; }

        /// <summary>
        /// Add drive info to the published data
        /// </summary>
        public bool publishDriveInfo { get; set; }

        public string TotalMemory
        {
            get
            {
                try
                {
                    return (Convert.ToDouble(UsedMemory) + Convert.ToDouble(FreeMemory)).ToString();
                }
                catch
                {
                    return "0";
                }
            }
        }

        public Telemetry Get()
        {
            Telemetry telemetry = new Telemetry();
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.RAMEnabled = true;
            computer.HDDEnabled = true;
            computer.FanControllerEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                        {
                            if (computer.Hardware[i].Sensors[j].Name == "CPU Package")
                                telemetry.CpuTemp = computer.Hardware[i].Sensors[j].Value.ToString();
                        }

                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                        {
                            if (computer.Hardware[i].Sensors[j].Name == "CPU Total")
                                telemetry.CpuUsage = computer.Hardware[i].Sensors[j].Value.ToString();
                        }
                    }
                }

                if (computer.Hardware[i].HardwareType == HardwareType.RAM)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        //
                        if (computer.Hardware[i].Sensors[j].Name == "Available Memory")
                            telemetry.FreeMemory = computer.Hardware[i].Sensors[j].Value.ToString();

                        if (computer.Hardware[i].Sensors[j].Name == "Used Memory")
                            telemetry.UsedMemory = computer.Hardware[i].Sensors[j].Value.ToString();
                    }
                }

            }
            computer.Close();
            if (publishDriveInfo)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType != DriveType.CDRom && drive.IsReady)
                    {
                        HDD hdd = new HDD();
                        hdd.Name = drive.Name;
                        hdd.VolumeLabel = drive.VolumeLabel;
                        hdd.DriveType = drive.DriveType.ToString();
                        hdd.FileSystem = drive.DriveFormat;
                        hdd.TotalSize = Utils.ToFileSizeApi(drive.TotalSize);
                        hdd.FreeSpace = Utils.ToFileSizeApi(drive.TotalFreeSpace);
                        hdd.UsedSpace = Utils.ToFileSizeApi(drive.TotalSize - drive.TotalFreeSpace);
                        telemetry.Drives.Add(hdd);
                        hdd = null;
                    }
                }
            }

            return telemetry;
        }

        internal void Test()
        {
        }
    }
}