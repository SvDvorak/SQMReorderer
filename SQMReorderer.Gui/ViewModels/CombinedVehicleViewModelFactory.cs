using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Gui.Dialogs.AddInit;

namespace SQMReorderer.Gui.ViewModels
{
    public class CombinedVehicleViewModelFactory
    {
        private readonly IAddInitDialogFactory _addInitDialogFactory;

        public CombinedVehicleViewModelFactory(IAddInitDialogFactory addInitDialogFactory)
        {
            _addInitDialogFactory = addInitDialogFactory;
        }

        public ICombinedVehicleViewModel Create(List<IVehicleViewModel> vehicleViewModels)
        {
            if (vehicleViewModels.Count == 0)
            {
                return null;
            }

            // TODO: This is uuuuugly, find a solution
            if (vehicleViewModels[0] is ArmA2.VehicleViewModel)
            {
                return new ArmA2.CombinedVehicleViewModel(vehicleViewModels.Cast<ArmA2.VehicleViewModel>().ToList(), _addInitDialogFactory);
            }
            else
            {
                return new ArmA3.CombinedVehicleViewModel(vehicleViewModels.Cast<ArmA3.VehicleViewModel>().ToList(), _addInitDialogFactory);
            }
        }
    }
}