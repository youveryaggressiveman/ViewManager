using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class ListAllowAppSingleton
    {
        public static ObservableCollection<string> S_AllowAppList = new ObservableCollection<string>();
    }
}
