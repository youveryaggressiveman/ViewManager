using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.ArrangeData;
using GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib;
using Microsoft.VisualBasic;
using Mono.Unix.Native;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Clients;
using ServerApp.Core.Singleton;
using ServerApp.Core.Statistics;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace ServerApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;
        private readonly IFileManager _fileStatManager;

        private readonly AuthController _authController;
        private readonly Visitor _visitor;
        private readonly ArrangeHelper _arrangeHelper;
        private readonly StatisticsSort _statisticsSort;
        private readonly ClientsSort _clientsSort;

        private Thread _start;

        private Visibility _visibility = Visibility.Collapsed;
        private Visibility _accountantButtonVisibility = Visibility.Collapsed;
        private Visibility _teacherButtonVisibility = Visibility.Collapsed;
        private Visibility _commonButtonVisibility = Visibility.Collapsed;

        public Visibility CommonButtonVisibility
        {
            get => _commonButtonVisibility;
            set
            {
                _commonButtonVisibility = value;
                OnPropertyChanged(nameof(CommonButtonVisibility));
            }
        }

        public Visibility TeacherButtonVisibility
        {
            get => _teacherButtonVisibility;
            set
            {
                _teacherButtonVisibility = value;
                OnPropertyChanged(nameof(TeacherButtonVisibility));
            }
        }

        public Visibility AccountantButtonVisibility
        {
            get => _accountantButtonVisibility;
            set
            {
                _accountantButtonVisibility = value;
                OnPropertyChanged(nameof(AccountantButtonVisibility));
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

        public ICommand StatisticsCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ComputerManagmentCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand CloseAppCommand { get; }

        public MainWindowViewModel()
        {
            LogManager.CreateMainFolder();

            StatisticsCommand = new DelegateCommand(Statistics);
            ComputerManagmentCommand = new DelegateCommand(ComputerManagment);
            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);
            SettingsCommand = new DelegateCommand(Settings);
            CloseAppCommand = new DelegateCommand(CloseApp);

            _fileManager = new PcFeaturesFileManager();
            _fileStatManager = new AppStatisticsFileManager();
            _clientsSort = new ClientsSort();

            _authController = new(ApiServerSingleton.GetConnectionApiString());

            _start = new Thread(TcpConnect);

            _statisticsSort = new();
            _arrangeHelper = new();
            _visitor = new();

            Timer timer = new Timer(5000);
            timer.Elapsed += async (sender, e) => await CheckAllConnection();
            timer.Start();

            FileWork();
        }

        private void Statistics(object obj)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    var mainPage = ((window as MainWindow).mainFrame.Content as Page).DataContext as MainPageViewModel;

                    mainPage.VisibilityPc = Visibility.Collapsed;
                }
            }

            FrameManager.SetPage(new StatisticsPage(), "mainPageFrame");
        }

        private void ComputerManagment(object obj)
        {
            FrameManager.SetPage(new ComputerManagementPage(), "mainPageFrame");
        }

        private void Settings(object obj)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    var mainPage = ((window as MainWindow).mainFrame.Content as Page).DataContext as MainPageViewModel;

                    mainPage.VisibilityPc = Visibility.Collapsed;
                }
            }

            FrameManager.SetPage(new SettingsPage(), "mainPageFrame");
        }

        private void Update(object obj)
        {
            FrameManager.SetPage(new UpdateUserListPage(), "mainPageFrame");
        }

        private void Create(object obj)
        {
            FrameManager.SetPage(new CreateUserPage(), "mainPageFrame");
        }

        private async void TcpConnect(object? obj)
        {
            await TcpController.StartTcp();
        }

        public async void CheckRole(User user, IFileManager fileManager)
        {
            if (user.RoleId == 1)
            {
                TeacherButtonVisibility = Visibility.Visible;
                AccountantButtonVisibility = Visibility.Collapsed;
                CommonButtonVisibility = Visibility.Visible;

                TcpServerSingleton.SetIp(string.Empty);
                await _clientsSort.Sort();

                await GetAllowApp(fileManager);

                var allStat = await fileManager.FileReader("Statistics");
                var statList = await _statisticsSort.Sort(allStat);

                statList.ToList().ForEach(AppStatSingleton.S_ListAppStat.Add);

                _start.Start();
            }
            else
            {
                TeacherButtonVisibility = Visibility.Collapsed;
                AccountantButtonVisibility = Visibility.Visible;
                CommonButtonVisibility = Visibility.Visible;

            }

            FrameManager.SetPage(new SettingsPage(), "mainPageFrame");
        }

        private async Task GetAllowApp(IFileManager fileManager)
        {
            var allApp = await fileManager.FileReader("AllowedApplications");
            var appList = allApp.Split('\n');

            foreach (var app in appList)
            {
                if (app != string.Empty)
                {
                    ListAllowAppSingleton.S_AllowAppList.Add(app);
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

        private async void CloseApp(object obj)
        {
            if (CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Are you sure you want to close the program?", "Вы уверены, что хотите закрыть программу?"), Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Yes, Assets.Custom.MessageBox.Basic.Buttons.No))
            {
                if (!string.IsNullOrEmpty(AuthUserSingleton.AuthUser.RoleValue) && AuthUserSingleton.AuthUser.RoleValue == "Teacher")
                {
                    await StatForm.SaveClient();
                    await StatForm.SaveStat();
                }

                Application.Current.Shutdown();
            }
        }

        public async Task<bool> CheckToken()
        {
            if(CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Your session is outdated. Do you want to reauthorize?", "Ваш сеанс устарел. Вы хотите повторно авторизоваться?"), Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Yes, Assets.Custom.MessageBox.Basic.Buttons.No))
            {
                LoadBorder(true);

                var user = new User()
                {
                    Login = ServerApp.Properties.Settings.Default.Login,
                    Password = ServerApp.Properties.Settings.Default.Password
                };

                try
                {
                    if (await _authController.AuthHelper(user))
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Auth: Authorization was successful.");

                        return true;
                    }
                    else
                    {
                        CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "There is no user with such data.", "Пользователя с такими данными нет."), Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LogManager.SaveLog("Server", DateTime.Today, "Auth: There is no user with such data.");

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    LogManager.SaveLog("Server", DateTime.Today, $"Auth: {ex.Message}.");

                    return false;
                }
                finally
                {
                    LoadBorder(false);
                }
            }
            else
            {
                FrameManager.SetPage(new AuthPage(), "mainFrame");

                return false;
            }
        }

        private async void FileWork()
        {
            string message = string.Empty;

            try
            {
                var result = _arrangeHelper.GetArrangeData();

                string endResult = string.Empty;

                foreach (var hardware in result.NameHardware)
                {
                    endResult += hardware + "\n";
                }

                await _fileManager.FileWriter(endResult, "Server");

                message = "The file with the characteristics has been filled in successfully";
            }
            catch
            {
                message = "An error occurred when filling in the character file";
            }
            finally
            {
                LogManager.SaveLog("Client", DateTime.Today, $"FileWriter: {message}.");
            }

        }

        private async Task CheckAllConnection()
        {
            if (ServerApp.Properties.Settings.Default.Date != DateTime.Today)
            {
                await _fileStatManager.FileWriter("Statistics", string.Empty);

                ServerApp.Properties.Settings.Default.Date = DateTime.Today;
                ServerApp.Properties.Settings.Default.Save();
            }

            if (await ApiServerSingleton.CheckConnection())
            {
                LoadBorder(false);
            }
            else
            {
                LoadBorder(true);
            }
        }

        public void LoadBorder(bool switchBorder)
        {
            if (switchBorder)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
