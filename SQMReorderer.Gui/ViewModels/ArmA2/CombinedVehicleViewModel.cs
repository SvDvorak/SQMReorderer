﻿using System;
using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;
using SQMReorderer.Gui.Dialogs.AddInit;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class CombinedVehicleViewModel : ViewModelBase, ICombinedVehicleViewModel
    {
        private readonly List<VehicleViewModel> _vehicles;
        private readonly IAddInitDialogFactory _addInitDialogFactory;

        public CombinedVehicleViewModel(List<VehicleViewModel> vehicles, IAddInitDialogFactory addInitDialogFactory)
        {
            _vehicles = vehicles;
            _addInitDialogFactory = addInitDialogFactory;
            AddInitCommand = new DelegateCommand(AddInit);
        }

        public DelegateCommand AddInitCommand { get; private set; }

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

        public string Init
        {
            get { return GetCombinedValue(x => x.Init); }
            set { Set(value, () => Init, () => SetCombinedValue(x => x.Init = value)); }
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

        private void AddInit()
        {
            var addInitDialog = _addInitDialogFactory.Create();
            var addInitResult = addInitDialog.ShowDialog();

            if (addInitResult.DialogResult == DialogResult.Ok)
            {
                SetCombinedValue(x => x.Init += addInitResult.InitToAdd);
                FirePropertyChanged(() => Init);
            }
        }
    }
}