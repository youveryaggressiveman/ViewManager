using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.ArrangeData;
using GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib;
using Mono.Unix.Native;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Controllers;
using ServerApp.Core;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ServerApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;
        private readonly IFileManager _fileStatManager;

        private readonly AuthController _authController;
        private readonly Visitor _visitor;
        private readonly ArrangeHelper _arrangeHelper;

        private Visibility _visibility = Visibility.Collapsed;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public MainWindowViewModel()
        {
            LogManager.CreateMainFolder();

            _fileManager= new PcFeaturesFileManager();
            _fileStatManager = new AppStatisticsFileManager();

            _authController = new(ApiServerSingleton.GetConnectionApiString());

            _arrangeHelper = new();
            _visitor = new();

            Timer timer = new Timer(5000);
            timer.Elapsed += async (sender, e) => await CheckAllConnection();
            timer.Start();

            FileWork();
        }

        public async Task<bool> CheckToken()
        {
            if( CustomMessageBox.Show("Your session is outdated. Do you want to reauthorize?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Yes, Assets.Custom.MessageBox.Basic.Buttons.No))
            {
                LoadBorder(true);

                var user = new User()
                {
                    Login = Settings.Default.Login,
                    Password = Settings.Default.Password
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
                        CustomMessageBox.Show("There is no user with such data.", Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LogManager.SaveLog("Server", DateTime.Today, "Auth: There is no user with such data.");

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

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
            if (Settings.Default.Date != DateTime.Today)
            {
                await _fileStatManager.FileWriter("Statistics", string.Empty);

                Settings.Default.Date = DateTime.Today;
                Settings.Default.Save();
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
