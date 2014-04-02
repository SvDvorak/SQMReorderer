using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class GroupViewModelsFactory : IGroupViewModelsFactory
    {
        private readonly IVehicleViewModelsFactory _vehicleViewModelsFactory;

        private int _groupEnumerator;

        public GroupViewModelsFactory(IVehicleViewModelsFactory vehicleViewModelsFactory)
        {
            _vehicleViewModelsFactory = vehicleViewModelsFactory;
        }

        public List<GroupViewModel> Create(List<Vehicle> vehicles)
        {
            _groupEnumerator = 0;

            return vehicles
                .Select(Create)
                .ToList();
        }

        private GroupViewModel Create(Vehicle vehicle)
        {
            return new GroupViewModel
                {
                    Name = GetGroupName(),
                    ConnectedVehicle = vehicle,
                    Vehicles = CreateVehicles(vehicle)
                };
        }

        private string GetGroupName()
        {
            _groupEnumerator += 1;

            return "Group " + _groupEnumerator;
        }

        private ObservableCollection<VehicleViewModelBase> CreateVehicles(Vehicle vehicle)
        {
            return new ObservableCollection<VehicleViewModelBase>(
                _vehicleViewModelsFactory.Create(vehicle.Vehicles));
        }
    }
}