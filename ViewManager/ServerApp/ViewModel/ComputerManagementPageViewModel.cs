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
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace ServerApp.ViewModel
{
    public class ComputerManagementPageViewModel : BaseViewModel
    {
        private readonly ClientsSort _clientSort;

        private ConnectedClient _selectedConnectedClient;

        private ObservableCollection<ConnectedClient> _connectedClientList;

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

                SelectedConnectedClientSingleton.ConnectedClient = _selectedConnectedClient;
            }
        }

        public ComputerManagementPageViewModel()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    var mainPage = ((window as MainWindow).mainFrame.Content as Page).DataContext as MainPageViewModel;

                    mainPage.VisibilityPc = Visibility.Visible;
                }
            }

            _clientSort = new();

            ConnectedClientList = LoadImage(ConnectedClientSingleton.S_ListConnectedClient);

            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sender, e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    ConnectedClientList = LoadImage(ConnectedClientSingleton.S_ListConnectedClient);
                });

            };
            timer.Start();
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

    }
}
