using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class ConnectedClientSingleton
    {
        public static ObservableCollection<ConnectedClient> ListConnectedClient { get; set; } = null;
    }
}
