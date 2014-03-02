using SQMReorderer.Core.Import;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveSqmFileDialog
    {
        private readonly ISaveFileDialogAdapter _saveFileDialogAdapter;
        private readonly ISqmContentsVisitor _sqmFileExporter;

        public SaveSqmFileDialog(ISaveFileDialogAdapter saveFileDialogAdapter, ISqmContentsVisitor sqmFileExporter)
        {
            _saveFileDialogAdapter = saveFileDialogAdapter;
            _sqmFileExporter = sqmFileExporter;

            _saveFileDialogAdapter.AddExtension = true;
            _saveFileDialogAdapter.Filter = "SQM File (*.sqm)|*.sqm";
        }

        public void ShowDialog(ISqmContents sqmContents)
        {
            var shouldOpen = _saveFileDialogAdapter.ShowDialog();

            if(shouldOpen.HasValue && shouldOpen.Value)
            {
                var fileStream = _saveFileDialogAdapter.OpenFile();

                sqmContents.Accept(_sqmFileExporter);

                fileStream.Close();
            }
        }
    }
}