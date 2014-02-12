using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class ViewModelToContentReorderer
    {
        private Dictionary<Vehicle, List<Vehicle>> _parentChildDictionary;

        public void Reorder(MissionState mission, MissionViewModel viewModel)
        {
            _parentChildDictionary = new Dictionary<Vehicle, List<Vehicle>>();
            AddToDictionary(viewModel.Groups.ToList());

            UpdateOrder(mission.Groups);
        }

        private void AddToDictionary(IEnumerable<VehicleViewModel> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                _parentChildDictionary.Add(vehicle.Vehicle, vehicle.Children.Select(x => x.Vehicle).ToList());

                AddToDictionary(vehicle.Children.ToList());
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