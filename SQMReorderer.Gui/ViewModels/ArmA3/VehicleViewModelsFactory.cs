using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import.ArmA3.ResultObjects;
using SQMImportExport.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class VehicleViewModelsFactory : IVehicleViewModelsFactory
    {
        public IEnumerable<VehicleViewModel> Create(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModelBase>()));
        }
    }
}