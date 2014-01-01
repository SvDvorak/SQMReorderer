using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            var fileReader = new FileToStringsReader();
            var fileText = fileReader.Read("mission.sqm");

            var contextCreator = new SqmContextCreator();
            var fileContext = contextCreator.CreateRootContext(fileText);

            var sqmParser = new SqmParser.SqmParser();
            var parseResult = sqmParser.ParseContext(fileContext);

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(parseResult.Mission);
        }

        public MissionViewModel Mission { get; set; }

        private IEnumerable<object> _selectedItems;
        public IEnumerable<object> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                SelectedItem = _selectedItems.FirstOrDefault();
                //PropertyChanged(this, new PropertyChangedEventArgs("SelectedItems"));
            }
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}