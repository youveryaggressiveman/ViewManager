using HidSharp.Utility;
using ServerApp.Controllers;
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

namespace ServerApp.Assets.Custom.ComputerInfoBox
{
    /// <summary>
    /// Логика взаимодействия для CustomComputerInfoBox.xaml
    /// </summary>
    public partial class CustomComputerInfoBox : Window
    {
        private static CustomComputerInfoBox s_customComputerInfoBox;
        private static bool s_result = false;
        private static string s_answer;

        public CustomComputerInfoBox()
        {
            InitializeComponent();
        }

        public static bool Show(string description)
        {
            s_customComputerInfoBox = new();

            s_customComputerInfoBox.descriptionTextBlock.Text= description;
            s_answer = description;

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
            TcpController.S_AnswerList.Remove(s_answer);
            Close();
        }

        private void pcInfoButton_Click(object sender, RoutedEventArgs e)
        {
            TcpController.S_AnswerList.Remove(s_answer);
            s_result = true;
            s_customComputerInfoBox.Close();
        }
    }
}
