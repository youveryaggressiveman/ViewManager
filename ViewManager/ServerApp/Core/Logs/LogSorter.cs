using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Logs
{
    public class LogSorter
    {
        public IEnumerable<double> SortLog(string allLogs)
        {
            var logList = allLogs.Split(';');


            foreach (var log in logList)
            {
                
            }

            return null;
        }
    }
}
