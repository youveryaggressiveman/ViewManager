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
                _selectedSpecializationOfSelectedList= value;
                OnPropertyChanged(nameof(SelectedSpecializationOfSelectedList));
            }
        }

        public ObservableCollection<Specialization> SelectedSpecializationList
        {
            get => _selectedSpecializationList;
            set
            {
                _selectedSpecializationList= value;
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
            get=> _selectedSpecialization;
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

        private void AddSpec(object obj)
        {
            if(SelectedSpecialization == null || SelectedSpecializationList.Contains(SelectedSpecialization))
            {
                return;
            }

            SelectedSpecializationList.Add(SelectedSpecialization);
        }

        private void RemoveSpec(object obj)
        {
            if(SelectedSpecializationOfSelectedList == null)
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
                CustomMessageBox.Show("All fields must be filled in!", Assets.Custom.MessageBox.Basic.Titles.Information, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing); 

                return;
            }

            if (CustomMessageBox.Show("Are you sure you want to add a new user to the system?", Assets.Custom.MessageBox.Basic.Titles.Ask, Assets.Custom.MessageBox.Basic.Buttons.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Cancel))
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
                NewUser.Login = "example";
                NewUser.Password = "example";
                NewUser.Id = "example";

                try
                {

                    if (await _userController.Create(NewUser))
                    {
                        CustomMessageBox.Show("New user successfully added to the system!", Assets.Custom.MessageBox.Basic.Titles.Confirm, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                        LoadInfo();

                        LogManager.SaveLog("Server", DateTime.Today, $"AccountantMode: data about {NewUser.FIO} successfully create.");
                    }
                    else
                    {
                        CustomMessageBox.Show("Failed to create new user, try again later.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
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
                    CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);

                    return;
                }

                officeList.ToList().ForEach(OfficeList.Add);
                specList.ToList().ForEach(SpecializationList.Add);

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Error server!", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
        }
    }
}
