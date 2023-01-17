using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class AppStatSingleton
    {
        public static ObservableCollection<ServerApp.Model.Statistics> S_ListAppStat { get; set; } = new ObservableCollection<ServerApp.Model.Statistics>();
    }
}
