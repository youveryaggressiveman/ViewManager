using GeneralLogic.Services.Files;
using GeneralLogic.Services.Settings;
using ServerApp.Assets.Custom.ComputerInfoBox;
using ServerApp.Assets.Custom.ListAllowAppBox;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Settings;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IFileManager _fileManager;

        private readonly UniversalController<User> _userController;
        private readonly CheckSettings _checkSettings;

        private List<string> _localIpList;
        private List<string> _themeList;
        private List<string> _languageList;

        private Visibility _visibility = Visibility.Collapsed;

        private string _selectedTheme;
        private string _selectedLanguage;
        private string _port;
        private string _selectedLocalIp;

        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }

        public string SelectedLocalIp
        {
            get => _selectedLocalIp;
            set
            {
                _selectedLocalIp = value;
                OnPropertyChanged(nameof(SelectedLocalIp));
            }
        }

        public List<string> LocalIpList
        {
            get => _localIpList;
            set
            {
                _localIpList = value;
                OnPropertyChanged(nameof(LocalIpList));
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility= value;
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

        public List<string> LanguageList
        {
            get => _languageList;
            set
            {
                _languageList = value;
                OnPropertyChanged(nameof(LanguageList));
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

        public ICommand OpenListAppCommand { get; }
        public ICommand CheckPcFeaturesCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public SettingsPageViewModel()
        {
            OpenListAppCommand = new DelegateCommand(OpenListApp);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            CheckPcFeaturesCommand = new DelegateCommand(CheckPcFeatures);

            _userController = new UniversalController<User>(ApiServerSingleton.GetConnectionApiString());
            _settingsManager = new SettingsManager();
            _fileManager = new PcFeaturesFileManager();

            _checkSettings = new();

            foreach (var tcp in TcpServerSingleton.GetLocalIp())
            {
                LocalIpList = tcp.ValueList;
            }

            SelectedLocalIp = TcpServerSingleton.GetIp();

            Port = TcpServerSingleton.GetPort().ToString();

            ThemeList = new List<string>()
            {
                "Light",
                "Dark"
            };
            LanguageList = new List<string>()
            {
                "English",
                "Русский"
            };

            CheckUserRole();
            LoadInfo();
        }

        private async void OpenListApp(object obj)
        {
            string message = string.Empty;

            try
            {
                await CustomListAllowAppBox.Show();

                message = "Approved applications have been successfully added.";
            }
            catch 
            {
                message = "An error occurred while adding the application.";
            }
            finally
            {
                LogManager.SaveLog("Server", DateTime.Today, $"TeacherMode: {message}");
            }
        }

        private async void CheckPcFeatures(object obj)
        {
            try
            {
                var pcInfo = await _fileManager.FileReader(Environment.MachineName);

                CustomComputerInfoBox.Show(Environment.MachineName, pcInfo.Replace("Hardware: {0}, ", "").Replace("Sensor: {0}, value: {1}, ", ""));
            }
            catch
            {
                CustomMessageBox.Show("Could not get data about your computer!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
        }

        private async void CheckUserRole()
        {
            try
            {
                var user = await _userController.Get(AuthUserSingleton.AuthUser.Id);

                if (user == null)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                if (user.RoleId == 1)
                {
                    Visibility = Visibility.Visible;
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }

            
        }

        private void LoadInfo()
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

        private void SaveChanges(object obj)
        {
            if (string.IsNullOrEmpty(Port))
            {
                CustomMessageBox.Show("The field with the input of the port cannot be empty!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            int port = int.Parse(Port);

            if (port <= 1023)
            {
                CustomMessageBox.Show("This port is reserved by the system.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                Settings.Default.ThemeName = SelectedTheme;
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {
                Settings.Default.LanguageName = _checkSettings.CheckCulture(SelectedLanguage);
            }


            Settings.Default.Port = port;
            TcpServerSingleton.SetPort(port);

            //Доделать
            //TcpServerSingleton.SetIp();

            Settings.Default.Save();

            if (CustomMessageBox.Show("In order for the some changes to apply, restart the application?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
            { 
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location;
                Info.WindowStyle = ProcessWindowStyle.Normal;
                Info.CreateNoWindow = true;
                Info.FileName = "ServerApp.exe";
                Process.Start(Info);
                Process.GetCurrentProcess().Kill();
            }
        }

    }
}
