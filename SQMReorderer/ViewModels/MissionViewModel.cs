using System.Collections.Generic;

namespace SQMReorderer.ViewModels
{
    public class MissionViewModel
    {
        private readonly List<ItemViewModel> _groupViewModels;

        public MissionViewModel(List<ItemViewModel> groupViewModels)
        {
            _groupViewModels = groupViewModels;
        }

        public List<ItemViewModel> Groups { get { return _groupViewModels; } }
    }
}