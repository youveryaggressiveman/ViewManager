using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.PcFeatures.Management
{
    public class PcManager
    {
        public string LoadPcFeature()
        {
            BasicInfo myInfo = new BasicInfo
            {
                OsVersion = Environment.OSVersion.ToString(),
                Os64 = Environment.Is64BitOperatingSystem,
                PcName = Environment.MachineName,
                NumberOfCpus = Environment.ProcessorCount,
                WindowsFolder = Environment.SystemDirectory,
                LogicalDrives = Environment.GetLogicalDrives()
            };

            return myInfo.ToString();
        }
    }
}
