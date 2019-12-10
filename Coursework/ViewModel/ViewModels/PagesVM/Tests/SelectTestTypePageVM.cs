using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.Models;
using Coursework.Models.Classes.Commands;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests
{
    class SelectTestTypePageVM : NavigateVM
    {
        private User _currentUser;
        private RelayCommand _backCommand;

        public SelectTestTypePageVM(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            _currentUser = user;
        }

        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ??
                    (_backCommand = new RelayCommand(obj =>
                    {
                        Manager.GoTo("MainPageVM");
                    }));
            }
        }
    }
}
