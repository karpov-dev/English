using System.Windows;
using Coursework.Properties;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.MangerOfNavigate;


namespace Coursework.ViewModel.NavigateBase
{
    abstract class NavigateVM : EventVM
    {
        private RelayCommand _goToNextWindowCommand;
        private RelayCommand _exitCommand;
        public string ViewModelName { get; }
        public NavigateManager Manager { get; }

        public NavigateVM(string viewModelName, NavigateManager navigateManager)
        {
            ViewModelName = viewModelName;
            Manager = navigateManager;
        }
        public NavigateVM(NavigateManager navigateManager)
        {
            Manager = navigateManager;
        }

        public object GoToWindow
        {
            get
            {
                return _goToNextWindowCommand ??
                    (_goToNextWindowCommand = new RelayCommand(obj =>
                    {
                        string nextWindow = obj as string;
                        Manager.GoTo(nextWindow);
                    }));
            }
        }
        public void ErrorMessage(string text, string title = "")
        {
            title = Resources.Error;
            MessageBoxResult message = MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void WarningMessage(string text, string tittl = "")
        {
            tittl = Resources.Warning;
            MessageBoxResult message = MessageBox.Show(text, tittl, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void InformationMessage(string text, string title = "")
        {
            title = Properties.Resources.Information;
            MessageBoxResult message = MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public RelayCommand Exit
        {
            get
            {
                return _exitCommand ??
                    (_exitCommand = new RelayCommand(obj =>
                    {
                        System.Windows.Application.Current.Shutdown();
                    }));
            }
        }
    }
}
