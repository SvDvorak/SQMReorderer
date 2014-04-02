using System.Collections.ObjectModel;
using SQMImportExport.Common;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IGroupViewModel
    {
        VehicleBase ConnectedVehicle { get; set; }
        ObservableCollection<VehicleViewModelBase> Vehicles { get; set; }
    }
}