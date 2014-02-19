using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

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