using ClientApp.Core.Screen;
using GeneralLogic.Services.Files;
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

        private ScreenConverter _screenConverter;
        private UdpClient _udpClient;

        private static TcpListener s_listener = null;
        private static TcpClient s_tcpClient;

        public MainWindowViewModelController(int port, string server) =>
            (_port, _server) = (port, server);

        public async Task<int> StartListenerTcp()
        {
            NetworkStream stream = null;

            try
            {
                s_tcpClient = new TcpClient(_server, _port);

                IPEndPoint ipep = (IPEndPoint)s_tcpClient.Client.LocalEndPoint;

                s_listener = new TcpListener(ipep.Address, _port);

                s_listener.Start();

                while (true)
                {
                    await s_tcpClient.ConnectAsync(_server, _port);

                    TcpClient client = await s_listener.AcceptTcpClientAsync();

                    stream = client.GetStream();

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
            finally
            {
                if (s_listener != null)
                    s_listener.Stop();
            }

        }

        public async Task<bool> SendMessage(string text)
        {
            NetworkStream stream = null;
            try
            {
                stream = s_tcpClient.GetStream();

                var message = "Command: " + text;

                byte[] data = Encoding.UTF8.GetBytes(message);

                await stream.WriteAsync(data, 0, data.Length);

                LogManager.SaveLog("Client", DateTime.Today, "TcpClient: The command is executed.");

                return true;
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

        public async Task<bool> SendFirstMessageTcp(string ip, int port)
        {
            NetworkStream stream = null;
            try
            {
                using (s_tcpClient = new TcpClient(ip, port))
                {

                    stream = s_tcpClient.GetStream();

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
