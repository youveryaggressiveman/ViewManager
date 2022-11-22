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
        private readonly CreateUserPageViewModelController _controller;

        private User _newUser;

        private ObservableCollection<Office> _officeList;
        private ObservableCollection<Specialization> _specializationList;

        private Specialization _selectedSpecialization;
        private Office _selectedOffice;

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

        public CreateUserPageViewModel()
        {
            CreateUserCommand = new DelegateCommand(CreateUser);

            _controller = new CreateUserPageViewModelController(ApiServerSingleton.GetConnectionApiString());

            SpecializationList = new ObservableCollection<Specialization>();
            OfficeList = new ObservableCollection<Office>();

            NewUser = new User();

            LoadInfo();
        }

        private async void CreateUser(object obj)
        {
            
        }

        private async void LoadInfo()
        {
            try
            {
                var officeList = await _controller.GetOfficeList();
                var specList = await _controller.GetSpecializationList();

                if (officeList == null || specList == null)
                {
                    MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                officeList.ToList().ForEach(OfficeList.Add);
                specList.ToList().ForEach(SpecializationList.Add);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
