using System.Collections.Generic;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IGroupViewModelsFactory
    {
        List<GroupViewModel> Create(List<Vehicle> vehicles);
    }
}