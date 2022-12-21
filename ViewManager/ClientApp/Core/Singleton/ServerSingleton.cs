
using ClientApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Core.Singleton
{
    public static class ServerSingleton
    {
        private static int s_serverPort = Properties.Settings.Default.ServerPort;
        private static string s_serverIp = Properties.Settings.Default.ServerIp;

        public static string GetServerIp() => 
            s_serverIp;

        public static int GetServerPort() =>
           s_serverPort;

        public static void SetServerPort(int port) =>
            (s_serverPort) = (port);

        public static void SetServerIp(string ip) =>
            (s_serverIp) = (ip);
    }
}
