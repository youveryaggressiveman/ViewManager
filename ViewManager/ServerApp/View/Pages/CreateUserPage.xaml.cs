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
    /// Логика взаимодействия для CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox).Text.Length >= 50)
            {
                e.Handled = true;
            }
        }
    }
}
