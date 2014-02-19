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
            var teamViewModels = new List<TeamViewModel>();
            var teamGroups = vehicles.GroupBy(x => x.Side);

            foreach (var teamGroup in teamGroups)
            {
                var teamViewModel = new TeamViewModel();
                teamViewModel.Side = GetTeamName(teamGroup.First().Side);

                teamViewModel.Groups = new ObservableCollection<GroupViewModel>(
                    _groupViewModelsFactory.Create(teamGroup.ToList()));

                teamViewModels.Add(teamViewModel);
            }

            return teamViewModels;
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
    }
}