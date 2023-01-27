﻿using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServerApp.Core.Clients
{
    public class ClientsSort
    {
        private readonly IFileManager _fileManager;

        public ClientsSort()
        {
            _fileManager = new AppStatisticsFileManager();
        }

        public BitmapImage GetImage(string value)
        {
            DirectoryInfo directoryInfo = new(@"../../../Assets/Images/");

            foreach (var image in directoryInfo.GetFiles())
            {
                if (image.Name == value + ".png")
                {
                    return new BitmapImage(new Uri(image.FullName));
                }
            }

            return null;
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
                        client.Port = connectedClient.Port;

                        return;
                    }
                }

                ConnectedClientSingleton.S_ListConnectedClient.Add(connectedClient);
            }
        }
    }
}
