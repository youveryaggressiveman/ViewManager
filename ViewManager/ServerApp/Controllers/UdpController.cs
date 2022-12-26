using ServerApp.Core.Screen;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ServerApp.Controllers
{
    public class UdpController
    {
        private int _countErorr = 0;

        private readonly string _ip;
        private readonly int _port;
        private readonly string _clientIp;
        private readonly int _clientPort;

        private Thread _start;

        private static Socket s_udpSocketClient = null;

        public UdpController(string ip, int port, string clientIp, int clientPort)
        {
            _ip = ip;
            _port = port;
            _clientIp = clientIp;

            _start = new Thread(StartUdp);
            _clientPort = clientPort;
        }

        public void Start()
        {
            _start.Start();
        }

        private async void StartUdp(object? obj)
        {
            var remoteIp = new IPEndPoint(IPAddress.Parse(_ip), _port + 1);
            EndPoint localIp = new IPEndPoint(IPAddress.Any, _port + 1);

            s_udpSocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s_udpSocketClient.Bind(localIp);

            while (true)
            {
                if(s_udpSocketClient == null)
                {
                    return;
                }

                try
                {
                    using (MemoryStream memoryStream = new())
                    {
                        byte[] data = new byte[65507];

                        await s_udpSocketClient.ReceiveFromAsync(data, SocketFlags.None, remoteIp);

                        memoryStream.Write(data, 2, data.Length - 2);

                        int countMsg = data[0] - 1;

                        if (countMsg > 10)
                        {
                            throw new Exception("Потеря первого пакета");
                        };

                        byte[] image = new byte[65507];

                        for (int i = 0; i < countMsg; i++)
                        {
                            await s_udpSocketClient.ReceiveFromAsync(image, SocketFlags.None, remoteIp);
                            memoryStream.Write(image, 0, image.Length);
                        }

                        ToScreenConverter.Convert(memoryStream.ToArray());
                    }
                }
                catch
                {
                    _countErorr++;
                }
            }

        }

        public void StopUdp()
        {

            s_udpSocketClient.Close();
            s_udpSocketClient.Dispose();

            s_udpSocketClient = null;
        }
    }
}
