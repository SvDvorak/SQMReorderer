using System.IO;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public class SqmFileImporter : ISqmFileImporter
    {
        private readonly IFileVersionRetriever _fileVersionRetriever;
        private readonly ISqmContentCombiner _contentCombiner;
        private readonly ArmA2.ISqmFileImporter _arma2Importer;
        private readonly ArmA3.ISqmFileImporter _arma3Importer;

        public SqmFileImporter(IFileVersionRetriever fileVersionRetriever,
            ISqmContentCombiner contentCombiner,
            ArmA2.ISqmFileImporter arma2Importer,
            ArmA3.ISqmFileImporter arma3Importer)
        {
            _fileVersionRetriever = fileVersionRetriever;
            _contentCombiner = contentCombiner;
            _arma2Importer = arma2Importer;
            _arma3Importer = arma3Importer;
        }

        public ISqmContents Import(Stream stream)
        {
            var fileVersion = _fileVersionRetriever.GetVersion(stream);
            if (fileVersion == FileVersion.FileVersion.ArmA2)
            {
                return _arma2Importer.Import(stream);

                //return _contentCombiner.Combine(arma2Contents);
            }

            return _arma3Importer.Import(stream);

            //return _contentCombiner.Combine(arma3Contents);
        }
    }
}
