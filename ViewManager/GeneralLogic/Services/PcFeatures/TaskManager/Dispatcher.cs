using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

namespace GeneralLogic.Services.PcFeatures.TaskManager
{
    public class Dispatcher
    {
        private List<string> _answerList;
        private List<string> _processList;

        public Dispatcher()
        {
            _processList = new List<string>();
            _answerList = new List<string>();
        }

        public bool TryKillProcess(string name)
        {
            var processList = System.Diagnostics.Process.GetProcesses().Where(proc => proc.ProcessName == name);

            int count = 0;

            if (processList == null)
            {
                return false;
            }

            foreach (System.Diagnostics.Process process in processList)
            {
                process.Kill();
            }

            return true;
        }

        public Task<List<string>> GetIncludeApps()
        {
            _processList = new List<string>();

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process process in processList)
            {
                try
                {
                    if (!process.MainModule.FileName.Contains("Windows"))
                    {
                        _processList.Add(process.ProcessName);
                    }
                }
                catch
                {

                }
            }

            var set = new HashSet<string>(_processList);

            if (set.SetEquals(_answerList))
            {
                return Task.FromResult(new List<string>());
            }
            else
            {
                _answerList.AddRange(_processList);
                var allprocessList = _answerList.Distinct().ToList();
                _answerList = allprocessList;

                return Task.FromResult(allprocessList);
            }
        }

        public static bool SetAutoRunValue(bool autoRun, string path, string name)
        {
            RegistryKey reg;

            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            try
            {
                if (autoRun)
                    reg.SetValue(name, path);
                else
                    reg.DeleteValue(name);
            }
            catch
            {
                return false;
            }
            finally
            {
                reg.Flush();
                reg.Close();
            }

            return true;
        }
    }
}
