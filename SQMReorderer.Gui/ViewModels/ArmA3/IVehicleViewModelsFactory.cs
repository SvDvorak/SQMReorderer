using System.Collections.Generic;
using SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IVehicleViewModelsFactory
    {
        IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles);
    }
}