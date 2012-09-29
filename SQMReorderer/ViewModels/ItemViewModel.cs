using System;
using System.Collections.ObjectModel;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class ItemViewModel
    {
        private readonly Vehicle _vehicle;

        public ItemViewModel(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public ObservableCollection<ItemViewModel> Items { get; set; }

        public static event Action<bool, ItemViewModel> SelectedItemChanged;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if(SelectedItemChanged != null)
                {
                    SelectedItemChanged(_isSelected, this);
                }
            }
        }

        public string Side
        {
            get { return _vehicle.Side; }
            set { _vehicle.Side = value; }
        }

        public string Vehicle
        {
            get { return _vehicle.VehicleName; }
            set { _vehicle.VehicleName = value; }
        }

        public string Rank
        {
            get { return _vehicle.Rank; }
            set { _vehicle.Rank = value; }
        }

        public string Text
        {
            get { return _vehicle.Text; }
            set { _vehicle.Text = value; }
        }

        public string Description
        {
            get { return _vehicle.Description; }
            set { _vehicle.Description = value; }
        }
    }
}
