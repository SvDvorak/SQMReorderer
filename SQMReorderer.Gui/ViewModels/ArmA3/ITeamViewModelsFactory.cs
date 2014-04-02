using System.Collections.Generic;
using SQMImportExport.ArmA3;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface ITeamViewModelsFactory
    {
        List<TeamViewModel> Create(List<Vehicle> vehicles);
    }
}