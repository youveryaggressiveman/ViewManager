using GeneralLogic.Services.Files;
using Newtonsoft.Json.Linq;
using ServerApp.Assets.Custom.ClientScreenBox;
using ServerApp.Assets.Custom.ComputerInfoBox;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core.Clients;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ServerApp.ViewModel
{
    public class ComputerManagementPageViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;
        private readonly ClientsSort _clientSort;

        private ConnectedClient _selectedConnectedClient;

        private ObservableCollection<ConnectedClient> _connectedClientList;

        private bool _isEnabled = false;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled= value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

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
            _clientSort = new();

            ConnectedClientList = ConnectedClientSingleton.S_ListConnectedClient;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            dispatcherTimer.Tick += (object? sender, EventArgs e) =>
            {
                ConnectedClientList = LoadImage(ConnectedClientSingleton.S_ListConnectedClient);
                CheckConnectedPc();
            };
            dispatcherTimer.Start();

            InfoCommand = new DelegateCommand(Info);
            BroadcastCommand = new DelegateCommand(Broadcast);
            TurnOffCommand = new DelegateCommand(TurnOff);

            CheckConnectedPc();
        }

        private void CheckConnectedPc()
        {
            if (ConnectedClientList.Count == 0)
            {
                IsEnabled= false;
            }
            else
            {
                IsEnabled= true;
            }
        }

        private ObservableCollection<ConnectedClient> LoadImage(ObservableCollection<ConnectedClient> connectedClients)
        {
            foreach (var client in connectedClients)
            {
                if(client.Status == "Connected")
                {
                   client.Image = _clientSort.GetImage("PcReady");
                }
                else
                {
                   client.Image = _clientSort.GetImage("PcSleep");
                }
            }
            return connectedClients;
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

                    CustomClientScreenBox.Show(SelectedConnectedClient);
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
