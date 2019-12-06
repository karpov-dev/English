using Coursework.ViewModels.Base;
using Coursework.Models;

namespace Coursework.ViewModels.ViewModels
{
    class MainPageVM : BaseViewModel
    {
        private User CurrentUser;
        public MainPageVM(string viewModelName, ViewModelsManager manager) : base(viewModelName, manager)
        {
            CurrentUser = Manager.CurrentUser;
        }
    }
}
