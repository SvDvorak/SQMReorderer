using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public class GroupViewModel
    {
        public string Name { get; set; }
        public Vehicle ConnectedVehicle { get; set; }

        public ObservableCollection<VehicleViewModel> Vehicles { get; set; }

        public List<Type> ChildTypes 
        {
            get
            {
                return new List<Type>()
                    {
                        typeof (VehicleViewModel)
                    };
            }
        }
    }
}