using System.Collections.Generic;
using System.Linq;

namespace SQMReorderer.Gui.ViewModels
{
    public class CombinedVehicleViewModelFactory
    {
        public ICombinedVehicleViewModel Create(List<IVehicleViewModel> vehicleViewModels)
        {
            if (vehicleViewModels.Count == 0)
            {
                return null;
            }

            // TODO: This is uuuuugly, find a solution
            if (vehicleViewModels[0] is ArmA2.VehicleViewModel)
            {
                return new ArmA2.CombinedVehicleViewModel(vehicleViewModels.Cast<ArmA2.VehicleViewModel>().ToList());
            }
            else
            {
                return new ArmA3.CombinedVehicleViewModel(vehicleViewModels.Cast<ArmA3.VehicleViewModel>().ToList());
            }
        }
    }
}