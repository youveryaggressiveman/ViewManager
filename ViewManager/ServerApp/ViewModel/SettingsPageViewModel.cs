using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Core.Settings;
using ServerApp.Core.Singleton;
using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<string> _themeList;
        private List<string> _languageList;

        private string _selectedTheme;
        private string _selectedLanguage;
        private string _port;
        private string _ip;

        public string Port
        {
            get => _port;
            set
            {
                _port= value;
                OnPropertyChanged(nameof(Port));
            }
        }

        public string Ip
        {
            get=> _ip;
            set
            {
                _ip = value;
                OnPropertyChanged(nameof(Ip));
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
            get=>_selectedTheme;
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

        public SettingsPageViewModel()
        {
            SaveChangesCommand = new DelegateCommand(SaveChanges);

            _settingsManager = new SettingsManager();

            Port = TcpServerSingleton.GetPort().ToString();
            Ip = TcpServerSingleton.GetIp();

            ThemeList = new List<string>()
            {
                "Light",
                "Dark"
            };
            LanguageList = new List<string>();

            LoadInfo();
        }

        private void LoadInfo()
        {
            if (ThemeList.Contains(Settings.Default.ThemeName))
            {
                SelectedTheme = Settings.Default.ThemeName;
            }

            if (ThemeList.Contains(Settings.Default.LanguageName))
            {
                SelectedTheme = Settings.Default.LanguageName;
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

            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                Settings.Default.ThemeName = SelectedTheme;
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {
                Settings.Default.LanguageName = SelectedLanguage;
            }

            if (port > 1023 && port<=65535)
            {
                Settings.Default.Port = port;
                TcpServerSingleton.SetPort(port);

                if(CustomMessageBox.Show("In order for the network changes to apply, restart the application?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
                {
                    //Перезапускать приложение
                }
            }

            Settings.Default.Save();
        }

    }
}
