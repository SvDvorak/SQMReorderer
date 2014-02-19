using System.Collections.Generic;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IVehicleViewModelFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }

    public class VehicleViewModelFactory : IVehicleViewModelFactory
    {
        public List<VehicleViewModel> Create(List<Vehicle> vehicles)
        {
            throw new System.NotImplementedException();
        }
    }
}