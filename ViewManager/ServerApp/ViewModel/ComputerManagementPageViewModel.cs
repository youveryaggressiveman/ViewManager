﻿using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.ClientScreenBox;
using ServerApp.Assets.Custom.ComputerInfoBox;
using ServerApp.Assets.Custom.MessageBox;
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
using System.Timers;
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

            Timer timer = new Timer(3000);
            timer.Elapsed += (sender, e) => AddNewClientList();
            timer.Start();
        }

        private void AddNewClientList()
        {
            ConnectedClientList = ConnectedClientSingleton.ListConnectedClient;
        }

        private async void TurnOff(object obj)
        {
            if (SelectedConnectedClient != null && SelectedConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClient, "3");
                }
                catch
                {
                    CustomMessageBox.Show("The selected PC could not be turned off.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
                
            }
        }

        private async void Broadcast(object obj)
        {
            if (SelectedConnectedClient != null && SelectedConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClient, "2");

                    CustomClientScreenBox customClientScreenBox = new(SelectedConnectedClient);
                    customClientScreenBox.ShowDialog();
                }
                catch
                {
                    CustomMessageBox.Show("Could not enable the broadcast of the selected client.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
                
            }
        }

        private async void Info(object obj)
        {
            if (SelectedConnectedClient != null && SelectedConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClient, "1");

                    CustomComputerInfoBox.Show(string.Empty, SelectedConnectedClient.Name);
                }
                catch
                {
                    CustomMessageBox.Show("Could not get information about the selected PC.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }
        }
    }
}
