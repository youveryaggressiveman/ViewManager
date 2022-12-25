using MaterialDesignThemes.Wpf;
using ServerApp.Assets.Custom.MessageBox.Basic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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

namespace ServerApp.Assets.Custom.MessageBox
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;

            SystemSounds.Asterisk.Play();
        }

        private static CustomMessageBox s_customMessageBox;
        private static bool s_result = false;

        public static bool Show(string message, Titles title, Buttons buttonOk, Buttons buttonNo)
        {
            s_customMessageBox = new(); 

            s_customMessageBox.description.Text = message;
            s_customMessageBox.title.Text = s_customMessageBox.GetTitle(title);

            s_customMessageBox.firstButton.Visibility = Visibility.Collapsed;
            s_customMessageBox.secondButton.Visibility = Visibility.Collapsed;

            switch (title)
            {
                case Titles.Error:
                    s_customMessageBox.image.Source = s_customMessageBox.GetImage("Error");

                    s_customMessageBox.firstButton.Content = s_customMessageBox.GetButton(buttonOk);
                    s_customMessageBox.firstButton.Visibility = Visibility.Visible;
                    break;
                case Titles.Information:
                    s_customMessageBox.image.Source = s_customMessageBox.GetImage("Info");

                    s_customMessageBox.firstButton.Content = s_customMessageBox.GetButton(buttonOk);
                    s_customMessageBox.firstButton.Visibility = Visibility.Visible;

                    if (buttonNo != Buttons.Nothing)
                    {
                        s_customMessageBox.secondButton.Content = s_customMessageBox.GetButton(buttonNo);
                        s_customMessageBox.secondButton.Visibility = Visibility.Visible;
                    }
                    break;
                case Titles.Ask:
                    s_customMessageBox.image.Source = s_customMessageBox.GetImage("Ask");

                    s_customMessageBox.firstButton.Content = s_customMessageBox.GetButton(buttonOk);
                    s_customMessageBox.firstButton.Visibility = Visibility.Visible;

                    s_customMessageBox.secondButton.Content = s_customMessageBox.GetButton(buttonNo);
                    s_customMessageBox.secondButton.Visibility = Visibility.Visible;
                    break;
                case Titles.Warning:
                    s_customMessageBox.image.Source = s_customMessageBox.GetImage("Warning");

                    s_customMessageBox.firstButton.Content = s_customMessageBox.GetButton(buttonOk);
                    s_customMessageBox.firstButton.Visibility = Visibility.Visible;
                    break;
                case Titles.Confirm:
                    s_customMessageBox.image.Source = s_customMessageBox.GetImage("Confirm");

                    s_customMessageBox.firstButton.Content = s_customMessageBox.GetButton(buttonOk);
                    s_customMessageBox.firstButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;

            }

            s_customMessageBox.ShowDialog();

            return s_result;
        }


        private BitmapImage GetImage(string value)
        {
            DirectoryInfo directoryInfo = new(@"../../../Assets/Custom/MessageBox/Icons/");

            foreach (var image in directoryInfo.GetFiles())
            {
                if (image.Name == value + ".png")
                {
                    return new BitmapImage(new Uri(image.FullName));
                }
            }

            return null;
        }

        private string GetTitle(Titles value)
        {
            return Enum.GetName(typeof(Titles), value);
        }

        private string GetButton(Buttons value)
        {
            return Enum.GetName(typeof(Buttons), value);
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
            Close();
        }

        private void secondButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = false;
            s_customMessageBox.Close();
        }

        private void firstButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = true;
            s_customMessageBox.Close();
        }
    }
}
