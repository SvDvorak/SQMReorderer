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
            var itemViewModels = CreateItemViewModels(missionState.Groups);

            return new MissionViewModel(itemViewModels);
        }

        private static ObservableCollection<ItemViewModel> CreateItemViewModels(List<Vehicle> items)
        {
            var itemViewModels = new ObservableCollection<ItemViewModel>();

            if(items == null)
            {
                return itemViewModels;
            }

            foreach (var item in items)
            {
                var itemViewModel = new ItemViewModel(item);
                itemViewModel.Items = CreateItemViewModels(item.Vehicles);

                itemViewModels.Add(itemViewModel);
            }

            return itemViewModels;
        }
    }
}
