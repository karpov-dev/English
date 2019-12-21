using System.Windows;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Views.Notification;

namespace Coursework.ViewModel.ViewModels.PagesVM.Notification
{
    class NotificationWindow 
    {
        private RelayCommand _buttonWindowPath;
        private NavigateManager _manager;
        private object _nextWindow;

        public NotificationWindow(NavigateManager manager, string title, string text, string imagePath, string 
            buttonText = null, object buttonPath = null)
        {
            NotificationTitle = title;
            NotificationText = text;
            ImagePath = imagePath;
            NotificationButton = buttonText;
            _nextWindow = buttonPath;
            _manager = manager;
        }

        public string NotificationTitle { get; private set; }
        public string ImagePath { get; private set; }
        public string NotificationText { get; private set; }
        public string NotificationButton { get; private set; }

        public RelayCommand GoToUserPage
        {
            get
            {
                return _buttonWindowPath ??
                    ( _buttonWindowPath = new RelayCommand(obj =>
                     {
                         _manager.CurrentViewModel = _nextWindow;
                         
                     }));
            }
        }
    }
}
