using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class TeamViewModelsFactory
    {
        private readonly IGroupViewModelsFactory _groupViewModelsFactory;

        public TeamViewModelsFactory(IGroupViewModelsFactory groupViewModelsFactory)
        {
            _groupViewModelsFactory = groupViewModelsFactory;
        }

        public List<TeamViewModel> Create(List<Vehicle> vehicles)
        {
            var teamGroups = vehicles.GroupBy(x => x.Side);

            return teamGroups
                .Select(teamGroup => CreateTeamViewModel(teamGroup.ToList())).ToList();
        }

        private TeamViewModel CreateTeamViewModel(List<Vehicle> teamGroups)
        {
            return new TeamViewModel
                {
                    Side = GetTeamName(teamGroups.First().Side),
                    Groups = CreateGroups(teamGroups)
                };
        }

        private string GetTeamName(string side)
        {
            if (side == "WEST")
            {
                return "BLUFOR";
            }
            if (side == "EAST")
            {
                return "OPFOR";
            }
            if (side == "GUER")
            {
                return "INDEPENDENT";
            }

            return "CIVILIAN";
        }

        private ObservableCollection<GroupViewModel> CreateGroups(List<Vehicle> teamGroups)
        {
            return new ObservableCollection<GroupViewModel>(_groupViewModelsFactory.Create(teamGroups));
        }
    }
}