using GeneralLogic.Services.Files;
using GeneralLogic.Services.Translator;
using Microsoft.Win32;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
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
        private UniversalController<Role> _roleController;
        private UniversalController<Office> _officeCintroller;
        private UniversalController<Specialization> _specializtionController;

        private List<IEnumerable<object>> _listData;

        protected async override void OnStartup(StartupEventArgs e)
        {
            _listData = new List<IEnumerable<object>>();

            _roleController = new UniversalController<Role>(ApiServerSingleton.GetConnectionApiString());
            _officeCintroller = new UniversalController<Office>(ApiServerSingleton.GetConnectionApiString());
            _specializtionController = new UniversalController<Specialization>(ApiServerSingleton.GetConnectionApiString());

            _listData.Add(await _roleController.GetList());
            _listData.Add(await _officeCintroller.GetList());
            _listData.Add(await _specializtionController.GetList());

            foreach (var data in _listData)
            {
               
            }

            var lang = Settings.Default.LanguageName;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            //var tet = await TranslatorController.GetTranslation("Hello", "en", "ru");

            GeneralLogic.Services.PcFeatures.TaskManager.Dispatcher.SetAutoRunValue(true, Assembly.GetExecutingAssembly().Location.Replace("dll", "exe"), "ServerApp");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
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
