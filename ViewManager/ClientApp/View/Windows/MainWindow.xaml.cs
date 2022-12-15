using ClientApp.Core.Settings;
using ClientApp.Properties;
using ClientApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        private readonly ISettingsManager _settingsManager;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel = new MainWindowViewModel();

            if (!string.IsNullOrEmpty(Settings.Default.ThemeName))
            {
                _settingsManager.SetTheme(Settings.Default.ThemeName);
            }
        }

        private void Port_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text.ToString();
            Regex inputRegex = new Regex(@"^[0-9]*$");
            Match match = inputRegex.Match(input);

            if (!match.Success || (sender as TextBox).Text.Length >= 5)
            {
                e.Handled = true;
            }
        }
    }
}
