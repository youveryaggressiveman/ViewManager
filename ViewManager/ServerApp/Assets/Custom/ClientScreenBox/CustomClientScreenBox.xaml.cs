using ServerApp.Controllers;
using ServerApp.Core.Screen;
using ServerApp.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ServerApp.Assets.Custom.ClientScreenBox
{
    /// <summary>
    /// Логика взаимодействия для CustomClientScreenBox.xaml
    /// </summary>
    public partial class CustomClientScreenBox : Window
    {
        private UdpController _udpController;
        private CustomClientScreenBox s_customClientScreenBox;
        private Thread _udpThread;

        public CustomClientScreenBox( string name)
        {
            InitializeComponent();

            pcNameRun.Text = name;

            _udpThread = new Thread(Start);
            _udpThread.Start();
        }

        private async void Start(object? obj)
        {
            _udpController = new(TcpServerSingleton.GetIp(), TcpServerSingleton.GetPort());

            await _udpController.StartUdp();

            screenImage.Source = ToScreenConverter.Image;
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
            _udpController.StopUdp();

            Close();
        }
    }
}
