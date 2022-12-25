using ClientApp.Assets.Custom.ComputerInfoBox;
using ClientApp.Assets.Custom.MessageBox;
using ClientApp.Command;
using ClientApp.Controllers;
using ClientApp.Core.Settings;
using ClientApp.Core.Singleton;
using ClientApp.Properties;
using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib;
using GeneralLogic.Services.PcFeatures.Management;
using GeneralLogic.Services.Settings;
using HidSharp.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IFileManager _fileManager;

        private readonly Visitor _visitor;
        private readonly CheckSettings _checkSettings;
        private readonly PcManager _pcManager;

        private MainWindowViewModelController _controller;

        private List<string> _themeList;
        private List<string> _languageList;

        private string _status;
        private string _serverIp;
        private string _serverPort;
        private string _selectedTheme;
        private string _selectedLanguage;

        private Thread _execute;

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
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string ServerIp
        {
            get => _serverIp;
            set
            {
                _serverIp = value;
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

        public ICommand CheckPcFeaturesCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand CheckConnectionCommand { get; }

        public MainWindowViewModel()
        {
            ServerIp = ServerSingleton.GetServerIp();
            ServerPort = ServerSingleton.GetServerPort().ToString();

            CheckPcFeaturesCommand = new DelegateCommand(CheckPcFeatures);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            CheckConnectionCommand = new DelegateCommand(CheckConnection);

            try
            {
                _controller = new MainWindowViewModelController(int.Parse(ServerPort), ServerIp);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message, Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
            

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

            _visitor = new();
            _checkSettings = new();
            _pcManager = new();

            _settingsManager = new SettingsManager();
            _fileManager = new PcFeaturesFileManager();

            _execute = new Thread(ExecuteCommand);

            _execute.Start();

            CheckConnection(null);

            LoadInfo();
            FileWork();
        }

        private async void ExecuteCommand(object? obj)
        {
            if(_controller == null)
            {
                return;
            }

            try
            {
                while (true)
                {

                    var command = await _controller.StartListenerTcp();

                    switch (command)
                    {
                        case 1:

                            var pcInfo = _pcManager.LoadPcFeature();

                            await _controller.SendMessage(pcInfo);

                            break;
                        case 2:
                             _controller.Start();
                            break;
                        case 3:
                            await _controller.SendMessage($"{Environment.MachineName}: Shutdown command completed successfully");

                            System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 00");
                            break;
                        case 4:
                            _controller.StopUdp();
                            break;
                        default:
                            break;
                    }
                }

            }
            catch
            {
                Status = "Disconnected";
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

        private void SaveChanges(object obj)
        {
            if (string.IsNullOrEmpty(ServerPort) || string.IsNullOrEmpty(ServerIp))
            {
                CustomMessageBox.Show("The fields for entering data about the local network cannot be empty!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            int serverPort = int.Parse(ServerPort);

            if (serverPort <= 1023)
            {
                CustomMessageBox.Show("This port is reserved by the system.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                ;
                Settings.Default.ThemeName = SelectedTheme;
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {
                ;
                Settings.Default.LanguageName = _checkSettings.CheckCulture(SelectedLanguage);
            }


            Settings.Default.ServerPort = serverPort;
            ServerSingleton.SetServerPort(serverPort);

            Settings.Default.ServerIp = ServerIp;
            ServerSingleton.SetServerIp(ServerIp);

            Settings.Default.Save();

            if (CustomMessageBox.Show("The saves have been saved successfully! The application will be restarted.", Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing))
            {
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location;
                Info.WindowStyle = ProcessWindowStyle.Normal;
                Info.CreateNoWindow = true;
                Info.FileName = "ClientApp.exe";
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
            string message = string.Empty;

            try
            {
                await _fileManager.FileWriter(_visitor.Monitor(), Environment.MachineName);

                message = "The file with the characteristics has been filled in successfully";
            }
            catch
            {
                message = "An error occurred when filling in the character file.";
            }
            finally
            {
                LogManager.SaveLog("Client", DateTime.Today, $"FileWriter: {message}.");
            }

        }

        private async void CheckConnection(object? obj)
        {
            try
            {
                LoadBorder(true);

                if(_controller == null)
                {
                    throw new Exception("The IP of the server you specified does not exist!");
                }

                await _controller.SendFirstMessageTcp(ServerIp, int.Parse(ServerPort));

                CustomMessageBox.Show("The connection is established, for the full operation of the application, click the \"Save changes\" button.", Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                Status = "Connected";
            }
            catch (Exception ex)
            {
                Status = "Disconnected";

                CustomMessageBox.Show(ex.Message, Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

            }
            finally
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
