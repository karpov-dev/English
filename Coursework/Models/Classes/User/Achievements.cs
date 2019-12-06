using System.Collections.ObjectModel;
using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.Models.Classes.User
{
    class Achievements : NotifyPropertyChangedEvent
    {
        private ObservableCollection<OneAchievement> _allAchievements;
        private ObservableCollection<OneAchievement> _progressAchievements;
    }
}
