﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.PcFeatures.TaskManager
{
    public class Dispatcher
    {
        public string GetIncludeApps()
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();

            string result = string.Empty;

            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.MainWindowTitle != string.Empty)
                {
                    result += process.MainWindowTitle + "\n";
                }
                    
            }

            return result;
        }
    }
}