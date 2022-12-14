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
using System.Windows.Markup;

namespace ClientApp.Controllers
{
    public class MainWindowViewModelController
    {
        private readonly int _port;
        private readonly string _server;

        private static TcpListener s_listener = null;
        private static TcpClient s_tcpClient;

        public MainWindowViewModelController(int port, string server) =>
            (_port, _server) = (port, server);

        public async Task<int> StartListenerTcp()
        {
            NetworkStream stream = null;
            s_tcpClient = new TcpClient(_server, _port);

            try
            {
                IPEndPoint ipep = (IPEndPoint)s_tcpClient.Client.LocalEndPoint;

                s_listener = new TcpListener(ipep.Address,2003);

                s_listener.Start();

                while (true)
                {
                    TcpClient client = s_listener.AcceptTcpClient();

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

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + "Command to execute: " + command);

                    return command;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + ex.Message);

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

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + "The command is executed");

                    return true;
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + ex.Message);

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
                using (s_tcpClient = new TcpClient(_server, _port))
                {

                    stream = s_tcpClient.GetStream();

                    var message = "Name: " + Environment.MachineName;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + "Successful connection to the server");

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, "TcpClient: " + ex.Message);

                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

        }
    }
}
