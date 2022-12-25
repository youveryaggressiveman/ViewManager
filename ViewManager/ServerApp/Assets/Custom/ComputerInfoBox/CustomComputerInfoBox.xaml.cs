using HidSharp.Utility;
using ServerApp.Controllers;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ServerApp.Assets.Custom.ComputerInfoBox
{
    /// <summary>
    /// Логика взаимодействия для CustomComputerInfoBox.xaml
    /// </summary>
    public partial class CustomComputerInfoBox : Window
    {
        private static CustomComputerInfoBox s_customComputerInfoBox;
        private static bool s_result = false;
        private static DispatcherTimer s_dispatcherTimer;

        public CustomComputerInfoBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public static bool Show(string description, string pcName)
        {
            s_customComputerInfoBox = new();

            if (!string.IsNullOrEmpty(description))
            {
                s_customComputerInfoBox.descriptionTextBlock.Text = description;

            }
            else
            {
                s_dispatcherTimer = new DispatcherTimer();
                s_dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                s_dispatcherTimer.Tick += (object? sender, EventArgs e) =>
                {
                    s_customComputerInfoBox.descriptionTextBlock.Text = TcpController.S_Answer;
                };
                s_dispatcherTimer.Start();
            }

            s_customComputerInfoBox.ShowDialog();
            return s_result;
        }

        private void DragWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                throw;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if(s_dispatcherTimer!= null)
            {
                s_dispatcherTimer.Stop();
            }
            
            Close();
        }

        private void pcInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (s_dispatcherTimer != null)
            {
                s_dispatcherTimer.Stop();
            }

            s_result = true;
            s_customComputerInfoBox.Close();
        }
    }
}
