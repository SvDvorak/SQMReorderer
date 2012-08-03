using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.TreeView;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            SelectedItems = new ObservableCollection<MultipleSelectionTreeViewItem>();

            var missionReader = new FileToStringsReader();
            var missionText = missionReader.Read("mission.sqm");
            SqmStream stream = new SqmStream(missionText);

            var sqmParser = new SqmParser.SqmParser();
            var parseResult = sqmParser.Parse(stream);

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(parseResult.Mission);

            ItemViewModel.SelectedItemChanged += OnSelectedItemChanged;
        }

        public MissionViewModel Mission { get; set; }

        public ObservableCollection<MultipleSelectionTreeViewItem> SelectedItems { get; set; }

        private void OnSelectedItemChanged(bool isSelected, ItemViewModel item)
        {
            if(isSelected)
            {
                //SelectedItems.Add(item);
            }
            //else
            //{
            //    SelectedItems.Remove(item);
            //}
        }
    }
}