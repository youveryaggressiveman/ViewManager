using GeneralLogic.Services.Files;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Command;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ServerApp.ViewModel
{
    public class AuthPageViewModel : BaseViewModel
    {
        private readonly AuthController _controller;

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

            _controller = new AuthController(ApiServerSingleton.GetConnectionApiString());
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
                        LogManager.SaveLog("Server", DateTime.Today, "Auth: Authorization was successful.");

                        Settings.Default.Login= Login;
                        Settings.Default.Password = Password;
                        Settings.Default.Save();

                        FrameManager.SetPage(new MainPage(), "mainFrame");
                    }
                    else
                    {
                        CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "There is no user with such data.", "Пользователя с такими данными нет."), Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LogManager.SaveLog("Server", DateTime.Today, "Auth: There is no user with such data.");
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    LogManager.SaveLog("Server", DateTime.Today, $"Auth: {ex.Message}.");
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
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "The input data is incorrect", "Входные данные неверны"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    LogManager.SaveLog("Server", DateTime.Today, $"Auth: {error.ErrorMessage}.");
                }
            }
        }
    }
}
