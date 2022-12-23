using ClientApp.Core.Address;
using ClientApp.Core.Screen;
using GeneralLogic.Services.Files;
using HidSharp.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace ClientApp.Controllers
{
    public class MainWindowViewModelController
    {
        private int _port;
        private string _server;

        private ScreenConverter _screenConverter;
        private UdpClient _udpClient;

        private readonly GetAddress _getAddress;
        private readonly GetSubnetMusk _getSubnetMusk;

        private static Socket s_socketServer = null;
        private static Socket s_socketClient = null;

        public MainWindowViewModelController(int port, string server)
        {
            _port = port;
            _server = server;

            _getAddress = new();
            _getSubnetMusk = new();

            s_socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                if (!GetIpAddress())
                {
                    throw new Exception("The IP of the server that you specified is registered in a different subnet!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool GetIpAddress()
        {
            try
            {
                var serverIp = IPAddress.Parse(_server);
                var mask = _getSubnetMusk.CreateByNetBitLength(24);
                IPAddress clientIp;

                foreach (var address in _getAddress.GetLocalIp())
                {
                    clientIp = IPAddress.Parse(address);
                    if (_getAddress.IsInSameSubnet(serverIp, clientIp, mask))
                    {
                        EndPoint ipPoint = new IPEndPoint(clientIp, _port);

                        s_socketServer.Bind(ipPoint);
                        s_socketServer.Listen(1000);

                        return true;
                    }

                }

                return false;
            }
            catch (Exception)
            {
                throw new Exception("The IP of the server you specified does not exist!");
            }
           
        }

        public async Task<int> StartListenerTcp()
        {
            NetworkStream stream = null;

            try
            {
                while (true)
                {
                    Socket client = await s_socketServer.AcceptAsync();

                    stream = new(client);

                    byte[] data = new byte[256];
                    var result = new StringBuilder();

                    do
                    {
                        int bytes = await stream.ReadAsync(data, 0, data.Length);
                        result.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    stream.Close();
                    client.Close();

                    var command = int.Parse(result.Remove(0, 9).ToString());

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + $"Command to execute: {command}.");

                    return command;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"TcpClient: {ex.Message};");

                return 0;
            }
        }

        public async Task SendMessage(string text)
        {
            NetworkStream stream = null;

            try
            {
                if (!GetIpAddress())
                {
                    throw new Exception("The IP of the server that you specified is registered in a different subnet!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                using (s_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse(_server), _port);

                    await s_socketClient.ConnectAsync(remotePoint);
                    stream = new(s_socketClient);

                    var message = "Answer: " + text;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: The command is executed.");
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"TcpClient: {ex.Message}.");

                throw new Exception("Connection with this ID and port does not exist!");
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public async Task SendFirstMessageTcp(string ip, int port)
        {
            NetworkStream stream = null;

            try
            {
                if (!GetIpAddress())
                {
                    throw new Exception("The IP of the server that you specified is registered in a different subnet!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                using (s_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);

                    await s_socketClient.ConnectAsync(remotePoint);
                    stream = new(s_socketClient);

                    var message = "Name: " + Environment.MachineName;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    _server = ip;
                    _port = port;

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: Successful connection to the server.");
                }


            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"TcpClient: {ex.Message}.");

                throw new Exception("Connection with this ID and port does not exist!");
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

        }

        public async Task StartUdp()
        {
            _udpClient = new(_server, _port);

            _screenConverter = new ScreenConverter(int.Parse(System.Windows.SystemParameters.PrimaryScreenWidth.ToString()),
                int.Parse(System.Windows.SystemParameters.PrimaryScreenHeight.ToString()));

            while (true)
            {
                var byteArrayList = _screenConverter.CutMsg(_screenConverter.Convert());

                for (int i = 0; i < byteArrayList.Count; i++)
                {

                    await _udpClient.SendAsync(byteArrayList[i], byteArrayList[i].Length);
                }
            }
        }

        public void StopUdp()
        {
            _udpClient.Close();
        }
    }
}
