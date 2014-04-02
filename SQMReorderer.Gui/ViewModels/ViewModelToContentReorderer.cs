using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Common;
using SQMImportExport.Import;
using SQMReorderer.Gui.ViewModels.ArmA3;

namespace SQMReorderer.Gui.ViewModels
{
    public class ViewModelToContentReorderer : IViewModelToContentReorderer
    {
        private Dictionary<VehicleBase, VehiclePositionInfo> _parentChildDictionary;
        private int _continousGroupIndex;

        public void Reorder(MissionStateBase mission, List<ITeamViewModel> teamViewModels)
        {
            _parentChildDictionary = new Dictionary<VehicleBase, VehiclePositionInfo>();
            _continousGroupIndex = 0;

            foreach (var teamViewModel in teamViewModels)
            {
                AddToDictionary(teamViewModel.Groups.ToList());
            }

            UpdateOrder(mission.Groups);
        }

        private void AddToDictionary(List<IGroupViewModel> groups)
        {
            for (var index = 0; index < groups.Count; index++)
            {
                var groupViewModel = groups[index];
                _parentChildDictionary.Add(
                    groupViewModel.ConnectedVehicle,
                    new VehiclePositionInfo(index + _continousGroupIndex, groupViewModel.Vehicles.Select(x => x.Vehicle).ToList()));

                AddToDictionary(groupViewModel.Vehicles.ToList());
            }

            _continousGroupIndex = groups.Count;
        }

        private void AddToDictionary(List<VehicleViewModelBase> vehicles)
        {
            for (var index = 0; index < vehicles.Count; index++)
            {
                var groupViewModel = vehicles[index];
                _parentChildDictionary.Add(
                    groupViewModel.Vehicle,
                    new VehiclePositionInfo(index, new List<VehicleBase>()));
            }
        }

        private void UpdateOrder(IEnumerable<VehicleBase> vehicles)
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
            public VehiclePositionInfo(int number, List<VehicleBase> children)
            {
                Number = number;
                Children = children;
            }

            public int Number { get; private set; }
            public List<VehicleBase> Children { get; private set; } 
        }
    }
}