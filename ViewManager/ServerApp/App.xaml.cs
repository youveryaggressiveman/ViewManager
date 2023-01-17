using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using ServerApp.Properties;
using ServerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var lang = Settings.Default.LanguageName;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            IFileManager fileManager = new AppStatisticsFileManager();

            SaveClient(fileManager);
            SaveStat(fileManager);

            base.OnExit(e);
        }

        private async void SaveStat(IFileManager fileManager)
        {
            var allAppStat = string.Empty;

            foreach (var stat in AppStatSingleton.S_ListAppStat)
            {
                if (stat.Title == "Verified")
                {
                    allAppStat += "Verified: ";
                }

                allAppStat += $"{stat.ProcessName}, Client: {stat.ClientName}\n";
            }

            await fileManager.FileWriter("Statistics", allAppStat);
        }

        private async void SaveClient(IFileManager fileManager)
        {
            string clientList = string.Empty;
            string key = "1652456";

            foreach (var client in ConnectedClientSingleton.S_ListConnectedClient)
            {
                clientList += client.Name + key;
                clientList += client.Ip + key;
                clientList += client.Port + key;
                clientList += "Disconnected" + "\n";
            }

            await fileManager.FileWriter("Clients", clientList);
        }
    }
}
