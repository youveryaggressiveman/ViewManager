using ServerApp.Command;
using ServerApp.Core.Settings;
using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                Settings.Default.ThemeName = SelectedTheme;
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {
                Settings.Default.LanguageName = SelectedLanguage;
            }

            Settings.Default.Save();
        }

    }
}
