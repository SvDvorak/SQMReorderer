using System.Collections.ObjectModel;
using SQMImportExport.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IGroupViewModel
    {
        VehicleBase ConnectedVehicle { get; set; }
        ObservableCollection<VehicleViewModelBase> Vehicles { get; set; }
    }
}