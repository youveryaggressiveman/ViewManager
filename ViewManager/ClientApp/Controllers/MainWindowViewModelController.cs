using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Controllers
{
    public class MainWindowViewModelController
    {
        private readonly int _port;
        private readonly string _server;
        private static TcpListener s_listener = null;

        public MainWindowViewModelController(int port, string server) =>
            (_port, _server) = (port, server);
    }
}
