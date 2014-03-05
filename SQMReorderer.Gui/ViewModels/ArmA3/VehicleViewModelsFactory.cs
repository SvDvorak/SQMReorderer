using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class VehicleViewModelsFactory : IVehicleViewModelsFactory
    {
        public List<VehicleViewModel> Create(List<Vehicle> vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModel>())).ToList();
        }
    }
}