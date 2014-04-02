using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class TeamViewModelsFactory : ITeamViewModelsFactory
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

        private ObservableCollection<IGroupViewModel> CreateGroups(List<Vehicle> teamGroups)
        {
            return new ObservableCollection<IGroupViewModel>(_groupViewModelsFactory.Create(teamGroups));
        }
    }
}