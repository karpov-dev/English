using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.Information;
using Coursework.Models;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class HelloPageVM : NavigateVM
    {
        private RelayCommand _nextButton;
        private Settings _userInformation;

        public HelloPageVM(Settings userInformation, string viewModelName, NavigateManager navigateManager) : base(viewModelName, navigateManager)
        {
            _userInformation = userInformation;
        }

        public string UserName { set; get; }

        public RelayCommand NextCommand
        {
            get
            {
                return _nextButton ??
                    (_nextButton = new RelayCommand(obj =>
                    {
                        if (UserName == "" || UserName == null)
                        {
                            InformationMessage(Properties.Resources.SayYourName);
                        }
                        else
                        {
                            _userInformation.Name = UserName;
                            Manager.GoTo("MainPageVM");
                        }
                    }));
            }
        }
    }
}
