using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class VehicleViewModelsFactory : IVehicleViewModelsFactory
    {
        public List<VehicleViewModel> Create(List<Vehicle> vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModel>())).ToList();
        }
    }
}