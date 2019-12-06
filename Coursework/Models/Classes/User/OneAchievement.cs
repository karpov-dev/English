using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.Models.Classes.User
{
    class OneAchievement : NotifyPropertyChangedEvent
    {
        private string _title;
        private string _description;
        private int _experience;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                OnPropertyChanged("Experience");
            }
        }
    }
}
