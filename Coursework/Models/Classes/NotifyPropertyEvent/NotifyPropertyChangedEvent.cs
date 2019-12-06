using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Coursework.Models.Classes.NotifyPropertyEvent
{
    class NotifyPropertyChangedEvent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
