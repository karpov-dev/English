using System.Collections.Generic;
using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.ViewModels.Base
{
    abstract class BaseViewModelManager : NotifyPropertyChangedEvent
    {
        protected object _currentViewModel;
        protected List<BaseViewModel> _viewModels;

        public BaseViewModelManager()
        {
            _viewModels = new List<BaseViewModel>();
        }

        public void GoTo(string nextWindowName)
        {
            object nextViewModel = SuchViewModel(nextWindowName);
            if (nextViewModel == null )
            {
                _currentViewModel = this;
            }
            else
            {
                _currentViewModel = nextViewModel;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public void AddViewModel(BaseViewModel newViewModel)
        {
            _viewModels.Add(newViewModel);
        }
        public void GetViewModel(string viewModelName) => SuchViewModel(viewModelName);


        private BaseViewModel SuchViewModel(string viewModelName)
        {
            for (int i = 0; i < _viewModels.Count; i++)
            {
                if (_viewModels[i].ViewModelName == viewModelName)
                {
                    return _viewModels[i];
                }
            }
            return null;
        }

    }
}
