using System.IO;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Core.StreamHelpers;

namespace SQMReorderer.Gui.Dialogs
{
    public class SqmFileImporter : ISqmFileImporter
    {
        private readonly IStreamToStringsReader _streamToStringsReader;
        private readonly ISqmContextCreator _sqmContextCreator;
        private readonly ISqmParser _sqmParser;

        public SqmFileImporter(IStreamToStringsReader streamToStringsReader, ISqmContextCreator sqmContextCreator, ISqmParser sqmParser)
        {
            _streamToStringsReader = streamToStringsReader;
            _sqmContextCreator = sqmContextCreator;
            _sqmParser = sqmParser;
        }

        public SqmContents Import(Stream fileStream)
        {
            var linesInFile = _streamToStringsReader.Read(fileStream);

            var rootContext = _sqmContextCreator.CreateRootContext(linesInFile);

            return _sqmParser.ParseContext(rootContext);
        }
    }
}