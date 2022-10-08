using GeneralLogic.Services.Files;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Timer = ServerApp.Core.Timer;

namespace ServerApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModelController _controller;

        private Visibility _visibility = Visibility.Collapsed;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public MainWindowViewModel()
        {
            _controller = new MainWindowViewModelController(TcpServerSingleton.GetPort(), TcpServerSingleton.GetServer());

            LogManager.CreateMainFolder();

            Timer.Run(CheckAllConnection, TimeSpan.FromSeconds(5));

            TcpConnect();
        }     

        private async void TcpConnect()
        {
            await _controller.StartTcp();
        }

        private async void CheckAllConnection()
        {
                LoadBorder(true);

                if (await ApiServerSingleton.CheckConnection())
                {
                    LoadBorder(false);
                }        
        }

        public void LoadBorder(bool switchBorder)
        {
            if (switchBorder)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
