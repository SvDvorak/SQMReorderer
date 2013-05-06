using System.Collections.ObjectModel;

namespace SQMReorderer.ViewModels
{
    public class MissionViewModel
    {
        public MissionViewModel()
        {
            Groups = new ObservableCollection<VehicleViewModel>();
            Markers = new ObservableCollection<MarkerViewModel>();
        }

        public ObservableCollection<VehicleViewModel> Groups { get; set; }
        public ObservableCollection<MarkerViewModel> Markers { get; set; }
    }
}