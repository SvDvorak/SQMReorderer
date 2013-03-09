using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class VehicleViewModel : StructureItemViewModelBase
    {
        private readonly Vehicle _vehicle;

        public VehicleViewModel(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public string Side
        {
            get { return _vehicle.Side; }
            set { Set(value, () => Side, () => _vehicle.Side = value); }
        }

        public string Vehicle
        {
            get { return _vehicle.VehicleName; }
            set { Set(value, () => Vehicle, () => _vehicle.VehicleName = value); }
        }

        public string Rank
        {
            get { return _vehicle.Rank; }
            set { Set(value, () => Rank, () => _vehicle.Rank = value); }
        }

        public string Text
        {
            get { return _vehicle.Text; }
            set { Set(value, () => Text, () => _vehicle.Text = value); }
        }

        public string Description
        {
            get { return _vehicle.Description; }
            set { Set(value, () => Description, () => _vehicle.Description = value); }
        }

        public override string ToString()
        {
            var textBuilder = new MultiLineTextBuilder();

            textBuilder.AddLine(Side);
            textBuilder.AddLine(Vehicle);
            textBuilder.AddLine(Rank);
            textBuilder.AddLine(Text);
            textBuilder.AddLine(Description);

            return textBuilder.ToString();
        }
    }
}
