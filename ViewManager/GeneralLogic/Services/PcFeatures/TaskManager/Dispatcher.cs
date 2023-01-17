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
        private List<string> _answerList;
        private ObservableCollection<string> _processList;

        public Dispatcher()
        {
            _processList = new ObservableCollection<string>();
            _answerList = new List<string>();
        }

        public bool TryKillProcess(string name)
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process process in processList)
            {
                if(process.MainWindowTitle == name)
                {
                    process.Kill();
                    return true;
                }
            }

            return false;
        }

        public Task<List<string>> GetIncludeApps()
        {
            _processList = new ObservableCollection<string>();

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.MainWindowTitle != string.Empty)
                {
                    _processList.Add(process.MainWindowTitle);
                }
            }

            if(_processList.SequenceEqual(_answerList))
            {
                return null;
            }
            else
            {
                _answerList.AddRange(_processList);
                var allprocessList = _answerList.Distinct().ToList();
                _answerList = allprocessList;

                return Task.FromResult(allprocessList);
            }
        }
    }
}
