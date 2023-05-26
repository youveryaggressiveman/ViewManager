using GeneralLogic.Services.Files;
using Newtonsoft.Json.Linq;
using ServerApp.Assets.Custom.ClientScreenBox;
using ServerApp.Assets.Custom.ComputerInfoBox;
using ServerApp.Assets.Custom.ListAllowAppBox;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Clients;
using ServerApp.Core.Screen;
using ServerApp.Core.Singleton;
using ServerApp.Core.Statistics;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ServerApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;

        private readonly UniversalController<User> _userController;

        private Visibility _visibility = Visibility.Collapsed;
        private Visibility _visibilityPc = Visibility.Collapsed;

        private BitmapImage _image;
        private User _user;

        private string _roleName;

        private bool _isEnabled = false;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public Visibility VisibilityPc
        {
            get => _visibilityPc;
            set
            {
                _visibilityPc = value;
                OnPropertyChanged(nameof(VisibilityPc));
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public string RoleName
        {
            get => _roleName;
            set
            {
                _roleName = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand InfoCommand { get; }
        public ICommand BroadcastCommand { get; }
        public ICommand TurnOffCommand { get; }
        public ICommand OpenListAppCommand { get; }
        public ICommand CheckPcFeaturesCommand { get; }

        public MainPageViewModel()
        {
            _fileManager = new AppStatisticsFileManager();

            CheckPcFeaturesCommand = new DelegateCommand(CheckPcFeatures);
            OpenListAppCommand = new DelegateCommand(OpenListApp);
            InfoCommand = new DelegateCommand(Info);
            BroadcastCommand = new DelegateCommand(Broadcast);
            TurnOffCommand = new DelegateCommand(TurnOff);

            _fileManager.FileWriter("Statistics", null);
            _fileManager.FileWriter("AllowedApplications", null);
            _fileManager.FileWriter("Clients", null);

            _userController = new UniversalController<User>(ApiServerSingleton.GetConnectionApiString());

            User = new User();

            LoadInfoAboutUser();

            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sender, e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    CheckConnectedPc();
                });

            };
            timer.Start();

            System.Timers.Timer timerForCheckConnection = new System.Timers.Timer(10000);
            timerForCheckConnection.Elapsed += (sender, e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    CheckConnection();
                });

            };
            timerForCheckConnection.Start();
        }

        private async void CheckConnection()
        {
            if (ConnectedClientSingleton.S_ListConnectedClient.Count == 0)
            {
                return;
            }
            try
            {
                foreach (var client in ConnectedClientSingleton.S_ListConnectedClient)
                {
                    if (!await TcpController.SendMessage(client, "0"))
                    {
                        client.Status = "Disconnected";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"ListConnection: {ex.Message}");
            }

        }

        private void CheckConnectedPc()
        {
            if (ConnectedClientSingleton.S_ListConnectedClient.Count == 0)
            {
                IsEnabled = false;

            }
            else
            {
                IsEnabled = true;
            }
        }

        private async void TurnOff(object obj)
        {
            if (SelectedConnectedClientSingleton.ConnectedClient != null && SelectedConnectedClientSingleton.ConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClientSingleton.ConnectedClient, "3");
                }
                catch
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "The selected PC could not be turned off.", "Выбранный компьютер не удалось выключить."), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }

            }
        }

        private async void Broadcast(object obj)
        {
            if (SelectedConnectedClientSingleton.ConnectedClient != null && SelectedConnectedClientSingleton.ConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClientSingleton.ConnectedClient, "2");

                    CustomClientScreenBox.Show(SelectedConnectedClientSingleton.ConnectedClient);
                }
                catch
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Could not enable the broadcast of the selected client.", "Не удалось включить широковещательную передачу выбранного клиента."), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }

            }
        }

        private async void Info(object obj)
        {
            if (SelectedConnectedClientSingleton.ConnectedClient != null && SelectedConnectedClientSingleton.ConnectedClient.Status == "Connected")
            {
                try
                {
                    await TcpController.SendMessage(SelectedConnectedClientSingleton.ConnectedClient, "1");

                    await CustomComputerInfoBox.Show(false, SelectedConnectedClientSingleton.ConnectedClient.Name);
                }
                catch
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Could not get information about the selected PC.", "Не удалось получить информацию о выбранном компьютере."), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }
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

        private void OpenListApp(object obj)
        {
            string message = string.Empty;

            try
            {
                CustomListAllowAppBox.Show();

                message = "Approved applications have been successfully added.";
            }
            catch
            {
                message = "An error occurred while adding the application.";
            }
            finally
            {
                LogManager.SaveLog("Server", DateTime.Today, $"TeacherMode: {message}");
            }
        }

        private async void CheckPcFeatures(object obj)
        {
            try
            {
                await CustomComputerInfoBox.Show(true, "Server");
            }
            catch
            {
                CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Could not get data about your computer!", "Не удалось получить данные о вашем компьютере!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
        }

        private void SetBorder(bool switchBorder)
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).LoadBorder(switchBorder);
        }

        private BitmapImage GetImage(string value)
        {
            DirectoryInfo directoryInfo = new(@"../../../Assets/Images");

            foreach (var image in directoryInfo.GetFiles())
            {
                if (image.Name == value + ".png")
                {
                    return new BitmapImage(new Uri(image.FullName));
                }
            }

            return null;
        }

        private async void LoadInfoAboutUser()
        {
            try
            {
                SetBorder(true);

                var user = await _userController.Get(AuthUserSingleton.AuthUser.Id);

                if (user == null)
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                User = user;

                foreach (var translation in TranslationSingleton.S_TranslationList)
                {
                    if (User.Role.Value == translation.Data)
                    {
                        if (ServerApp.Properties.Settings.Default.LanguageName == "en-US")
                        {
                            RoleName = translation.Data;
                        }
                        else
                        {
                            RoleName = translation.Translation;
                        }

                        break;
                    }
                }

                Image = GetImage(User.Role.Value);

                if (User.RoleId == 1)
                {
                    Visibility = Visibility.Visible;
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }

                foreach (Window window in App.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        (window.DataContext as MainWindowViewModel).CheckRole(User, _fileManager);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is Exception)
                {
                    if (await ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).CheckToken())
                    {
                        LoadInfoAboutUser();
                    }
                }
                else
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }
            finally
            {
                SetBorder(false);
            }

        }
    }
}
