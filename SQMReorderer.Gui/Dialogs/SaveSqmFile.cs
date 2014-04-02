using SQMImportExport.Common;
using SQMImportExport.Export;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveSqmFile
    {
        private readonly IStreamFactory _streamFactory;
        private readonly ISqmExporter _sqmExporter;

        public SaveSqmFile(IStreamFactory streamFactory, ISqmExporter sqmExporter)
        {
            _streamFactory = streamFactory;
            _sqmExporter = sqmExporter;
        }

        public void Save(string filePath, SqmContentsBase sqmContents)
        {
            var stream = _streamFactory.Create(filePath);

            _sqmExporter.Export(stream, sqmContents);

            stream.Close();
        }
    }
}