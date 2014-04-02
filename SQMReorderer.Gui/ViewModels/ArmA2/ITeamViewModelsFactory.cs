using System.Collections.Generic;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface ITeamViewModelsFactory
    {
        List<TeamViewModel> Create(List<Vehicle> vehicles);
    }
}