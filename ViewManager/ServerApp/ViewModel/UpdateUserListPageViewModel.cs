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
            if (string.IsNullOrWhiteSpace(SelectedUser.Login) || string.IsNullOrWhiteSpace(SelectedUser.Password) ||
                    SelectedRole == null || SelectedOffice == null)
            {
                MessageBox.Show("All fields must be filled in!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (MessageBox.Show("Are you sure you want to change the information about this user?", "Ask", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                SelectedUser.OfficeId = SelectedOffice.Id;
                SelectedUser.Office = SelectedOffice;
                SelectedUser.RoleId = SelectedRole.Id;
                SelectedUser.Role = SelectedRole;

                try
                {
                    if (await _userController.PutUserInfo(SelectedUser))
                    {
                        MessageBox.Show("Information has been successfully updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update information, try again later", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("No data yet", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                userList.ToList().ForEach(UserList.Add);
                roleList.ToList().ForEach(RoleList.Add);
                officeList.ToList().ForEach(OfficeList.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void CheckSelectedUserInfo()
        {
            SelectedRole = RoleList.FirstOrDefault(role => role.Id == SelectedUser.RoleId);
            SelectedOffice = OfficeList.FirstOrDefault(office => office.Id == SelectedUser.OfficeId);
        }
    }
}
