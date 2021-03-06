using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Common;
using SQMImportExport.Export;
using SQMImportExport.Import;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;
using SQMReorderer.Gui.Dialogs.AddInit;

namespace SQMReorderer.Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private SqmContentsBase _sqmContents;
        private string _lastOpenedFilePath;

        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(OpenFile);
            SaveCommand = new DelegateCommand(SaveFile);
            SaveAsCommand = new DelegateCommand(SaveFileAs);
        }

        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SaveAsCommand { get; private set; }

        private IEnumerable<ITeamViewModel> _teams;
        public IEnumerable<ITeamViewModel> Teams
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
                var combinedVehicleViewModelFactory = new CombinedVehicleViewModelFactory(new AddInitDialogFactory());
                SelectedItemsViewModel =
                    combinedVehicleViewModelFactory.Create(_selectedItems
                        .Where(x => x is VehicleViewModelBase)
                        .Cast<VehicleViewModelBase>()
                        .ToList());
            }
        }

        private ICombinedVehicleViewModel _selectedItemsViewModel;
        public ICombinedVehicleViewModel SelectedItemsViewModel
        {
            get { return _selectedItemsViewModel; }
            set { Set(value, () => SelectedItemsViewModel, () => _selectedItemsViewModel = value); }
        }

        private void OpenFile()
        {
            var openSqmFileDialog = new OpenSqmFileDialog(new OpenFileDialogAdapter(),
                new SqmImporter(), new MessageBoxPresenter());

            _sqmContents = openSqmFileDialog.ShowDialog();

            if (_sqmContents != null)
            {
                var teamViewModelsVisitor = new TeamViewModelsFactoryVisitor(
                    new ArmA2.TeamViewModelsFactory(new ArmA2.GroupViewModelsFactory(new ArmA2.VehicleViewModelsFactory())),
                    new ArmA3.TeamViewModelsFactory(new ArmA3.GroupViewModelsFactory(new ArmA3.VehicleViewModelsFactory())));

                Teams = _sqmContents.Accept(teamViewModelsVisitor);
                _lastOpenedFilePath = openSqmFileDialog.SelectedPath;
            }
        }

        private void SaveFile()
        {
            if(!string.IsNullOrEmpty(_lastOpenedFilePath))
            {
                var saveSqmFile = new SaveSqmFile(new StreamFactory(), new SqmExporter());

                var contentReorderer = new ViewModelToContentReorderer();
				contentReorderer.Reorder(_sqmContents.Mission, Teams.ToList());

                saveSqmFile.Save(_lastOpenedFilePath, _sqmContents);
            }
        }

        private void SaveFileAs()
        {
            var saveSqmFileDialog = new SaveSqmAsFileDialog(new SaveFileDialogAdapter(), new SqmExporter());

            var contentReorderer = new ViewModelToContentReorderer();
            contentReorderer.Reorder(_sqmContents.Mission, Teams.ToList());

            saveSqmFileDialog.ShowDialog(_sqmContents);
        }
    }
}