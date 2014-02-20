using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public class SqmFileImporter : ISqmFileImporter
    {
        private readonly ArmA2.ISqmParser _arma2Parser;
        private readonly ArmA3.ISqmParser _arma3Parser;

        public SqmFileImporter(
            ArmA2.ISqmParser arma2Parser,
            ArmA3.ISqmParser arma3Parser)
        {
            _arma2Parser = arma2Parser;
            _arma3Parser = arma3Parser;
        }

        public SqmContents Import(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
