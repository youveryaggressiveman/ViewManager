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

namespace ClientApp.Assets.Custom.ComputerInfoBox
{
    /// <summary>
    /// Логика взаимодействия для CustomComputerInfoBox.xaml
    /// </summary>
    public partial class CustomComputerInfoBox : Window
    {
        private static CustomComputerInfoBox s_customComputerInfoBox;
        private static bool s_result = false;

        public CustomComputerInfoBox()
        {
            InitializeComponent();
        }

        public static bool Show(string pcName, string description)
        {
            s_customComputerInfoBox = new CustomComputerInfoBox();

            s_customComputerInfoBox.pcNameRun.Text = pcName;
            s_customComputerInfoBox.descriptionTextBlock.Text = description;

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
            Close();
        }

        private void pcInfoButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = true;
            s_customComputerInfoBox.Close();
        }
    }
}
