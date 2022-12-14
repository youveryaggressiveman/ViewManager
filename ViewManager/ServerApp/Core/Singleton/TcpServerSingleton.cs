using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class TcpServerSingleton
    {
        private static readonly int s_port = 2003;
        private static readonly string s_server = "10.200.31.7";

        public static string GetServer() =>
            s_server;

        public static int GetPort() => 
            s_port;
    }
}
