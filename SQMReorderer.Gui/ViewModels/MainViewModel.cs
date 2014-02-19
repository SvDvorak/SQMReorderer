using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Core.StreamHelpers;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private SqmContents _sqmContents;

        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(OpenFile);
            SaveAsCommand = new DelegateCommand(SaveFileAs);
        }

        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand SaveAsCommand { get; private set; }

        private MissionViewModel _mission;
        public MissionViewModel Mission
        {
            get { return _mission; }
            set
            {
                Set(value, () => Mission, () => _mission = value);
            }
        }

        private IEnumerable<object> _selectedItems;
        public IEnumerable<object> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                Set(value, () => SelectedItems, () => _selectedItems = value);
                SelectedItemsViewModel =
                    new CombinedVehicleViewModel(_selectedItems
                        .Cast<VehicleViewModel>()
                        .ToList());
            }
        }

        private CombinedVehicleViewModel _selectedItemsViewModel;
        public CombinedVehicleViewModel SelectedItemsViewModel
        {
            get { return _selectedItemsViewModel; }
            set
            {
                Set(value, () => SelectedItemsViewModel, () => _selectedItemsViewModel = value);
            }
        }

        private void OpenFile()
        {
            var openSqmFileDialog = new OpenSqmFileDialog(new OpenFileDialogAdapter(), new SqmFileImporter(new StreamToStringsReader(), new SqmContextCreator(), new SqmParser()));

            _sqmContents = openSqmFileDialog.ShowDialog();

            if(_sqmContents != null)
            {
                var sqmViewModelCreator = new SqmViewModelCreator();
                Mission = sqmViewModelCreator.CreateMissionViewModel(_sqmContents.Mission);
            }
        }

        private void SaveFileAs()
        {
            var saveSqmFileDialog = new SaveSqmFileDialog(new SaveFileDialogAdapter(), new SqmFileExporter(new SqmElementExportVisitor(), new ContextIndenter(), new StreamWriterFactory()));

            var reorderer = new ViewModelToContentReorderer();
            reorderer.Reorder(_sqmContents.Mission, Mission);

            saveSqmFileDialog.ShowDialog(_sqmContents);
        }
    }
}