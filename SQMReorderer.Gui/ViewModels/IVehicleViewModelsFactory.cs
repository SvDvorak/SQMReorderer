using System.Collections.Generic;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IVehicleViewModelsFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }
}