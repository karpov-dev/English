using System.Collections.Generic;

namespace Coursework.ViewModel.NavigateBase
{
    abstract class ManagerVM : EventVM
    {
        protected object _currentViewModel;
        protected List<NavigateVM> _viewModels;

        public ManagerVM()
        {
            _viewModels = new List<NavigateVM>();
        }

        public void GoTo(string nextWindowName)
        {
            object nextViewModel = SuchViewModel(nextWindowName);
            if (nextViewModel == null)
            {
                _currentViewModel = this;
            }
            else
            {
                _currentViewModel = nextViewModel;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public void AddViewModel(NavigateVM newViewModel)
        {
            _viewModels.Add(newViewModel);
        }
        public void GetViewModel(string viewModelName) => SuchViewModel(viewModelName);


        private NavigateVM SuchViewModel(string viewModelName)
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
