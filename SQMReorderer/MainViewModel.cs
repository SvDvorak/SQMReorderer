using System.ComponentModel;
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
            var fileContext = contextCreator.CreateContext(fileText);

            var sqmParser = new SqmParser.SqmParser();
            var parseResult = sqmParser.Parse(fileContext);

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(parseResult.Mission);
        }

        public MissionViewModel Mission { get; set; }

        private ItemViewModel _selectedItem;
        public ItemViewModel SelectedItem
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