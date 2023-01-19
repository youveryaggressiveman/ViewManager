using ClientApp.Controllers;
using ClientApp.Core.Singleton;
using ClientApp.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _controller = new(ServerSingleton.GetServerPort(), ServerSingleton.GetServerIp());

            await _controller.SendMessage($"{Environment.MachineName}: The app was turned off");

            base.OnExit(e);
        }
    }
}
