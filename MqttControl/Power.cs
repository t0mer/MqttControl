using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MqttControl
{
    public class Power
    {
        public static string BatteryChargeStatus()
        {
            return SystemInformation.PowerStatus.BatteryChargeStatus.ToString();
        }

        public static string BatteryFullLifetime()
        {
            return SystemInformation.PowerStatus.BatteryFullLifetime.ToString();
        }

        public static string BatteryLifePercent()
        {
            return SystemInformation.PowerStatus.BatteryLifePercent.ToString();
        }

        public static string BatteryLifeRemaining()
        {
            return SystemInformation.PowerStatus.BatteryLifeRemaining.ToString();
        }

        public static string PowerLineStatus()
        {
            return SystemInformation.PowerStatus.PowerLineStatus.ToString();
        }

        public static void Hibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }

        public static void Suspend()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        public static void Restart()
        {
            Process.Start("shutdown.exe", "-r -t 10");
        }

        public static void ShutDown()
        {
            Process.Start("shutdown.exe", "-s -t 10");
        }
    }
}
