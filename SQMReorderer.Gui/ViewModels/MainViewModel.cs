using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Export;
using SQMImportExport.Import;
using SQMReorderer.Gui.Command;
using SQMReorderer.Gui.Dialogs;
using SQMReorderer.Gui.Dialogs.AddInit;

namespace SQMReorderer.Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ISqmContents _sqmContents;
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
                        .Where(x => x is IVehicleViewModel)
                        .Cast<IVehicleViewModel>()
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
                var saveSqmFile = new SaveSqmFile();

                var reordererVisitor = new ViewModelToContentReordererVisitor(
                    Teams.ToList(),
                    new ArmA2.ViewModelToContentReorderer(),
                    new ArmA3.ViewModelToContentReorderer());

                _sqmContents.Accept(reordererVisitor);

                saveSqmFile.Save(_lastOpenedFilePath, _sqmContents);
            }
        }

        private void SaveFileAs()
        {
            var exporterFactory = new SqmFileExporterFactory();
            var saveSqmFileDialog = new SaveSqmAsFileDialog(new SaveFileDialogAdapter(), exporterFactory);

            var reordererVisitor = new ViewModelToContentReordererVisitor(
                Teams.ToList(),
                new ArmA2.ViewModelToContentReorderer(),
                new ArmA3.ViewModelToContentReorderer());

            _sqmContents.Accept(reordererVisitor);

            saveSqmFileDialog.ShowDialog(_sqmContents);
        }
    }
}