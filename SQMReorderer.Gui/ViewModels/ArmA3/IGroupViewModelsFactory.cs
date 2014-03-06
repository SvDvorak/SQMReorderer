using System.Collections.Generic;
using SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IGroupViewModelsFactory
    {
        List<GroupViewModel> Create(List<Vehicle> vehicles);
    }
}