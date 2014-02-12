using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SQMReorderer.Core.SqmExport;
using SQMReorderer.Core.SqmParser;
using SQMReorderer.Core.SqmParser.Context;
using SQMReorderer.Core.SqmParser.ResultObjects;
using SQMReorderer.Core.Streams;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Gui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SqmContents _sqmContents;

        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(OpenFile);
            SaveAsCommand = new DelegateCommand(SaveFileAs);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand SaveAsCommand { get; private set; }

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

        private void OpenFile()
        {
            var openSqmFileDialog = new OpenSqmFileDialog(new OpenFileDialogAdapter(), new SqmFileImporter(new StreamToStringsReader(), new SqmContextCreator(), new SqmParser()));

            _sqmContents = openSqmFileDialog.ShowDialog();

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(_sqmContents.Mission);
        }

        private void SaveFileAs()
        {
            var saveSqmFileDialog = new SaveSqmFileDialog(new SaveFileDialogAdapter(), new SqmFileExporter(new SqmElementExportVisitor(), new ContextIndenter(), new StreamWriterFactory()));

            saveSqmFileDialog.ShowDialog(_sqmContents);
        }
    }
}