using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Model;
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
        private readonly UpdateUserListPageViewModelController<User> _userController;
        private readonly UpdateUserListPageViewModelController<Role> _roleController;
        private readonly UpdateUserListPageViewModelController<Office> _officeController;

        private ObservableCollection<User> _userList;
        private ObservableCollection<Role> _roleList;
        private ObservableCollection<Office> _officeList;

        private Role _selectedRole;
        private Office _selectedOffice;
        private User _selectedUser;

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
            _userController = new UpdateUserListPageViewModelController<User>(ApiServerSingleton.GetConnectionApiString());
            _roleController = new UpdateUserListPageViewModelController<Role>(ApiServerSingleton.GetConnectionApiString());
            _officeController = new UpdateUserListPageViewModelController<Office>(ApiServerSingleton.GetConnectionApiString());

            UserList = new ObservableCollection<User>();
            OfficeList = new ObservableCollection<Office>();
            RoleList = new ObservableCollection<Role>();

            PutCommand = new DelegateCommand(Put);

            LoadInfo();
        }

        private async void Put(object obj)
        {
            if (SelectedUser == null) 
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedUser.Login) || string.IsNullOrWhiteSpace(SelectedUser.Password) ||
                    SelectedRole == null || SelectedOffice == null)
            {
                CustomMessageBox.Show("Required fields must be filled in!", Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);           

                return;
            }

            if (CustomMessageBox.Show("Are you sure you want to change the information about this user?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
            {
                SelectedUser.OfficeId = SelectedOffice.Id;
                SelectedUser.Office = SelectedOffice;
                SelectedUser.RoleId = SelectedRole.Id;
                SelectedUser.Role = SelectedRole;

                try
                {
                    if (await _userController.PutUserInfo(SelectedUser))
                    {
                        CustomMessageBox.Show("Information has been successfully updated", Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LoadInfo();

                        LogManager.SaveLog("Server", DateTime.Today, $"AccountantMode: data about {SelectedUser.FIO} successfully updated");
                    }
                    else
                    {
                        CustomMessageBox.Show("Failed to update information, try again later", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }


            }

        }

        private async void LoadInfo()
        {
            try
            {
                var userList = await _userController.GetList();
                var roleList = await _roleController.GetList();
                var officeList = await _officeController.GetList();

                if (userList == null || roleList == null || officeList == null)
                {
                    CustomMessageBox.Show("No data yet", Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                userList.ToList().ForEach(UserList.Add);
                roleList.ToList().ForEach(RoleList.Add);
                officeList.ToList().ForEach(OfficeList.Add);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }

        }

        private void CheckSelectedUserInfo()
        {
            SelectedRole = RoleList.FirstOrDefault(role => role.Id == SelectedUser.RoleId);
            SelectedOffice = OfficeList.FirstOrDefault(office => office.Id == SelectedUser.OfficeId);
        }
    }
}
