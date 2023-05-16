using ServerApp.Properties;
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

namespace ServerApp.Assets.Custom.StatBox
{
    /// <summary>
    /// Логика взаимодействия для CustomStatBox.xaml
    /// </summary>
    public partial class CustomStatBox : Window
    {
        private static CustomStatBox s_customStatBox;
        private static bool s_result = false;

        public CustomStatBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;

            SystemSounds.Asterisk.Play();
        }

        private static string GetDataByCulture(string culture, string enData, string ruData)
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

        public static bool Show(string message, string title, string status)
        {
            s_customStatBox = new();

            s_customStatBox.description.Text = GetDataByCulture(Settings.Default.LanguageName, $"What do you want to do with the app: {message}?", $"Что вы хотите делать с приложением: {message}?");
            s_customStatBox.title.Text = title;
            s_customStatBox.image.Source = s_customStatBox.GetImage(status);

            s_customStatBox.ShowDialog();

            return s_result;
        }


        private BitmapImage GetImage(string value)
        {
            if (value == "Approved")
            {
                value = "Confirm";
            }

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
            s_result = true;
            Close();
        }

        private void secondButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = false;
            s_customStatBox.Close();
        }

        private void firstButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = true;
            s_customStatBox.Close();
        }
    }
}
