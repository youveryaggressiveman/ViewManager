using GeneralLogic.Services.Files;
using H.NotifyIcon;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class UpdateUserListPageViewModel : BaseViewModel
    {
        private readonly UniversalController<User> _userController;
        private readonly UniversalController<Role> _roleController;
        private readonly UniversalController<Office> _officeController;

        private ObservableCollection<User> _userList;
        private ObservableCollection<Role> _roleList;
        private ObservableCollection<Office> _officeList;

        private Role _selectedRole;
        private Office _selectedOffice;
        private User _selectedUser;

        private string _password;

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

        public string Password
        {
            get => _password;
            set
            {
                _password= value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public Office SelectedOffice
        {
            get => _selectedOffice;
            set
            {
                _selectedOffice = value;
                OnPropertyChanged(nameof(SelectedOffice));
            }
        }

        public ObservableCollection<Role> RoleList
        {
            get => _roleList;
            set
            {
                _roleList = value;
                OnPropertyChanged(nameof(RoleList));
            }
        }

        public ObservableCollection<Office> OfficeList
        {
            get => _officeList;
            set
            {
                _officeList = value;
                OnPropertyChanged(nameof(OfficeList));
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));

                if(_selectedUser == null)
                {
                    IsEnabled = false;
                }
                else
                {
                    IsEnabled = true;
                }

                CheckSelectedUserInfo();
            }
        }

        public ObservableCollection<User> UserList
        {
            get => _userList;
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }

        public ICommand PutCommand { get; }

        public UpdateUserListPageViewModel()
        {
            _userController = new UniversalController<User>(ApiServerSingleton.GetConnectionApiString());
            _roleController = new UniversalController<Role>(ApiServerSingleton.GetConnectionApiString());
            _officeController = new UniversalController<Office>(ApiServerSingleton.GetConnectionApiString());

            PutCommand = new DelegateCommand(Put);

            LoadInfo();
        }

        private void SetBorder(bool switchBorder)
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).LoadBorder(switchBorder);
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

        private async void Put(object obj)
        {
            if (SelectedUser == null) 
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedUser.Login) || string.IsNullOrWhiteSpace(Password) ||
                    SelectedRole == null || SelectedOffice == null)
            {
                CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Required fields must be filled in!", "Обязательные поля должны быть заполнены!"), Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);           

                return;
            }

            if (CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Are you sure you want to change the information about this user?", "Вы уверены, что хотите изменить информацию об этом пользователе?"), Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
            {
                SetBorder(true);

                SelectedUser.OfficeId = SelectedOffice.Id;
                SelectedUser.Office = SelectedOffice;
                SelectedUser.RoleId = SelectedRole.Id;
                SelectedUser.Role = SelectedRole;
                SelectedUser.Password = Password;

                try
                {
                    if (await _userController.Put(SelectedUser))
                    {
                        CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Information has been successfully updated!", "Информация была успешно обновлена!"), Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LogManager.SaveLog("Server", DateTime.Today, $"AdminMode: data about {SelectedUser.FIO} successfully updated.");

                        LoadInfo();
                    }
                    else
                    {
                        CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Failed to update information, try again later.", "Не удалось обновить информацию, повторите попытку позже."), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException() is Exception)
                    {
                        if (await ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).CheckToken())
                        {
                            Put(null);
                        }
                    }
                    else
                    {
                        CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                    }
                }
                finally
                {
                    SetBorder(false);
                }
            }

        }

        private async void LoadInfo()
        {
            SelectedUser = null;

            UserList = new ObservableCollection<User>();
            OfficeList = new ObservableCollection<Office>();
            RoleList = new ObservableCollection<Role>();

            try
            {
                var userList = await _userController.GetList();
                var roleList = await _roleController.GetList();
                var officeList = await _officeController.GetList();

                if (userList == null || roleList == null || officeList == null)
                {
                    CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "No data yet!", "Данных пока нет!"), Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                userList.ToList().ForEach(UserList.Add);
                roleList.ToList().ForEach(RoleList.Add);
                officeList.ToList().ForEach(OfficeList.Add);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is Exception)
                {
                    if (await ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).CheckToken())
                    {
                        LoadInfo();
                    }
                }
                else
                {
                    CustomMessageBox.Show(GetDataByCulture(Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }

        }

        private void CheckSelectedUserInfo()
        {
            if (SelectedUser != null)
            {
                SelectedRole = RoleList.FirstOrDefault(role => role.Id == SelectedUser.RoleId);
                SelectedOffice = OfficeList.FirstOrDefault(office => office.Id == SelectedUser.OfficeId);
            }
        }
    }
}
