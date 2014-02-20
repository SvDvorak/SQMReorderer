using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.ArmA2;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public class OpenSqmFileDialog
    {
        private readonly IOpenFileDialogAdapter _openFileDialog;
        private readonly ISqmFileImporter _sqmFileImporter;

        public OpenSqmFileDialog(
            IOpenFileDialogAdapter openFileDialog,
            ISqmFileImporter sqmFileImporter)
        {
            _openFileDialog = openFileDialog;
            _sqmFileImporter = sqmFileImporter;

            _openFileDialog.Filter = "SQM Files (*.sqm)|*.sqm";
        }

        public SqmContents ShowDialog()
        {
            SqmContents sqmContents = null;
            var shouldSave = _openFileDialog.ShowDialog();

            if (shouldSave.HasValue && shouldSave.Value)
            {
                var fileStream = _openFileDialog.OpenFile();

                sqmContents = _sqmFileImporter.Import(fileStream);

                fileStream.Close();
            }

            return sqmContents;
        }
    }
}