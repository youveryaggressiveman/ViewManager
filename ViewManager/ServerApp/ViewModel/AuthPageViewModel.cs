using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class AuthPageViewModel : BaseViewModel
    {
        private readonly AuthPageViewModelController _controller;

        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand AuthCommand { get; }

        public AuthPageViewModel()
        {
            AuthCommand = new DelegateCommand(Auth);

            _controller = new AuthPageViewModelController(ApiServerSingleton.GetConnectionApiString());
        }

        private void SetBorder(bool switchBorder)
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).LoadBorder(switchBorder);
        }

        private async void Auth(object obj)
        {
            var user = new User()
            {
                Login = Login,
                Password = Password
            };

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            if(Validator.TryValidateObject(user, context, results, true))
            {
                SetBorder(true);

                try
                {
                    if (await _controller.AuthHelper(user))
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Auth: Authorization was successful");

                        FrameManager.SetPage(new MainPage(), "mainFrame");
                    }
                    else
                    {
                        CustomMessageBox.Show("There is no user with such data.", Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LogManager.SaveLog("Server", DateTime.Today, "Auth: There is no user with such data");
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    LogManager.SaveLog("Server", DateTime.Today, "Auth: " + ex.Message);
                }
                finally
                {
                    SetBorder(false);
                }
            }
            else
            {
                foreach (var error in results)
                {
                    CustomMessageBox.Show(error.ErrorMessage, Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    LogManager.SaveLog("Server", DateTime.Today, "Auth: " + error.ErrorMessage);
                }
            }
        }
    }
}
