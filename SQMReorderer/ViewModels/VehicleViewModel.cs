using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class VehicleViewModel : ObservableCollection<VehicleViewModel>
    {
        private readonly Vehicle _vehicle;

        public VehicleViewModel(Vehicle vehicle, List<VehicleViewModel> childItemViewModels) : base(childItemViewModels)
        {
            _vehicle = vehicle;
        }

        public string Header { get { return _vehicle.Side; } }

        public string Vehicle { get { return _vehicle.VehicleName; } set { _vehicle.VehicleName = value; } }
        public string Rank { get { return _vehicle.Rank; } set { _vehicle.Rank = value; } }
        public string Text { get { return _vehicle.Text; } set { _vehicle.Text = value; } }
        public string Description { get { return _vehicle.Description; } set { _vehicle.Description = value; } }
    }
}
