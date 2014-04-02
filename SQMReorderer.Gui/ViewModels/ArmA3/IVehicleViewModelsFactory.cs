using System.Collections.Generic;
using SQMImportExport.ArmA3;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IVehicleViewModelsFactory
    {
        IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles);
    }
}