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
using System.Net.Mail;
using System.Security.Policy;
using ServerApp.Assets.Custom.ComputerInfoBox;

namespace ServerApp.Controllers
{
    public static class TcpController
    {
        private static ObservableCollection<string> s_answerList = new ObservableCollection<string>();

        private static readonly int s_port = TcpServerSingleton.GetPort();
        private static readonly string s_server = TcpServerSingleton.GetIp();

        private static readonly ClientsSort s_clientsSort = new ClientsSort();

        private static Socket s_socketServer = null;
        private static Socket s_socketClient = null;

        public static ObservableCollection<string> S_AnswerList
        {
            get => s_answerList;
            set
            {
                s_answerList = value;

                if (s_answerList != null || s_answerList.Count > 0)
                {
                    CustomComputerInfoBox.Show(s_answerList[0]);
                }
            }
        }

        public static async Task StartTcp()
        {
            try
            {
                EndPoint ipPoint = new IPEndPoint(IPAddress.Parse(s_server), s_port);

                s_socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                s_socketServer.Bind(ipPoint);
                s_socketServer.Listen(1000);

                while (true)
                {

                    Socket client = await s_socketServer.AcceptAsync();

                    NetworkStream stream = null;
                    try
                    {
                        stream = new(client);
                        byte[] data = new byte[512];

                        StringBuilder builder = new StringBuilder();
                        do
                        {
                            int bytes = await stream.ReadAsync(data, 0, data.Length);
                            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);

                        string message = builder.ToString();

                        switch (message[0])
                        {
                            case 'N':

                                IPEndPoint ipep = (IPEndPoint)client.RemoteEndPoint;

                                if (message.Contains($"Shutdown command completed successfully"))
                                {
                                    foreach (var connectedClient in ConnectedClientSingleton.ListConnectedClient)
                                    {
                                        if (message.Contains(connectedClient.Name))
                                        {
                                            connectedClient.Status = "Disconnected";
                                        }
                                    }
                                }
                                else
                                {
                                    ConnectedClient connectedClient = new()
                                    {
                                        Ip = ipep.Address.ToString(),
                                        Port = ipep.Port,
                                        Name = message.Remove(0, 5),
                                        Status = "Connected"
                                    };

                                    await s_clientsSort.Sort(connectedClient);

                                    LogManager.SaveLog("Server", DateTime.Today, "TcpClient: " + connectedClient.Name + ": Successful connection to the server.");
                                }
                                break;
                            case 'A':

                                LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {message}.");

                                S_AnswerList.Add(message.Remove(0, 7));
                                break;
                            default:
                                break;
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
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"TcpClient: {ex.Message}.");
            }
        }

        public static async Task<bool> SendMessage(ConnectedClient client, string text)
        {
            NetworkStream stream = null;
            try
            {
                using (s_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse(client.Ip), s_port);
                    await s_socketClient.ConnectAsync(remotePoint);
                    stream = new(s_socketClient);

                    var message = "Command: " + text;

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogManager.SaveLog("Server", DateTime.Today, "TcpServer: The command is sent.");

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"TcpServer: {ex.Message}.");

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
