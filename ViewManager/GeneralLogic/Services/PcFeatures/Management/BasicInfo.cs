using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.PcFeatures.Management
{
    public class BasicInfo
    {
        public string OsVersion { get; set; } = null;
        public bool Os64 { get; set; }
        public string PcName { get; set; } = null;
        public int NumberOfCpus { get; set; }
        public string WindowsFolder { get; set; } = null;
        public string[] LogicalDrives { get; set; } = null;

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendFormat("Version of Windows: {0}\n", OsVersion);
            output.AppendFormat("Which is bit operation system?: {0}\n", Os64 ? "64 bit" : "32 bit");
            output.AppendFormat("Computer name: {0}\n", PcName);
            output.AppendFormat("CPUs number: {0}\n", NumberOfCpus);
            output.AppendFormat("System folder: {0}\n", WindowsFolder);
            output.AppendFormat("Logical drivers: {0}\n",
                  String.Join(", ", LogicalDrives)
               .Replace("\\", String.Empty));
            return output.ToString();
        }
    }
}
