﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class VehicleViewModel : VehicleViewModelBase
    {
        public VehicleViewModel(Vehicle vehicle, List<VehicleViewModelBase> childItemViewModels)
        {
            Vehicle = vehicle;
            Children = new ObservableCollection<VehicleViewModelBase>(childItemViewModels);
        }

        public new Vehicle Vehicle
        {
            get { return (Vehicle) base.Vehicle; }
            private set { base.Vehicle = value; }
        }

        public string Header { get { return GetPropertyWithData(); } }

        public string VehicleName
        {
            get { return Vehicle.VehicleName; }
            set
            {
                Set(value, () => VehicleName, () => Vehicle.VehicleName = value);
                UpdateHeader();
            }
        }

        public string Rank
        {
            get { return Vehicle.Rank; }
            set
            {
                Set(value, () => Rank, () => Vehicle.Rank = value);
                UpdateHeader();
            }
        }

        public string Text
        {
            get { return Vehicle.Text; }
            set
            {
                Set(value, () => Text, () => Vehicle.Text = value);
                UpdateHeader();
            }
        }

        public string Description
        {
            get { return Vehicle.Description; }
            set
            {
                Set(value, () => Description, () => Vehicle.Description = value);
                UpdateHeader();
            }
        }

        public string Init
        {
            get { return Vehicle.Init; }
            set
            {
                Set(value, () => Init, () => Vehicle.Init = value);
                UpdateHeader();
            }
        }

        public ObservableCollection<VehicleViewModelBase> Children { get; set; }

        private string GetPropertyWithData()
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                return Text;
            }
            if (!string.IsNullOrWhiteSpace(VehicleName))
            {
                return VehicleName;
            }

            return Vehicle.Side;
        }

        private void UpdateHeader()
        {
            FirePropertyChanged(() => Header);
        }
    }
}
