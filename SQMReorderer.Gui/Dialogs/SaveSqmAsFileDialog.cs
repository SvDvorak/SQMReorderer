using SQMImportExport.Common;
using SQMImportExport.Export;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveSqmAsFileDialog
    {
        private readonly ISaveFileDialogAdapter _saveFileDialogAdapter;
        private readonly ISqmExporter _sqmExporter;

        public SaveSqmAsFileDialog(ISaveFileDialogAdapter saveFileDialogAdapter, ISqmExporter sqmExporter)
        {
            _saveFileDialogAdapter = saveFileDialogAdapter;
            _sqmExporter = sqmExporter;

            _saveFileDialogAdapter.AddExtension = true;
            _saveFileDialogAdapter.Filter = "SQM File (*.sqm)|*.sqm";
        }

        public void ShowDialog(SqmContentsBase sqmContents)
        {
            var shouldOpen = _saveFileDialogAdapter.ShowDialog();

            if(shouldOpen.HasValue && shouldOpen.Value)
            {
                var fileStream = _saveFileDialogAdapter.OpenFile();

				_sqmExporter.Export(fileStream, sqmContents);

                fileStream.Close();
            }
        }
    }
}