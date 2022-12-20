using ClientApp.Model;
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
        private static int s_thisPort = Properties.Settings.Default.YourPort;
        private static readonly string s_thisIp = Properties.Settings.Default.YourIp;

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

        public static string SetIp(string value)
        {
            if (value == string.Empty && s_thisIp == string.Empty)
            {
                var listIp = GetLocalIp();

                foreach (var ip in listIp)
                {
                    Properties.Settings.Default.YourIp = ip.ValueList[0];
                    break;
                }
            }
            else if (value != string.Empty && s_thisIp != value)
            {
                Properties.Settings.Default.YourIp = value;
            }

            Properties.Settings.Default.Save();

            return Properties.Settings.Default.YourIp;
        }

        public static ObservableCollection<LocalIpModel> GetLocalIp()
        {
            var ipList = new ObservableCollection<LocalIpModel>();

            var macAddresses = NetworkInterface.GetAllNetworkInterfaces()
                                               .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                                                        && x.OperationalStatus == OperationalStatus.Up)
                                                            .ToList();
            foreach (var item in macAddresses)
            {
                ipList.Add(new LocalIpModel()
                {
                    Name = item.Name,
                    IpAddresses = item.GetIPProperties().UnicastAddresses.ToList()

                });
            }

            return ipList;
        }
    }
}
