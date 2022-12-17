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
    /// Логика взаимодействия для ComputerManagementPage.xaml
    /// </summary>
    public partial class ComputerManagementPage : Page
    {
        private readonly ComputerManagementPageViewModel _viewModel;

        public ComputerManagementPage()
        {
            InitializeComponent();

            DataContext = _viewModel = new ComputerManagementPageViewModel();
        }
    }
}
