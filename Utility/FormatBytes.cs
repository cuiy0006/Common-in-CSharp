using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class FormatBytes
    {
        public static string FormatBytesMethod(long bytes)
        {
            string[] magnitudes = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(1024, magnitudes.Length);

            return string.Format("{1:##:##} {0}",
                magnitudes.FirstOrDefault(magnitude => bytes > (max/=1024))??"0 Bytes", ((decimal)bytes/(decimal)max)).Trim();
        }
    }
}
