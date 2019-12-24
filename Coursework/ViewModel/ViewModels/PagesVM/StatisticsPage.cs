using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.Information;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class StatisticsPage : Event
    {
        private ManagerVM _owner;
        private User _user;

        private RelayCommand _backButton;

        public StatisticsPage(ManagerVM owner, User user)
        {
            _owner = owner;
            _user = user;
        }


        public ObservableCollection<OneCollection> UserCollections => _user.Collections.GetCollections();

        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    ( _backButton = new RelayCommand(obj =>
                     {
                         _owner.GoTo("MainPageVM");
                     }) );
            }
        }
    }
}
