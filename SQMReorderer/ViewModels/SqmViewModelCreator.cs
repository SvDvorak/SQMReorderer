using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class SqmViewModelCreator
    {
        public MissionViewModel CreateMissionViewModel(MissionState missionState)
        {
            var groupViewModels = CreateItemViewModels(missionState.Groups);
            var markerViewModels = CreateItemViewModels(missionState.Markers);

            var missionViewModel = new MissionViewModel();
            missionViewModel.Groups = new ObservableCollection<VehicleViewModel>(groupViewModels);
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

        private List<VehicleViewModel> CreateItemViewModels(List<Vehicle> items)
        {
            var itemViewModels = new List<VehicleViewModel>();

            if (items == null)
            {
                return itemViewModels;
            }

            foreach (var item in items)
            {
                if (item.Side == "LOGIC")
                {
                    continue;
                }

                var childItemViewModels = CreateItemViewModels(item.Vehicles);
                var itemViewModel = new VehicleViewModel(item, childItemViewModels);

                itemViewModels.Add(itemViewModel);
            }

            return itemViewModels;
        }
    }
}
