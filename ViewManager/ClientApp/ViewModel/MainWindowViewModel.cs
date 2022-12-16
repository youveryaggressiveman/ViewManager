using ClientApp.Assets.Custom.MessageBox;
using ClientApp.Command;
using ClientApp.Controllers;
using ClientApp.Core.Settings;
using ClientApp.Core.Singleton;
using ClientApp.Properties;
using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.Management;
using GeneralLogic.Services.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;

        private readonly CheckSettings _checkSettings;
        private readonly FileManager _fileManager;
        private readonly PcManager _pcManager;
        private readonly MainWindowViewModelController _controller;

        private List<string> _themeList;
        private List<string> _languageList;

        private string _status;
        private string _yourIp;
        private string _yourPort;
        private string _serverIp;
        private string _serverPort;
        private string _selectedTheme;
        private string _selectedLanguage;

        private Visibility _visibility = Visibility.Collapsed;

        public List<string> LanguageList
        {
            get => _languageList;
            set
            {
                _languageList = value;
                OnPropertyChanged(nameof(LanguageList));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status= value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string YourIp
        {
            get => _yourIp;
            set
            {
                _yourIp= value;
                OnPropertyChanged(nameof(YourIp));
            }
        }

        public string YourPort
        {
            get=> _yourPort;
            set
            {
                _yourPort = value;
                OnPropertyChanged(nameof(YourPort));
            }
        }

        public string ServerIp
        {
            get=> _serverIp;
            set
            {
                _serverIp= value;
                OnPropertyChanged(nameof(ServerIp));
            }
        }

        public string ServerPort
        {
            get => _serverPort; 
            set
            {
                _serverPort = value;
                OnPropertyChanged(nameof(ServerPort));
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public List<string> ThemeList
        {
            get => _themeList;
            set
            {
                _themeList = value;
                OnPropertyChanged(nameof(ThemeList));
            }
        }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));

                _settingsManager.SetTheme(value);
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand CheckConnectionCommand { get; }

        public MainWindowViewModel()
        {
            YourIp = ServerSingleton.GetThisIp();
            YourPort = ServerSingleton.GetThisPort().ToString();

            ServerIp = ServerSingleton.GetServerIp();
            ServerPort = ServerSingleton.GetServerPort().ToString();

            _controller = new MainWindowViewModelController(int.Parse(ServerPort), ServerIp);

            SaveChangesCommand = new DelegateCommand(SaveChanges);
            CheckConnectionCommand = new DelegateCommand(CheckConnection);

            LogManager.CreateMainFolder();

            LanguageList = new List<string>()
            {
                "English",
                "Русский"
            };
            ThemeList = new List<string>()
            {
                "Light",
                "Dark"
            };

            _checkSettings = new CheckSettings();
            _settingsManager = new SettingsManager();
            _pcManager = new PcManager();
            _fileManager = new FileManager();

            LoadInfo();
            FileWork();
            StartTcp();
            CheckConnection(null);
        }

        private void SaveChanges(object obj)
        {
            if (string.IsNullOrEmpty(ServerPort) || string.IsNullOrEmpty(YourPort) || string.IsNullOrEmpty(ServerIp))
            {
                CustomMessageBox.Show("The fields for entering data about the local network cannot be empty!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            int serverPort = int.Parse(ServerPort);
            int yourPort = int.Parse(YourPort);

            if (serverPort <= 1023 || yourPort <= 1023)
            {
                CustomMessageBox.Show("This port is reserved by the system.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            if (!string.IsNullOrEmpty(SelectedTheme))
            {;
                Settings.Default.ThemeName = SelectedTheme;
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {;
                Settings.Default.LanguageName = _checkSettings.CheckCulture(SelectedLanguage);
            }

            Settings.Default.YourPort = yourPort;
            ServerSingleton.SetThisPort(yourPort);

            Settings.Default.ServerPort = serverPort;
            ServerSingleton.SetServerPort(serverPort);

            Settings.Default.ServerIp = ServerIp;
            ServerSingleton.SetServerIp(ServerIp);

            Settings.Default.Save();

            if (CustomMessageBox.Show("In order for the some changes to apply, restart the application?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
            {
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"";
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.FileName = "cmd.exe";
                Process.Start(Info);
                Process.GetCurrentProcess().Kill();
            }
        }

        public void LoadInfo()
        {
            var lang = _checkSettings.CheckLang(Settings.Default.LanguageName);

            if (ThemeList.Contains(Settings.Default.ThemeName))
            {
                SelectedTheme = Settings.Default.ThemeName;
            }

            if (LanguageList.Contains(lang))
            {
                SelectedLanguage = lang;
            }
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

        private async void CheckConnection(object obj)
        {
            LoadBorder(true);

            if (await _controller.SendFirstMessageTcp())
            {
                Status = "Connected";
            }
            else
            {
                Status = "Disconnected";

                CustomMessageBox.Show("Connection with this ID and port does not exist.", Assets.Custom.MessageBox.Basic.Titles.Error, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }

            LoadBorder(false);
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
