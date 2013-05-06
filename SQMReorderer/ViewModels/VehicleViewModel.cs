using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public override string ToString()
        {
            var textBuilder = new MultiLineTextBuilder();

            textBuilder.AddLine(_vehicle.Side);
            //textBuilder.AddLine(_vehicle.VehicleName);
            //textBuilder.AddLine(Rank);
            //textBuilder.AddLine(Text);
            //textBuilder.AddLine(Description);

            return textBuilder.ToString();
        }
    }
}
