using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.Models.Classes.User
{
    class OneCollection : NotifyPropertyChangedEvent
    {
        private string _title;
        private string _description;

        public string Title
        {
            get => _title;
            set
            {
                _description = value;
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
    }
}
