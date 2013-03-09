using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class SqmViewModelCreator
    {
        public MissionViewModel CreateMissionViewModel(MissionState missionState)
        {
            var groupViewModels = CreateItemViewModels(missionState.Groups);
            var markerViewModels = CreateItemViewModels(missionState.Markers);

            var missionViewModel = new MissionViewModel();
            missionViewModel.Groups = groupViewModels;
            missionViewModel.Markers = markerViewModels;

            return missionViewModel;
        }

        private ObservableCollection<MarkerViewModel> CreateItemViewModels(List<Marker> markers)
        {
            var markerViewModels = new ObservableCollection<MarkerViewModel>();

            markerViewModels.Add(new MarkerViewModel(new Marker() { Text = "Text1", Name = "Marker1" }));
            markerViewModels.Add(new MarkerViewModel(new Marker() { Text = "Text2", Name = "Marker2" }));

            return markerViewModels;
        }

        private ObservableCollection<StructureItemViewModelBase> CreateItemViewModels(List<Vehicle> items)
        {
            var itemViewModels = new ObservableCollection<StructureItemViewModelBase>();

            if (items == null)
            {
                return itemViewModels;
            }

            foreach (var item in items)
            {
                var itemViewModel = new VehicleViewModel(item);
                itemViewModel.Items = CreateItemViewModels(item.Vehicles);

                itemViewModels.Add(itemViewModel);
            }

            return itemViewModels;
        }
    }
}
