using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class VehicleViewModel : ObservableCollection<VehicleViewModel>
    {
        public VehicleViewModel(Vehicle vehicle, List<VehicleViewModel> childItemViewModels) : base(childItemViewModels)
        {
            Vehicle = vehicle;
        }

        public Vehicle Vehicle { get; private set; }

        public string Header { get { return Vehicle.Side; } }

        public string VehicleName { get { return Vehicle.VehicleName; } set { Vehicle.VehicleName = value; } }
        public string Rank { get { return Vehicle.Rank; } set { Vehicle.Rank = value; } }
        public string Text { get { return Vehicle.Text; } set { Vehicle.Text = value; } }
        public string Description { get { return Vehicle.Description; } set { Vehicle.Description = value; } }

    }
}
