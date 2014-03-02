using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IGroupViewModelsFactory
    {
        List<GroupViewModel> Create(List<Vehicle> vehicles);
    }
}