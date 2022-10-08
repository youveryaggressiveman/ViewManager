using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Core.Singleton
{
    public static class ServerSingleton
    {
        private static readonly int s_port = 2003;
        private static readonly string s_server = "127.0.0.1";

        public static string GetServer() =>
            s_server;

        public static int GetPort() =>
            s_port;
    }
}
