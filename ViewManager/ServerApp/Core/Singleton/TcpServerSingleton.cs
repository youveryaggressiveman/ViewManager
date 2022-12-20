using HarfBuzzSharp;
using ServerApp.Model;
using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class TcpServerSingleton
    {
        private static int s_port = Properties.Settings.Default.Port;
        private static readonly string s_ip = Properties.Settings.Default.Ip;

        public static string GetIp() =>
            s_ip;

        public static int GetPort() => 
            s_port;

        public static void SetPort(int value) =>
            (s_port) = (value);

        public static string SetIp(string value)
        {
            if(value == string.Empty && s_ip == string.Empty)
            {
                var listIp = GetLocalIp();

                foreach (var ip in listIp)
                {
                    Properties.Settings.Default.Ip = ip.ValueList[0];
                    break;
                }             
            }
            else if(value != string.Empty && s_ip != value)
            {
                Properties.Settings.Default.Ip = value;
            }

            Properties.Settings.Default.Save();

            return Properties.Settings.Default.Ip;
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
