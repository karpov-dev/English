using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.ViewModels.PagesVM;
using Coursework.ViewModel.ViewModels.PagesVM.ApplictionSettings;
using Coursework.Models;

namespace Coursework.ViewModel.MangerOfNavigate
{
    class NavigateManager : ManagerVM
    {
        public User CurrentUser;
        public NavigateManager()
        {
            CurrentUser = new User();
            AddViewModel(new MainPageVM("MainPageVM", this, CurrentUser));
            AddViewModel(new AppSettings("Settings", this, CurrentUser));
        }

        public object CurrentViewModel
        {
            get
            {
                if (_currentViewModel == null)
                {
                    _currentViewModel = _viewModels[0];
                    CurrentViewModel = _currentViewModel;
                }

                if (_currentViewModel == this)
                {
                    System.Windows.Application.Current.Shutdown();
                }

                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
    }
}
