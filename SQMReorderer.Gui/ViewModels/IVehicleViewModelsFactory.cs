using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IVehicleViewModelsFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }
}