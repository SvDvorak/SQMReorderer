using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public class SaveSqmFileDialog
    {
        private readonly ISaveFileDialogAdapter _saveFileDialogAdapter;
        private readonly ISqmFileExporter _sqmFileExporter;

        public SaveSqmFileDialog(ISaveFileDialogAdapter saveFileDialogAdapter, ISqmFileExporter sqmFileExporter)
        {
            _saveFileDialogAdapter = saveFileDialogAdapter;
            _sqmFileExporter = sqmFileExporter;
        }

        public void ShowDialog(SqmContents sqmContents)
        {
            _saveFileDialogAdapter.ShowDialog();

            var fileStream = _saveFileDialogAdapter.OpenFile();

            _sqmFileExporter.Export(fileStream, sqmContents);

            fileStream.Close();
        }
    }
}