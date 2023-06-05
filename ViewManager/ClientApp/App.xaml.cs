using ClientApp.Controllers;
using ClientApp.Core.Singleton;
using ClientApp.Properties;
using GeneralLogic.Services.Files;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModelController _controller;

        protected override void OnStartup(StartupEventArgs e)
        {
            var lang = Settings.Default.LanguageName;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            GeneralLogic.Services.PcFeatures.TaskManager.Dispatcher.SetAutoRunValue(true, Assembly.GetExecutingAssembly().Location.Replace("dll", "exe"), "ClientApp");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SendLastMessage();

            base.OnExit(e);
        }

        private async void SendLastMessage()
        {
            try
            {
                _controller = new(ServerSingleton.GetServerPort(), ServerSingleton.GetServerIp());

                await _controller.SendMessage($"{Environment.MachineName}: The app was turned off");
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Client", DateTime.Today, $"SendApp: {ex.Message}.");
            }
            
        }
    }
}
