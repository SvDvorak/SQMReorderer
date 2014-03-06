using System.IO;
using SQMReorderer.Core.Import.FileVersion;

namespace SQMReorderer.Core.Import
{
    public class SqmImporter : ISqmImporter
    {
        private readonly IFileVersionRetriever _fileVersionRetriever;
        private readonly ISqmFileImporter _arma2Importer;
        private readonly ISqmFileImporter _arma3Importer;

        internal SqmImporter(IFileVersionRetriever fileVersionRetriever,
            ISqmFileImporter arma2Importer,
            ISqmFileImporter arma3Importer)
        {
            _fileVersionRetriever = fileVersionRetriever;
            _arma2Importer = arma2Importer;
            _arma3Importer = arma3Importer;
        }

    //public SqmImporter() : this(new FileVersionRetriever(new StreamReaderFactory()), new Core.Import.ArmA2.)

        public ISqmContents Import(Stream stream)
        {
            var fileVersion = _fileVersionRetriever.GetVersion(stream);
            if (fileVersion == FileVersion.FileVersion.ArmA2)
            {
                return _arma2Importer.Import(stream);
            }

            return _arma3Importer.Import(stream);
        }
    }
}
