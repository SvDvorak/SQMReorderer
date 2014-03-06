using SQMReorderer.Core.Import;

namespace SQMReorderer.Core.Export
{
    public class SaveSqmFile
    {
        private readonly IStreamFactory _streamFactory;
        private readonly ISqmFileExporterFactory _exporterFactory;

        internal SaveSqmFile(IStreamFactory streamFactory, ISqmFileExporterFactory exporterFactory)
        {
            _streamFactory = streamFactory;
            _exporterFactory = exporterFactory;
        }

        public void Save(string filePath, ISqmContents sqmContents)
        {
            var stream = _streamFactory.Create(filePath);

            var exportVisitor = _exporterFactory.Create(stream);
            sqmContents.Accept(exportVisitor);

            stream.Close();
        }
    }
}