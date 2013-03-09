using System.Collections.ObjectModel;

namespace SQMReorderer.ViewModels
{
    public class MissionViewModel
    {
        public MissionViewModel()
        {
            Groups = new ObservableCollection<StructureItemViewModelBase>();
            Markers = new ObservableCollection<MarkerViewModel>();
        }

        public ObservableCollection<StructureItemViewModelBase> Groups { get; set; }
        public ObservableCollection<MarkerViewModel> Markers { get; set; }
    }
}