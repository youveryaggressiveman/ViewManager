using ServerApp.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void pswBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as AuthPageViewModel).Password = pswBox.Password;
        }

        private void checkPsw_Click(object sender, RoutedEventArgs e)
        {
            if (checkPsw.IsChecked == true)
            {
                pswBox.Visibility = Visibility.Collapsed;
                pswTextBox.Visibility = Visibility.Visible;

                pswTextBox.Text = pswBox.Password;
            }
            else
            {
                pswBox.Visibility = Visibility.Visible;
                pswTextBox.Visibility = Visibility.Collapsed;

                pswBox.Password = pswTextBox.Text;
            }
        }
    }
}
