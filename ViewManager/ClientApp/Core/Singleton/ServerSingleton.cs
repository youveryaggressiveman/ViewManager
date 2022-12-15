using ClientApp.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Core.Singleton
{
    public static class ServerSingleton
    {
        private static int s_thisPort = Properties.Settings.Default.YourPort;
        private static readonly string s_thisIp = SetIp();

        private static int s_serverPort = Properties.Settings.Default.ServerPort;
        private static string s_serverIp = Properties.Settings.Default.ServerIp;

        public static string GetThisIp() =>
            s_thisIp;

        public static int GetThisPort() =>
            s_thisPort;

        public static string GetServerIp() => 
            s_serverIp;

        public static int GetServerPort() =>
           s_serverPort;

        public static void SetThisPort(int port) =>
            (s_thisPort) = (port);

        public static void SetServerPort(int port) =>
            (s_serverPort) = (port);

        public static void SetServerIp(string ip) =>
            (s_serverIp) = (ip);

        private static string SetIp()
        {
            var host = Dns.GetHostName();

            return Dns.GetHostByName(host).AddressList[5].ToString();
        }
    }
}
