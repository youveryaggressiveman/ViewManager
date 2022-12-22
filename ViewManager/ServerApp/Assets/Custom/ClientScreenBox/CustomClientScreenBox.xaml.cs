﻿using ServerApp.Controllers;
using ServerApp.Core.Clients;
using ServerApp.Core.Screen;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

        private ConnectedClient _client;

        public CustomClientScreenBox(ConnectedClient client)
        {
            InitializeComponent();

            _client= client;

            pcNameRun.Text = client.Name;

            ToScreenConverter.Image = GetImage("ImageScreen");

            _udpThread = new Thread(Start);
            _udpThread.Start();
        }

        private async void Start(object? obj)
        {
            _udpController = new(TcpServerSingleton.GetIp(), TcpServerSingleton.GetPort());

            await _udpController.StartUdp();
        }

        private BitmapImage GetImage(string value)
        {
            DirectoryInfo directoryInfo = new(@"../../../Assets/Images/");

            foreach (var image in directoryInfo.GetFiles())
            {
                if (image.Name == value + ".png")
                {
                    return new BitmapImage(new Uri(image.FullName));
                }
            }

            return null;
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
            await TcpController.SendMessage(_client, "4");

            _udpController.StopUdp();

            Close();
        }
    }
}