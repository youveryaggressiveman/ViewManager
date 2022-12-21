using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerApp.Core.Clients;

namespace ServerApp.Controllers
{
    public class TcpController
    {
        private readonly int _port;
        private readonly string _server;

        private readonly ClientsSort _clientsSort;

        private static TcpListener s_listener = null;

        public static ObservableCollection<string> S_AnswerList { get; set; } = new ObservableCollection<string>();

        public TcpController(int port, string server)
        {
            _port = port;
            _server = server;

            _clientsSort= new ClientsSort();
        }


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
                LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {ex.Message}.");
            }
            finally
            {
                if (s_listener != null)
                    s_listener.Stop();
            }

        }

        public async void GetDataTcp(object? obj)
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
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    switch (message[0])
                    {
                        case 'N':

                            IPEndPoint ipep = (IPEndPoint)client.Client.RemoteEndPoint;

                            ConnectedClient connectedClient = new()
                            {
                                Ip = ipep.Address.ToString(),
                                Port = ipep.Port,
                                Name = message.Remove(0, 6),
                                Status = "Connected"
                            };
                            
                            await _clientsSort.Sort(connectedClient);

                            LogManager.SaveLog("Server", DateTime.Today, "TcpClient: " + connectedClient.Name + ": Successful connection to the server.");
                            break;
                        case 'C':
                            LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {message}.");
                            break;
                        case 'A':
                            S_AnswerList.Add(message);

                            LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {message}.");
                            break;
                        default:
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {ex.Message}.");
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
