using System.Collections.ObjectModel;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestBase
{
    abstract class Test : NavigateVM
    {
        public ObservableCollection<OneCollection> WordCollection { get; }

        public Test(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            CheckUserCollectionForNull(user.Collections.GetCollections());
            WordCollection = FindCheckedCollections(user.Collections.GetCollections());
            CheckCheckedCollectionsForNull(WordCollection);
        }

        public int AmountWords => WordCollection.Count;

        private ObservableCollection<OneCollection> FindCheckedCollections(ObservableCollection<OneCollection> userCollection)
        {
            ObservableCollection<OneCollection> checkedCollection = new ObservableCollection<OneCollection>();
            for(int i = 0; i < userCollection.Count; i++)
            {
                if(userCollection[i].IsChecked == true)
                {
                    checkedCollection.Add(userCollection[i]);
                }
            }
            return checkedCollection;
        }
        private void CheckCheckedCollectionsForNull(ObservableCollection<OneCollection> collection)
        {
            if(collection == null)
            {
                InformationMessage(Properties.Resources.WithoutCheckedCollections);
                Manager.GoTo("MainPageVM");
            }
        }
        private void CheckUserCollectionForNull(ObservableCollection<OneCollection> collection)
        {
            if(collection == null)
            {
                InformationMessage(Properties.Resources.UserWithoutCollections);
                Manager.GoTo("MainPageVM");
            }
        }
    }
}
