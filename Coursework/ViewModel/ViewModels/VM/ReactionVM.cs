using Coursework.Models.Classes.Events;
using System;

namespace Coursework.ViewModel.ViewModels.VM
{
    [Serializable]
    class ReactionVM : Event
    {
        private string _reaction;
        public ReactionVM()
        {
            Reaction = "";
        }
        public string Reaction
        {
            get => _reaction;
            set
            {
                _reaction = value;
                OnPropertyChanged("Reaction");
            }
        }
    }
}
