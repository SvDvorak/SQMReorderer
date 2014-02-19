using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class ViewModelToContentReorderer
    {
        private Dictionary<Vehicle, List<Vehicle>> _parentChildDictionary;

        public void Reorder(MissionState mission, List<TeamViewModel> teamViewModels)
        {
            _parentChildDictionary = new Dictionary<Vehicle, List<Vehicle>>();

            foreach (var teamViewModel in teamViewModels)
            {
                AddToDictionary(teamViewModel.Groups.ToList());
            }

            UpdateOrder(mission.Groups);
        }

        private void AddToDictionary(IEnumerable<GroupViewModel> groups)
        {
            foreach (var group in groups)
            {
                _parentChildDictionary.Add(group.ConnectedVehicle, group.Vehicles.Select(x => x.Vehicle).ToList());
            }
        }

        private void UpdateOrder(IEnumerable<Vehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                if(_parentChildDictionary.ContainsKey(vehicle))
                {
                    vehicle.Vehicles = _parentChildDictionary[vehicle];
                }

                UpdateOrder(vehicle.Vehicles);
            }
        }
    }
}