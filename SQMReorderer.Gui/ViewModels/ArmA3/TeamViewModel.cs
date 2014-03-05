using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class TeamViewModel : ITeamViewModel
    {
        public TeamViewModel()
        {
            Groups = new ObservableCollection<GroupViewModel>();
        }

        public string Side { get; set; }

        public ObservableCollection<GroupViewModel> Groups { get; set; }

        public List<Type> ChildTypes
        {
            get
            {
                return new List<Type>()
                    {
                        typeof (GroupViewModel)
                    };
            }
        }
    }
}