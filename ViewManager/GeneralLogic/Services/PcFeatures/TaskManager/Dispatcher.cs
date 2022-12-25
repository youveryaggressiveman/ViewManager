using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;

namespace GeneralLogic.Services.PcFeatures.TaskManager
{
    public class Dispatcher
    {
        public static ObservableCollection<string> S_AnswerList= new ObservableCollection<string>();
            
        public ObservableCollection<string> ProcessList = new ObservableCollection<string>(); 

        public Dispatcher()
        {
            System.Timers.Timer timer = new(5000);
            timer.Elapsed += (sender, e) => GetIncludeApps();
            timer.Start();
        }

        public void GetIncludeApps()
        {
            S_AnswerList = new ObservableCollection<string>();

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            if(ProcessList.Count== 0)
            {
                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.MainWindowTitle != string.Empty)
                    {
                        ProcessList.Add(process.MainWindowTitle);
                        S_AnswerList.Add(process.MainWindowTitle);
                    }

                }
            }
            else
            {
                foreach (var processNow in ProcessList)
                {
                    foreach (var process in processList)
                    {
                        if(process.MainWindowTitle != string.Empty && process.MainWindowTitle == processNow)
                        {

                        }
                    }
                }
            }

            return;
        }
    }
}
