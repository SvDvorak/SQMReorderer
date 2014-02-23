using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Core.StreamHelpers;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;
using SqmFileExporter = SQMReorderer.Core.Export.SqmFileExporter;

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

        private IEnumerable<TeamViewModel> _teams;

        public IEnumerable<TeamViewModel> Teams
        {
            get { return _teams; }
            set { Set(value, () => Teams, () => _teams = value); }
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
                        .Where(x => x is VehicleViewModel)
                        .Cast<VehicleViewModel>()
                        .ToList());
            }
        }

        private CombinedVehicleViewModel _selectedItemsViewModel;

        public CombinedVehicleViewModel SelectedItemsViewModel
        {
            get { return _selectedItemsViewModel; }
            set { Set(value, () => SelectedItemsViewModel, () => _selectedItemsViewModel = value); }
        }

        private void OpenFile()
        {
            var streamToStringsReader = new StreamToStringsReader();
            var sqmContextCreator = new SqmContextCreator();
            var arma2Importer = new Core.Import.ArmA2.SqmFileImporter(streamToStringsReader, sqmContextCreator,
                new Core.Import.ArmA2.SqmParser());
            var arma3Importer = new Core.Import.ArmA3.SqmFileImporter(streamToStringsReader, sqmContextCreator,
                new Core.Import.ArmA3.SqmParser());
            var openSqmFileDialog = new OpenSqmFileDialog(new OpenFileDialogAdapter(),
                new SqmFileImporter(new FileVersionRetriever(new StreamReaderFactory()), new SqmContentCombiner(),
                    arma2Importer, arma3Importer));

            _sqmContents = openSqmFileDialog.ShowDialog();

            if (_sqmContents != null)
            {
                var teamViewModelsFactory =
                    new TeamViewModelsFactory(new GroupViewModelsFactory(new VehicleViewModelsFactory()));
                Teams = teamViewModelsFactory.Create(_sqmContents.Mission.Groups);
            }
        }

        private void SaveFileAs()
        {
            var contextIndenter = new ContextIndenter();
            var streamWriterFactory = new StreamWriterFactory();
            var saveSqmFileDialog = new SaveSqmFileDialog(new SaveFileDialogAdapter(), new SqmFileExporter(
                new Core.Export.ArmA2.SqmFileExporter(new Core.Export.ArmA2.SqmElementExportVisitor(),
                    contextIndenter, streamWriterFactory),
                new Core.Export.ArmA3.SqmFileExporter(new Core.Export.ArmA3.SqmElementExportVisitor(),
                    contextIndenter, streamWriterFactory),
                new FileVersionRetriever(new StreamReaderFactory())));

            var reorderer = new ViewModelToContentReorderer();
            reorderer.Reorder(_sqmContents.Mission, Teams.ToList());

            saveSqmFileDialog.ShowDialog(_sqmContents);
        }
    }
}