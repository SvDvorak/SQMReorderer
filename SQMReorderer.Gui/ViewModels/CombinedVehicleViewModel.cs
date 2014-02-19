using System;
using System.Collections.Generic;
using System.Linq;

namespace SQMReorderer.Gui.ViewModels
{
    public class CombinedVehicleViewModel : ViewModelBase
    {
        private readonly List<VehicleViewModel> _vehicles;

        public CombinedVehicleViewModel(List<VehicleViewModel> vehicles)
        {
            _vehicles = vehicles;
        }

        public string VehicleName
        {
            get { return GetCombinedValue(x => x.VehicleName); }
            set { Set(value, () => VehicleName, () => SetCombinedValue(x => x.VehicleName = value)); }
        }

        public string Rank
        {
            get { return GetCombinedValue(x => x.Rank); }
            set { SetCombinedValue(x => x.Rank = value); }
        }

        public string Text
        {
            get { return GetCombinedValue(x => x.Text); }
            set { SetCombinedValue(x => x.Text = value); }
        }

        public string Description
        {
            get { return GetCombinedValue(x => x.Description); }
            set { Set(value, () => Description, () => SetCombinedValue(x => x.Description = value)); }
        }

        private string GetCombinedValue(Func<VehicleViewModel, string> getValueFunc)
        {
            if (_vehicles.Count == 0)
            {
                return null;
            }

            var firstValue = getValueFunc(_vehicles.First());

            if (_vehicles.Any(x => getValueFunc(x) != firstValue))
            {
                return null;
            }

            return firstValue;
        }

        private void SetCombinedValue(Action<VehicleViewModel> setValueFunc)
        {
            _vehicles.ForEach(setValueFunc);
        }
    }
}