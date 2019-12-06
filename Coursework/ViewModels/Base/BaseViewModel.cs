using System;
using System.Windows;
using Coursework.Properties;
using Coursework.Models.Classes.NotifyPropertyEvent;
using Coursework.Models.Classes.Commands;

namespace Coursework.ViewModels.Base
{
    abstract class BaseViewModel : NotifyPropertyChangedEvent
    {
        private RelayCommand _goToNextWindowCommand;
        private RelayCommand _exitCommand;
        public string ViewModelName { get; }
        public ViewModelsManager Manager { get; }

        public BaseViewModel(string viewModelName, ViewModelsManager manager)
        {
            ViewModelName = viewModelName;
            Manager = manager;
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
