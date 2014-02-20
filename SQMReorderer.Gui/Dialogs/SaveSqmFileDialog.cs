using SQMReorderer.Core.Export;
using SQMReorderer.Core.Export.ArmA2;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveSqmFileDialog
    {
        private readonly ISaveFileDialogAdapter _saveFileDialogAdapter;
        private readonly ISqmFileExporter _sqmFileExporter;

        public SaveSqmFileDialog(ISaveFileDialogAdapter saveFileDialogAdapter, ISqmFileExporter sqmFileExporter)
        {
            _saveFileDialogAdapter = saveFileDialogAdapter;
            _sqmFileExporter = sqmFileExporter;

            _saveFileDialogAdapter.AddExtension = true;
            _saveFileDialogAdapter.Filter = "SQM File (*.sqm)|*.sqm";
        }

        public void ShowDialog(SqmContents sqmContents)
        {
            var shouldOpen = _saveFileDialogAdapter.ShowDialog();

            if(shouldOpen.HasValue && shouldOpen.Value)
            {
                var fileStream = _saveFileDialogAdapter.OpenFile();

                _sqmFileExporter.Export(fileStream, sqmContents);

                fileStream.Close();
            }
        }
    }
}