using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System.Collections.ObjectModel;

namespace ServerApp.Controllers
{
    public class MainWindowViewModelController
    {
        private readonly int _port;
        private readonly string _server;
        private static TcpListener s_listener = null;

        public static ObservableCollection<string> S_AnswerList { get; set; } = new ObservableCollection<string>();

        public MainWindowViewModelController(int port, string server) =>
            (_port, _server) = (port, server);


        public async Task StartTcp()
        {
            try
            {
                s_listener = new TcpListener(IPAddress.Parse(_server), _port);
                s_listener.Start();

                while (true)
                {
                    TcpClient client = await s_listener.AcceptTcpClientAsync();
                    
                    Thread clientThread = new Thread(new ParameterizedThreadStart(GetDataTcp));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("TcpClient: ", DateTime.Today, ex.Message);
            }
            finally
            {
                if (s_listener != null)
                    s_listener.Stop();
            }

        }

        public void GetDataTcp(object? obj)
        {
            var client = (TcpClient)obj;

            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    switch (message[0])
                    {
                        case 'I':
                            ConnectedClient connectedClient = new()
                            {
                                Id = message.Split(";")[0].Remove(0, 4)
                            };
                            ConnectedClientSingleton.ListConnectedClient.Add(connectedClient);

                            LogManager.SaveLog("TcpClient: ", DateTime.Today, message);
                                break;
                        case 'Q':
                            LogManager.SaveLog("TcpClient: ", DateTime.Today, message);
                                break;
                        case 'A':
                            S_AnswerList.Add(message);

                            LogManager.SaveLog("TcpClient: ", DateTime.Today, message);
                                break;
                        default:
                                break;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("TcpClient: ", DateTime.Today, ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
    }
}
