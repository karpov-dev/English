using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace Coursework.Models.Classes.Events
{
    [Serializable]
    class Event : INotifyPropertyChanged
    {
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
