using System.Collections.Generic;
using System.Linq;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class VehicleViewModelsFactory : IVehicleViewModelsFactory
    {
        public IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModelBase>()));
        }
    }
}