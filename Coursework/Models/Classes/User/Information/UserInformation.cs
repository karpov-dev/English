using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.Information
{
    [Serializable]
    class UserInformation : Event
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
