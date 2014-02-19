using System.Collections.ObjectModel;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class GroupViewModel
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }

        public ObservableCollection<VehicleViewModel> Vehicles { get; set; }
    }
}