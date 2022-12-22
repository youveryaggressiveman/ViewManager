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
        private readonly int _port;
        private readonly string _server;

        private EndPoint _remotePoint;
        private ScreenConverter _screenConverter;
        private UdpClient _udpClient;

        private static Socket s_socketServer = null;
        private static Socket s_socketClient = null;

        public MainWindowViewModelController(int port, string server, EndPoint remotePoint)
        {
            _port = port;
            _server = server;
            _remotePoint = remotePoint;

            s_socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            EndPoint ipPoint = new IPEndPoint(IPAddress.Broadcast, _port);

            s_socketServer.Bind(ipPoint);
            s_socketServer.Listen(1000);
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

        public async Task<bool> SendMessage(string text)
        {
            NetworkStream stream = null;
            try
            {
                using (s_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    await s_socketClient.ConnectAsync(_remotePoint);
                    stream = new(s_socketClient);

                    var message = "Answer: " + text;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: The command is executed.");

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"TcpClient: {ex.Message}.");

                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public async Task<bool> SendFirstMessageTcp()
        {
            NetworkStream stream = null;
            try
            {
                using (s_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    await s_socketClient.ConnectAsync(_remotePoint);
                    stream = new(s_socketClient);

                    var message = "Name: " + Environment.MachineName;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: Successful connection to the server.");

                    return true;
                }


            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"TcpClient: {ex.Message}.");

                return false;
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
