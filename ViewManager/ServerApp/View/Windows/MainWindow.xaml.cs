using MaterialDesignThemes.Wpf;
using ServerApp.Core;
using ServerApp.Core.Settings;
using ServerApp.Properties;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISettingsManager _settingsManager;

        public MainWindow()
        {
            InitializeComponent();

            FrameManager.MainFrame = mainFrame;
            FrameManager.SetPage(new AuthPage(), "mainFrame");

            _settingsManager = new SettingsManager();

            if (!string.IsNullOrEmpty(Settings.Default.ThemeName))
            {
                _settingsManager.SetTheme(Settings.Default.ThemeName);
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) 
                Hide();

            base.OnStateChanged(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();

            base.OnClosing(e);
        }

        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
        }
    }
}
