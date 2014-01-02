using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SQMReorderer.Command;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(OpenFile);
        }

        private void OpenFile()
        {
            var openSqmFileDialog = new OpenSqmFileDialog(new OpenFileDialogAdapter(), new SqmFileImporter(new StreamToStringsReader(), new SqmContextCreator(), new SqmParser.SqmParser()));

            var parseResult = openSqmFileDialog.ShowDialog();

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(parseResult.Mission);
        }

        private MissionViewModel _mission;
        public MissionViewModel Mission
        {
            get { return _mission; }
            set
            {
                _mission = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Mission"));
            }
        }

        public DelegateCommand OpenCommand { get; private set; }

        private IEnumerable<object> _selectedItems;
        public IEnumerable<object> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                SelectedItem = _selectedItems.FirstOrDefault();
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItems"));
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