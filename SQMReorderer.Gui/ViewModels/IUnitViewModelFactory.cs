using System.Collections.Generic;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IUnitViewModelFactory
    {
        List<VehicleViewModel> Create(List<Vehicle> vehicles);
    }
}