using System.Windows;
using Coursework.ViewModels;

namespace Coursework.Views
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelsManager();
        }
    }
}
