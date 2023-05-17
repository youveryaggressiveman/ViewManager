using GeneralLogic.Services.Files;
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
    public class CreateUserPageViewModel : BaseViewModel
    {
        private readonly UniversalController<Specialization> _specializationController;
        private readonly UniversalController<Office> _officeController;
        private readonly UniversalController<User> _userController;

        private User _newUser;

        private ObservableCollection<Specialization> _selectedSpecializationList;
        private ObservableCollection<Office> _officeList;
        private ObservableCollection<Specialization> _specializationList;

        private Specialization _selectedSpecializationOfSelectedList;
        private Specialization _selectedSpecialization;
        private Office _selectedOffice;

        public Specialization SelectedSpecializationOfSelectedList
        {
            get => _selectedSpecializationOfSelectedList;
            set
            {
                _selectedSpecializationOfSelectedList = value;
                OnPropertyChanged(nameof(SelectedSpecializationOfSelectedList));
            }
        }

        public ObservableCollection<Specialization> SelectedSpecializationList
        {
            get => _selectedSpecializationList;
            set
            {
                _selectedSpecializationList = value;
                OnPropertyChanged(nameof(SelectedSpecializationList));
            }
        }

        public User NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                OnPropertyChanged(nameof(NewUser));
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

        public Specialization SelectedSpecialization
        {
            get => _selectedSpecialization;
            set
            {
                _selectedSpecialization = value;
                OnPropertyChanged(nameof(SelectedSpecialization));
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

        public ObservableCollection<Specialization> SpecializationList
        {
            get => _specializationList;
            set
            {
                _specializationList = value;
                OnPropertyChanged(nameof(SpecializationList));
            }
        }

        public ICommand CreateUserCommand { get; }
        public ICommand AddSpecCommand { get; }
        public ICommand RemoveSpecCommand { get; }

        public CreateUserPageViewModel()
        {
            RemoveSpecCommand = new DelegateCommand(RemoveSpec);
            CreateUserCommand = new DelegateCommand(CreateUser);
            AddSpecCommand = new DelegateCommand(AddSpec);

            _userController = new UniversalController<User>(ApiServerSingleton.GetConnectionApiString());
            _specializationController = new UniversalController<Specialization>(ApiServerSingleton.GetConnectionApiString());
            _officeController = new UniversalController<Office>(ApiServerSingleton.GetConnectionApiString());

            NewUser = new();
            NewUser.Specializations = new ObservableCollection<Specialization>();

            LoadInfo();
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

        private void AddSpec(object obj)
        {
            if (SelectedSpecialization == null || SelectedSpecializationList.Contains(SelectedSpecialization))
            {
                return;
            }

            SelectedSpecializationList.Add(SelectedSpecialization);
        }

        private void RemoveSpec(object obj)
        {
            if (SelectedSpecializationOfSelectedList == null)
            {
                return;
            }

            SelectedSpecializationList.Remove(SelectedSpecializationOfSelectedList);
        }

        private void SetBorder(bool switchBorder)
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).LoadBorder(switchBorder);
        }

        private async void CreateUser(object obj)
        {
            if (string.IsNullOrWhiteSpace(NewUser.FirstName) || string.IsNullOrWhiteSpace(NewUser.LastName)
                    || SelectedOffice == null)
            {
                CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "All fields must be filled in!", "Все поля должны быть заполнены!"), Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                return;
            }

            if (CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Are you sure you want to add a new user to the system?", "Вы уверены, что хотите добавить нового пользователя в систему?"), Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
            {
                SetBorder(true);

                NewUser.RoleId = 1;
                NewUser.Office = SelectedOffice;
                NewUser.OfficeId = SelectedOffice.Id;
                NewUser.Specializations = SelectedSpecializationList.ToList();

                NewUser.Role = new Role()
                {
                    Value = "Teacher",
                    Id = 1
                };
                NewUser.Login = string.Empty;
                NewUser.Password = string.Empty;
                NewUser.Id = string.Empty;

                foreach (var translation in TranslationSingleton.S_TranslationList)
                {
                    if (Settings.Default.LanguageName == "en-US")
                    {
                        if (NewUser.Office.Value == translation.Data)
                        {
                            NewUser.Office.Value = translation.Data;
                        }

                        foreach (var spec in NewUser.Specializations)
                        {
                            if (translation.Data == spec.Value)
                            {
                                spec.Value = translation.Data;
                            }
                        }
                    }
                    else
                    {
                        if (NewUser.Office.Value == translation.Translation)
                        {
                            NewUser.Office.Value = translation.Data;
                        }

                        foreach (var spec in NewUser.Specializations)
                        {
                            if (translation.Translation == spec.Value)
                            {
                                spec.Value = translation.Data;
                            }
                        }
                    }
                }

                try
                {

                    var newUser = await _userController.Create(NewUser);

                    if (newUser != default)
                    {
                        CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName,
                            "New user successfully added to the system!\n" +
                            $"Login: {newUser.Login}\n" +
                            $"Password: {newUser.Password}",
                            "Новый пользователь успешно добавлен в систему!\n" +
                            $"Логин: {newUser.Login}\n" +
                            $"Пароль: {newUser.Password}"),
                            Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LoadInfo();

                        LogManager.SaveLog("Server", DateTime.Today, $"AdminMode: data about {NewUser.FIO} successfully create.");
                    }
                    else
                    {
                        CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Failed to create new user, try again later.", "Не удалось создать нового пользователя, повторите попытку позже."), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException() is Exception)
                    {
                        if (await ((Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel).CheckToken())
                        {
                            CreateUser(null);
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

        private async void LoadInfo()
        {
            SpecializationList = new ObservableCollection<Specialization>();
            OfficeList = new ObservableCollection<Office>();
            SelectedSpecializationList = new ObservableCollection<Specialization>();

            try
            {
                var officeList = await _officeController.GetList();
                var specList = await _specializationController.GetList();

                if (officeList == null || specList == null)
                {
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                foreach (var translation in TranslationSingleton.S_TranslationList)
                {
                    foreach (var office in officeList)
                    {
                        if (office.Value == translation.Data)
                        {
                            if (Settings.Default.LanguageName == "en-US")
                            {
                                office.Value = translation.Data;
                            }
                            else
                            {
                                office.Value = translation.Translation;
                            }
                        }
                    }

                    foreach (var spec in specList)
                    {
                        if (spec.Value == translation.Data)
                        {
                            if (Settings.Default.LanguageName == "en-US")
                            {
                                spec.Value = translation.Data;
                            }
                            else
                            {
                                spec.Value = translation.Translation;
                            }
                        }
                    }
                }

                officeList.ToList().ForEach(OfficeList.Add);
                specList.ToList().ForEach(SpecializationList.Add);

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
                    CustomMessageBox.Show(GetDataByCulture(ServerApp.Properties.Settings.Default.LanguageName, "Error server!", "Ошибка сервера!"), Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
            }
        }
    }
}
