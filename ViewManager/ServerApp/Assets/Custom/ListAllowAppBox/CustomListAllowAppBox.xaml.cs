using GeneralLogic.Services.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ServerApp.Assets.Custom.ListAllowAppBox
{
    /// <summary>
    /// Логика взаимодействия для CustomListAllowAppBox.xaml
    /// </summary>
    public partial class CustomListAllowAppBox : Window
    {
        private static readonly IFileManager s_fileManager = new AppStatisticsFileManager();

        private static CustomListAllowAppBox s_castomListAllowAppBox;
        private static bool s_result = false;

        public CustomListAllowAppBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public async static Task<bool> Show()
        {
            s_castomListAllowAppBox = new();

            try
            {
                var allApp = await s_fileManager.FileReader("AllowedApplications");
                var appList = allApp.Split('\n');

                foreach (var app in appList)
                {
                    if (app != string.Empty)
                    {
                        s_castomListAllowAppBox.appListView.Items.Add(app);
                    }                 
                }
            }
            finally
            {
                s_castomListAllowAppBox.ShowDialog();
            }

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

        private void saveChangedButton_Click(object sender, RoutedEventArgs e)
        {
            string allApp = string.Empty;

            foreach (var app in s_castomListAllowAppBox.appListView.Items)
            {
                allApp += app + "\n";
            }

            s_fileManager.FileWriter("AllowedApplications", allApp);

            s_result = true;
            s_castomListAllowAppBox.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(s_castomListAllowAppBox.appNameTextBox.Text))
            {
                foreach (var app in s_castomListAllowAppBox.appListView.Items)
                {
                    if(app.ToString() == s_castomListAllowAppBox.appNameTextBox.Text)
                    {
                        return;
                    }
                }

                s_castomListAllowAppBox.appListView.Items.Add(s_castomListAllowAppBox.appNameTextBox.Text);
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(s_castomListAllowAppBox.appListView.SelectedItem == null))
            {
                s_castomListAllowAppBox.appListView.Items.Remove(s_castomListAllowAppBox.appListView.SelectedItem);
            }
        }
    }
}
