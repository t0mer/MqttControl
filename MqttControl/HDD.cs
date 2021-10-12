using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttControl
{
    public class HDD
    {
        public string Name { get; set; }
        public string VolumeLabel { get; set; }
        public string DriveType { get; set; }
        public string TotalSize { get; set; }
        public string FreeSpace { get; set; }
        public string UsedSpace { get; set; }
        public string FileSystem { get; set; }

    }
}
