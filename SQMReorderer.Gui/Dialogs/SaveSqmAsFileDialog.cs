using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveSqmAsFileDialog
    {
        private readonly ISaveFileDialogAdapter _saveFileDialogAdapter;
        private readonly ISqmFileExporterFactory _exporterFactory;

        public SaveSqmAsFileDialog(ISaveFileDialogAdapter saveFileDialogAdapter, ISqmFileExporterFactory exporterFactory)
        {
            _saveFileDialogAdapter = saveFileDialogAdapter;
            _exporterFactory = exporterFactory;

            _saveFileDialogAdapter.AddExtension = true;
            _saveFileDialogAdapter.Filter = "SQM File (*.sqm)|*.sqm";
        }

        public void ShowDialog(ISqmContents sqmContents)
        {
            var shouldOpen = _saveFileDialogAdapter.ShowDialog();

            if(shouldOpen.HasValue && shouldOpen.Value)
            {
                var fileStream = _saveFileDialogAdapter.OpenFile();
                var exporter = _exporterFactory.Create(fileStream);

                sqmContents.Accept(exporter);

                fileStream.Close();
            }
        }
    }
}