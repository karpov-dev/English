using System;
using System.Collections.ObjectModel;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestBase;
using Coursework.ViewModel.MangerOfNavigate;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestBase
{
    class WordsTypeVM : Test
    {
        private User _currentUser;
        private Random random = new Random();

        public WordsTypeVM(User user, NavigateManager manager) : base(user, manager)
        {
            _currentUser = user;
        }
    }
}
