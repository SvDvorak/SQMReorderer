using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class VehicleViewModelsFactory : IVehicleViewModelsFactory
    {
        public List<VehicleViewModel> Create(List<Vehicle> vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModel>())).ToList();
        }
    }
}