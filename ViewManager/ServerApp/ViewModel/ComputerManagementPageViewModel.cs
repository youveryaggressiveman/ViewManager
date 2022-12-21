﻿using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.ClientScreenBox;
using ServerApp.Assets.Custom.ComputerInfoBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class ComputerManagementPageViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;

        private ObservableCollection<ConnectedClient> _connectedClientList;
        private ConnectedClient _selectedConnectedClient;

        public ObservableCollection<ConnectedClient> ConnectedClientList
        {
            get => _connectedClientList;
            set
            {
                _connectedClientList = value;
                OnPropertyChanged(nameof(ConnectedClientList));
            }
        }

        public ConnectedClient SelectedConnectedClient
        {
            get => _selectedConnectedClient;
            set
            {
                _selectedConnectedClient = value;
                OnPropertyChanged(nameof(SelectedConnectedClient));
            }
        }

        public ICommand InfoCommand { get; }
        public ICommand BroadcastCommand { get; }
        public ICommand TurnOffCommand { get; }

        public ComputerManagementPageViewModel()
        {
            _fileManager = new PcFeaturesFileManager();

            InfoCommand = new DelegateCommand(Info);
            BroadcastCommand = new DelegateCommand(Broadcast);
            TurnOffCommand = new DelegateCommand(TurnOff);

            ConnectedClientList = ConnectedClientSingleton.ListConnectedClient;
        }

        private async void TurnOff(object obj)
        {
            if (SelectedConnectedClient != null)
            {
                await TcpController.SendMessage(SelectedConnectedClient, "3");
            }
        }

        private void Broadcast(object obj)
        {
            if (SelectedConnectedClient != null)
            {
                CustomClientScreenBox customClientScreenBox = new(SelectedConnectedClient.Name);
                customClientScreenBox.ShowDialog();
            }
        }

        private async void Info(object obj)
        {
            if (SelectedConnectedClient != null)
            {
                string desription = string.Empty;

                try
                {
                    desription = await _fileManager.FileReader(SelectedConnectedClient.Name);
                }
                catch
                {

                }
                finally
                {
                    CustomComputerInfoBox.Show(SelectedConnectedClient.Name, desription);
                }
            }
        }
    }
}
