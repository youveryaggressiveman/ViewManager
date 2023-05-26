using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Controllers;
using ServerApp.Core.Clients;
using ServerApp.Core.Screen;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
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
using System.Windows.Interop;
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
        private static ConnectedClient s_client;

        public CustomClientScreenBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        private string GetDataByCulture(string culture, string enData, string ruData)
        {
            if (culture == "en-US")
            {
                return enData;
            }
            else
            {
                return ruData;
            }
        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
        }

        public static void Show(ConnectedClient client)
        {
            s_customClientScreenBox = new();

            s_client = client;

            s_customClientScreenBox.pcNameRun.Text = client.Name;

            s_udpController = new(TcpServerSingleton.GetIp(), TcpServerSingleton.GetPort(), client.Ip, client.Port);
            s_udpController.Start();

            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += (sender, e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    if(ToScreenConverter.S_Image.Count != 0)
                    {
                        s_customClientScreenBox.image.Source = Imaging.CreateBitmapSourceFromHBitmap(ToScreenConverter.S_Image[0]!.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        ToScreenConverter.S_Image.Remove(ToScreenConverter.S_Image[0]);
                    }

                });

            };
            timer.Start();

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

                ToScreenConverter.S_Image.Clear();

                s_udpController.StopUdp();
                Close();
            }
            catch
            {
                CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "The broadcast was turned off.", "Трансляция была выключена."), MessageBox.Basic.Titles.Information, MessageBox.Basic.Buttons.Ok, MessageBox.Basic.Buttons.Nothing);
            }
        }
    }
}
