using ClientApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModelController _controller;

        public MainWindowViewModel()
        {
            _controller = new MainWindowViewModelController();
        }
    }
}
