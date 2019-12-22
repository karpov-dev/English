using System.Collections.ObjectModel;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models.Classes.User.Information;

namespace Coursework.ViewModel.ViewModels.PagesVM.Develop
{
    class TestReactions : NavigateVM
    {
        private ReactionVM _selectedReaction;

        private RelayCommand _backButton;
        private RelayCommand _addReaction;
        private RelayCommand _deleteReaction;

        public TestReactions(NavigateManager navigateManager, ObservableCollection<ReactionVM> reactions, string reactionType) : base(navigateManager)
        {
            Type = reactionType;
            Reactions = reactions;
            SetSelectedReactionFirst();
        }

        public string Type { get; private set; }
        public ObservableCollection<ReactionVM> Reactions { get; set; }
        public ReactionVM SelectedReaction
        {
            get => _selectedReaction;
            set
            {
                _selectedReaction = value;
                OnPropertyChanged("SelectedReaction");
            }
        }

        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    ( _backButton = new RelayCommand(obj =>
                     {
                         if(!СheckForEmptyReaction()) //проверка на пустоту в листе
                         {
                             Manager.GoTo("Settings");
                         }
                         else
                         {
                             ErrorMessage(Properties.Resources.EmptyFieldsError);
                         }
                     }) );
            }
        }
        public RelayCommand AddReaction
        {
            get
            {
                return _addReaction ??
                    ( _addReaction = new RelayCommand(obj =>
                     {
                         Reactions.Add(new ReactionVM());
                         SelectedReaction = Reactions[Reactions.Count - 1];
                     }) );
            }
        }
        public RelayCommand DeleteReaction
        {
            get
            {
                return _deleteReaction ??
                    ( _deleteReaction = new RelayCommand(obj =>
                     {
                         ReactionVM reaction = obj as ReactionVM;
                         Reactions.Remove(reaction);
                         SetSelectedReactionFirst();
                     }) );
            }
        }

        private bool СheckForEmptyReaction()
        {
            if(Reactions.Count >= 1 )
            {
                for ( int i = 0; i < Reactions.Count; i++ )
                {
                    if ( Reactions[i].Reaction == "" )
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        private void SetSelectedReactionFirst()
        {
            if(Reactions.Count > 0)
            {
                SelectedReaction = Reactions[0];
            }
        }
    }
}
