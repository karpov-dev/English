using Coursework.Models;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.ViewModels.PagesVM.Tests;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager
{
    class TestManager : NavigateVM
    {
        User _currentUser;
        public TestManager(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            _currentUser = user;
        }
    }
}