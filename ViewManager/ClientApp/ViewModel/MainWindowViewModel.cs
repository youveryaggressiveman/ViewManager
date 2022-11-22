using ClientApp.Controllers;
using ClientApp.Core.Singleton;
using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ClientApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly FileManager _fileManager;
        private readonly PcManager _pcManager;
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
            _controller = new MainWindowViewModelController(ServerSingleton.GetPort(), ServerSingleton.GetServer());

            Timer timer = new Timer(5000);
            timer.Elapsed += async (sender, e) => await CheckConnection();
            timer.Start();

            LogManager.CreateMainFolder();

            _pcManager = new PcManager();
            _fileManager = new FileManager();

            FileWork();
            StartTcp();
        }

        private async void FileWork()
        {  
            await _fileManager.FileWriter(_pcManager.LoadPcFeature(), Environment.MachineName);

            LogManager.SaveLog("Client", DateTime.Today, "FileWriter: The file with the characteristics has been filled in successfully");
        }

        private async void StartTcp()
        {
            await _controller.StartListenerTcp();
        }

        private async Task CheckConnection()
        {
            LoadBorder(true);

            if (await _controller.SendFirstMessageTcp())
            {
                LoadBorder(false);
            }  
        }

        private void LoadBorder(bool switchBorder)
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
