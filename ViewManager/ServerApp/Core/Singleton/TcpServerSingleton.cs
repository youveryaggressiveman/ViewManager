using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class TcpServerSingleton
    {
        private static int s_port = Properties.Settings.Default.Port;
        private static readonly string s_ip = SetIp();

        public static string GetIp() =>
            s_ip;

        public static int GetPort() => 
            s_port;

        public static void SetPort(int value) =>
            (s_port) = (value);

        private static string SetIp()
        {
            var host = Dns.GetHostName();

            return Dns.GetHostByName(host).AddressList[0].ToString();
        }
            
    }
}
