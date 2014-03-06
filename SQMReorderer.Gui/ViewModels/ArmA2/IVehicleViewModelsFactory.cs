using System.Collections.Generic;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IVehicleViewModelsFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }
}