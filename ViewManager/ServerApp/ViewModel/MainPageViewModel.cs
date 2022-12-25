using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Clients;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;

        private readonly ClientsSort _clientsSort;

        private readonly UniversalController<User> _userController;

        private Thread _start;

        private Visibility _accountantButtonVisibility = Visibility.Collapsed;
        private Visibility _teacherButtonVisibility = Visibility.Collapsed;
        private Visibility _commonButtonVisibility = Visibility.Collapsed;

        private User _user;

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
            get=> _accountantButtonVisibility;
            set
            {
                _accountantButtonVisibility = value;
                OnPropertyChanged(nameof(AccountantButtonVisibility));
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

        public ICommand StatisticsCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ComputerManagmentCommand { get; }
        public ICommand SettingsCommand { get; }

        public MainPageViewModel()
        {
            _fileManager = new AppStatisticsFileManager();
            _clientsSort = new ClientsSort();

            _fileManager.FileWriter("Statistics", null);
            _fileManager.FileWriter("AllowedApplications", null);
            _fileManager.FileWriter("Clients", null);

            _userController = new UniversalController<User>(ApiServerSingleton.GetConnectionApiString());

            User = new User();

            _start = new Thread(TcpConnect);

            StatisticsCommand = new DelegateCommand(Statistics);
            ComputerManagmentCommand = new DelegateCommand(ComputerManagment);
            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);
            SettingsCommand = new DelegateCommand(Settings);

            LoadInfoAboutUser();
        }

        private void Statistics(object obj)
        {
            FrameManager.SetPage(new StatisticsPage(), "mainPageFrame");
        }

        private void ComputerManagment(object obj)
        {
            FrameManager.SetPage(new ComputerManagementPage(), "mainPageFrame");
        }

        private void Settings(object obj)
        {
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

        private void SetBorder(bool switchBorder)
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).LoadBorder(switchBorder);
        }

        private async void LoadInfoAboutUser()
        {
            try
            {
                SetBorder(true);

                var user = await _userController.Get(AuthUserSingleton.AuthUser.Id);

                if(user == null)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                User = user;

                CheckRole();
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
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }
            finally
            {
                SetBorder(false);
            }
            
        }

        private async void CheckRole()
        {
            if(User.RoleId == 1)
            {
                TeacherButtonVisibility = Visibility.Visible;
                AccountantButtonVisibility = Visibility.Collapsed;
                CommonButtonVisibility = Visibility.Visible;

                TcpServerSingleton.SetIp(string.Empty);
                await _clientsSort.Sort();

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
    }
}
