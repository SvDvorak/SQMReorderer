using System.Collections.Generic;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface ITeamViewModelsFactory
    {
        List<TeamViewModel> Create(List<Vehicle> vehicles);
    }
}