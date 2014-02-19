using System.Collections.ObjectModel;

namespace SQMReorderer.Gui.ViewModels
{
    public class GroupViewModel
    {
        public string Name { get; set; }

        public ObservableCollection<VehicleViewModel> Units { get; set; } 
    }
}