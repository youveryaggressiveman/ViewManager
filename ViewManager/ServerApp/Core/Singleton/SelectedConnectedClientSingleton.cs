using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class SelectedConnectedClientSingleton
    {
        public static ConnectedClient ConnectedClient { get; set; }
    }
}
