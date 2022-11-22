﻿using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly MainPageViewModelController _controller;

        private Visibility _accountantButtonVisibility = Visibility.Collapsed;
        private Visibility _teacherButtonVisibility = Visibility.Collapsed;

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

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }

        public MainPageViewModel()
        {
            _controller = new MainPageViewModelController(ApiServerSingleton.GetConnectionApiString());

            User = new User();

            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);

            LoadInfoAboutUser();
        }

        private void Update(object obj)
        {
            FrameManager.SetPage(new UpdateUserListPage(), "mainPageFrame");
        }

        private void Create(object obj)
        {
            FrameManager.SetPage(new CreateUserPage(), "mainPageFrame");
        }

        private async void LoadInfoAboutUser()
        {
            try
            {
                var user = await _controller.GetUserById();

                if(user == null)
                {
                    MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                User = user;

                CheckRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void CheckRole()
        {
            if(User.RoleId == 1)
            {
                TeacherButtonVisibility = Visibility.Visible;
                AccountantButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                TeacherButtonVisibility = Visibility.Collapsed;
                AccountantButtonVisibility = Visibility.Visible;

                FrameManager.SetPage(new UpdateUserListPage(), "mainPageFrame");
            }
        }
    }
}