using Coursework.Models.Classes.Test;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.Statistics;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests
{
    class SessionResultPage : Event
    {
        private OneSessionStatistics _statistics;
        private TestManager _owner; 
        private RelayCommand _backButton;

        public SessionResultPage(OneSessionStatistics statistics, TestManager owner)
        {
            _statistics = statistics;
            _owner = owner;
        }

        public OneSessionStatistics Statistics => _statistics;

        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    (_backButton = new RelayCommand(obj =>
                     {
                         _owner.BackAndClean();
                     }));
            }
        }
    }
}
