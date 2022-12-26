using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Controllers;
using ServerApp.Core.Clients;
using ServerApp.Core.Screen;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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

namespace ServerApp.Assets.Custom.ClientScreenBox
{
    /// <summary>
    /// Логика взаимодействия для CustomClientScreenBox.xaml
    /// </summary>
    public partial class CustomClientScreenBox : Window
    {
        private static UdpController s_udpController;
        private static CustomClientScreenBox s_customClientScreenBox;
        private static DispatcherTimer s_dispatcherTimer;
        private static ConnectedClient s_client;

        public CustomClientScreenBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public static void Show(ConnectedClient client)
        {
            s_customClientScreenBox = new();

            s_client = client;

            s_customClientScreenBox.pcNameRun.Text = client.Name;

            s_udpController = new(TcpServerSingleton.GetIp(), TcpServerSingleton.GetPort(), client.Ip, client.Port);
            s_udpController.Start();

            s_dispatcherTimer = new DispatcherTimer();
            s_dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            s_dispatcherTimer.Tick += (object? sender, EventArgs e) =>
            {
                s_customClientScreenBox.image.Dispatcher.Invoke(() =>
                {
                   
                }, DispatcherPriority.Normal); 
            };
            s_dispatcherTimer.Start();

            s_customClientScreenBox.ShowDialog();
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

        private async void exitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await TcpController.SendMessage(s_client, "4");

                s_udpController.StopUdp();
                Close();
            }
            catch
            {
                CustomMessageBox.Show("The broadcast was turned off.", MessageBox.Basic.Titles.Information, MessageBox.Basic.Buttons.Ok, MessageBox.Basic.Buttons.Nothing);
            }
        }
    }
}
