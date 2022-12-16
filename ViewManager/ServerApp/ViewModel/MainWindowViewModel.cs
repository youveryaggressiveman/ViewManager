using GeneralLogic.Services.Files;
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

            Timer timer = new Timer(5000);
            timer.Elapsed += async (sender, e) => await CheckAllConnection();
            timer.Start();
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
