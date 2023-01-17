using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Clients
{
    public class ClientsSort
    {
        private readonly IFileManager _fileManager;

        public ClientsSort()
        {
            _fileManager = new AppStatisticsFileManager();
        }

        public async Task Sort(ConnectedClient connectedClient = null)
        {
            if (connectedClient == null)
            {
                var allClients = await _fileManager.FileReader("Clients");
                var clientList = allClients.Replace("\r", string.Empty).Split("\n");

                foreach (var client in clientList)
                {
                    if(client != string.Empty)
                    {
                        var addClient = client.Split("1652456");

                        ConnectedClientSingleton.S_ListConnectedClient.Add(new ConnectedClient()
                        {
                            Name = addClient[0],
                            Ip = addClient[1],
                            Port = int.Parse(addClient[2]),
                            Status = addClient[3]
                        });
                    }
                }
            }
            else
            {
                foreach (var client in ConnectedClientSingleton.S_ListConnectedClient)
                {
                    if(client.Name == connectedClient.Name)
                    {
                        client.Status = "Connected";

                        return;
                    }
                }

                ConnectedClientSingleton.S_ListConnectedClient.Add(connectedClient);
            }
        }
    }
}
