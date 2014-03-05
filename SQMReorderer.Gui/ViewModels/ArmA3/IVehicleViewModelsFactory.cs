using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IVehicleViewModelsFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }
}