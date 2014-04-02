using System.Collections.Generic;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IVehicleViewModelsFactory
    {
        IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles);
    }
}