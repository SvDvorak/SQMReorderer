using System.Collections.Generic;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IVehicleViewModelsFactory
    {
        IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles);
    }
}