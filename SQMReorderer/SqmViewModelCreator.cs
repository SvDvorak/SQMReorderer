using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class SqmViewModelCreator
    {
        public MissionViewModel CreateMissionViewModel(Mission mission)
        {
            var itemViewModels = CreateItemViewModels(mission.Groups);

            return new MissionViewModel(itemViewModels);
        }

        private static List<ItemViewModel> CreateItemViewModels(List<Item> items)
        {
            var itemViewModels = new List<ItemViewModel>();

            if(items == null)
            {
                return itemViewModels;
            }

            foreach (var item in items)
            {
                var itemViewModel = new ItemViewModel(item);
                itemViewModel.Items = CreateItemViewModels(item.Items);

                itemViewModels.Add(itemViewModel);
            }

            return itemViewModels;
        }
    }
}
