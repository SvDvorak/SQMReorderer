using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class ViewModelToContentReorderer : IViewModelToContentReorderer
    {
        private Dictionary<Vehicle, VehiclePositionInfo> _parentChildDictionary;

        public void Reorder(MissionState mission, List<TeamViewModel> teamViewModels)
        {
            _parentChildDictionary = new Dictionary<Vehicle, VehiclePositionInfo>();

            foreach (var teamViewModel in teamViewModels)
            {
                AddToDictionary(teamViewModel.Groups.ToList());
            }

            UpdateOrder(mission.Groups);
        }

        private void AddToDictionary(List<GroupViewModel> groups)
        {
            for (int index = 0; index < groups.Count; index++)
            {
                var groupViewModel = groups[index];
                _parentChildDictionary.Add(
                    groupViewModel.ConnectedVehicle,
                    new VehiclePositionInfo(index, groupViewModel.Vehicles.Select(x => x.Vehicle).ToList()));

                AddToDictionary(groupViewModel.Vehicles.ToList());
            }
        }

        private void AddToDictionary(List<VehicleViewModel> vehicles)
        {
            for (int index = 0; index < vehicles.Count; index++)
            {
                var groupViewModel = vehicles[index];
                _parentChildDictionary.Add(
                    groupViewModel.Vehicle,
                    new VehiclePositionInfo(index, new List<Vehicle>()));
            }
        }

        private void UpdateOrder(List<Vehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                if(_parentChildDictionary.ContainsKey(vehicle))
                {
                    var vehiclePositionInfo = _parentChildDictionary[vehicle];
                    vehicle.Number = vehiclePositionInfo.Number;
                    vehicle.Vehicles = vehiclePositionInfo.Children;
                }

                UpdateOrder(vehicle.Vehicles);
            }
        }

        private class VehiclePositionInfo
        {
            public VehiclePositionInfo(int number, List<Vehicle> children)
            {
                Number = number;
                Children = children;
            }

            public int Number { get; set; }
            public List<Vehicle> Children { get; set; } 
        }
    }
}