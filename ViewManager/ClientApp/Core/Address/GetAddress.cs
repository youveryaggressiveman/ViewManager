using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Core.Address
{
    public class GetAddress
    {
        public ObservableCollection<string> GetLocalIp()
        {
            var ipList = new ObservableCollection<string>();

            var macAddresses = NetworkInterface.GetAllNetworkInterfaces()
                                               .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                                                        && x.OperationalStatus == OperationalStatus.Up)
                                                            .ToList();
            foreach (var item in macAddresses)
            {
                foreach (var address in item.GetIPProperties().UnicastAddresses.ToList())
                {
                    if(address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipList.Add(address.Address.ToString());
                    } 
                }
            }

            return ipList;
        }

        private IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public bool IsInSameSubnet(IPAddress addressServer, IPAddress addressClient, IPAddress subnetMask)
        {
            try
            {
                IPAddress network1 = GetNetworkAddress(addressClient, subnetMask);
                IPAddress network2 = GetNetworkAddress(addressServer, subnetMask);

                return network1.Equals(network2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
