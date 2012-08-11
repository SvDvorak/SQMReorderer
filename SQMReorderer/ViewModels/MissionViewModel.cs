using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SQMReorderer.ViewModels
{
    public class MissionViewModel
    {
        private readonly ObservableCollection<ItemViewModel> _groupViewModels;

        public MissionViewModel(ObservableCollection<ItemViewModel> groupViewModels)
        {
            _groupViewModels = new ObservableCollection<ItemViewModel>(groupViewModels);
        }

        public ObservableCollection<ItemViewModel> Groups { get { return _groupViewModels; } }
    }
}