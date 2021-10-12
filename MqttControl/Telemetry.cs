using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttControl
{
    public class Telemetry
    {
        public string CpuTemp { get; set; }
        public string CpuUsage { get; set; }
        public string UsedMemory { get; set; }
        public string FreeMemory { get; set; }

        public List<HDD> Drives { get; set; }

        public Telemetry()
        {
            this.Drives = new List<HDD>();
        }
    }
}
