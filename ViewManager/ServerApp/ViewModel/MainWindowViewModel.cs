using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ServerApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;

        private readonly Visitor _visitor;

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
            LogManager.CreateMainFolder();

            _fileManager= new PcFeaturesFileManager();

            _visitor = new();

            Timer timer = new Timer(5000);
            timer.Elapsed += async (sender, e) => await CheckAllConnection();
            timer.Start();

            FileWork();
        }

        private async void FileWork()
        {
            string message = string.Empty;

            try
            {
                await _fileManager.FileWriter(_visitor.Monitor(), "Server");

                message = "The file with the characteristics has been filled in successfully";
            }
            catch
            {
                message = "An error occurred when filling in the character file";
            }
            finally
            {
                LogManager.SaveLog("Client", DateTime.Today, $"FileWriter: {message}.");
            }

        }

        private async Task CheckAllConnection()
        {
            if (await ApiServerSingleton.CheckConnection())
            {
                LoadBorder(false);
            }
            else
            {
                LoadBorder(true);
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
