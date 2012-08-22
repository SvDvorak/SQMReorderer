using System.ComponentModel;
using SQMReorderer.SqmParser;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            var missionReader = new FileToStringsReader();
            var missionText = missionReader.Read("mission.sqm");
            SqmStream stream = new SqmStream(missionText);

            var sqmParser = new SqmParser.SqmParser();
            var parseResult = sqmParser.Parse(stream);

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