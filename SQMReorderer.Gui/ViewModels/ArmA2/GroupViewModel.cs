using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMImportExport.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public class GroupViewModel : IGroupViewModel
    {
        public string Name { get; set; }
        public VehicleBase ConnectedVehicle { get; set; }

        public ObservableCollection<VehicleViewModelBase> Vehicles { get; set; }

        public List<Type> ChildTypes 
        {
            get
            {
                return new List<Type>()
                    {
                        typeof (VehicleViewModelBase)
                    };
            }
        }
    }
}