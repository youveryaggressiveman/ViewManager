using ServerApp.Core.Screen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ServerApp.Controllers
{
    public class UdpController
    {
        private int _countErorr = 0;

        private readonly string _ip;
        private readonly int _port;

        private UdpClient _udpServer;

        public UdpController(string ip, int port) =>
            (_ip, _port) = (ip, port);

        public async Task StartUdp()
        {
            var localIP = new IPEndPoint(IPAddress.Parse(_ip), _port);

            _udpServer = new UdpClient(localIP);

            while (true)
            {
                if(_udpServer == null)
                {
                    break;
                }

                try
                {
                    using (MemoryStream memoryStream = new())
                    {
                        var result = await _udpServer.ReceiveAsync();

                        memoryStream.Write(result.Buffer, 2, result.Buffer.Length - 2);

                        int countMsg = result.Buffer[0] - 1;

                        if (countMsg > 10)
                        {
                            throw new Exception("Потеря первого пакета");
                        }

                        for (int i = 0; i < countMsg; i++)
                        {
                            var answer = await _udpServer.ReceiveAsync();
                            memoryStream.Write(answer.Buffer, 0, answer.Buffer.Length);
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
            
            _udpServer.Close();
            _udpServer.Dispose();

            _udpServer = null;
        }
    }
}
