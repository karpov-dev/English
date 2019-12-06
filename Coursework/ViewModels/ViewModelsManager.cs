using Coursework.ViewModels.Base;
using Coursework.ViewModels.ViewModels;
using Coursework.Models;

namespace Coursework.ViewModels
{
    class ViewModelsManager : BaseViewModelManager
    {
        public User CurrentUser;
        public ViewModelsManager()
        {
            AddViewModel(new MainPageVM("MainPageVM", this));
            CurrentUser = new User();
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
