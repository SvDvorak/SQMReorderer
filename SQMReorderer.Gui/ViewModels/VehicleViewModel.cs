using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        public VehicleViewModel(Vehicle vehicle, List<VehicleViewModel> childItemViewModels)
        {
            Vehicle = vehicle;
            Children = new ObservableCollection<VehicleViewModel>(childItemViewModels);
        }

        public Vehicle Vehicle { get; private set; }

        public string Header { get { return GetPropertyWithData(); } }

        public string VehicleName
        {
            get { return Vehicle.VehicleName; }
            set { Set(value, () => VehicleName, () => Vehicle.VehicleName = value); }
        }

        public string Rank
        {
            get { return Vehicle.Rank; }
            set { Set(value, () => Rank, () => Vehicle.Rank = value); }
        }

        public string Text
        {
            get { return Vehicle.Text; }
            set { Set(value, () => Text, () => Vehicle.Text = value); }
        }

        public string Description
        {
            get { return Vehicle.Description; }
            set { Set(value, () => Description, () => Vehicle.Description = value); }
        }

        public ObservableCollection<VehicleViewModel> Children { get; set; }

        private string GetPropertyWithData()
        {
            if (!string.IsNullOrWhiteSpace(Description))
            {
                return Description;
            }
            if (!string.IsNullOrWhiteSpace(VehicleName))
            {
                return VehicleName;
            }

            return Vehicle.Side;
        }
    }
}
