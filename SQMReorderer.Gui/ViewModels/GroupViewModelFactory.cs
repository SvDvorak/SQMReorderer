using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class GroupViewModelsFactory : IGroupViewModelsFactory
    {
        private readonly IVehicleViewModelsFactory _vehicleViewModelsFactory;

        private int _groupEnumerator = 0;

        public GroupViewModelsFactory(IVehicleViewModelsFactory vehicleViewModelsFactory)
        {
            _vehicleViewModelsFactory = vehicleViewModelsFactory;
        }

        public List<GroupViewModel> Create(List<Vehicle> vehicles)
        {
            _groupEnumerator = 0;

            return vehicles
                .Select(vehicle => new GroupViewModel
                    {
                        Name = GetGroupName(),
                        Vehicles = new ObservableCollection<VehicleViewModel>(
                            _vehicleViewModelsFactory.Create(vehicle.Vehicles))
                    })
                .ToList();
        }

        private string GetGroupName()
        {
            _groupEnumerator += 1;
            return "Group " + _groupEnumerator;
        }
    }
}