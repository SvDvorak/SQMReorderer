using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
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