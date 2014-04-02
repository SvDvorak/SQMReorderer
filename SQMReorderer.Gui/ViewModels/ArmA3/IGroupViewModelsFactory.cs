using System.Collections.Generic;
using SQMImportExport.ArmA3;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IGroupViewModelsFactory
    {
        List<GroupViewModel> Create(List<Vehicle> vehicles);
    }
}