using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MqttControl
{
    public class Utils
    {


        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern Int32 StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);


        public static string ToFileSizeApi(long file_size)
        {
            StringBuilder sb = new StringBuilder(20);
            StrFormatByteSize(file_size, sb, 20);
            return sb.ToString();
        }


        public static string ToFileSize(double value)
        {
            string[] suffixes = { "bytes", "KB", "MB", "GB",
        "TB", "PB", "EB", "ZB", "YB"};
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value <= (Math.Pow(1024, i + 1)))
                {
                    return ThreeNonZeroDigits(value /
                        Math.Pow(1024, i)) +
                        " " + suffixes[i];
                }
            }

            return ThreeNonZeroDigits(value /
                Math.Pow(1024, suffixes.Length - 1)) +
                " " + suffixes[suffixes.Length - 1];
        }

        private static string ThreeNonZeroDigits(double value)
        {
            if (value >= 100)
            {
                // No digits after the decimal.
                return value.ToString("0,0");
            }
            else if (value >= 10)
            {
                // One digit after the decimal.
                return value.ToString("0.0");
            }
            else
            {
                // Two digits after the decimal.
                return value.ToString("0.00");
            }
        }
    }
}
