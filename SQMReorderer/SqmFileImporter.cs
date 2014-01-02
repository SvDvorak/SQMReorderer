using System.IO;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public class SqmFileImporter : ISqmFileImporter
    {
        private readonly IFileToStringsReader _fileToStringsReader;
        private readonly ISqmContextCreator _sqmContextCreator;
        private readonly ISqmParser _sqmParser;

        public SqmFileImporter(IFileToStringsReader fileToStringsReader, ISqmContextCreator sqmContextCreator, ISqmParser sqmParser)
        {
            _fileToStringsReader = fileToStringsReader;
            _sqmContextCreator = sqmContextCreator;
            _sqmParser = sqmParser;
        }

        public SqmContents Import(Stream fileStream)
        {
            var linesInFile = _fileToStringsReader.Read(fileStream);

            var rootContext = _sqmContextCreator.CreateRootContext(linesInFile);

            return _sqmParser.ParseContext(rootContext);
        }
    }
}