using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public class FileVersionRetriever : IFileVersionRetriever
    {
        private readonly IStreamReaderFactory _streamReaderFactory;

        public FileVersionRetriever(IStreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public FileVersion GetVersion(Stream stream)
        {
            var streamReader = _streamReaderFactory.Create(stream);

            var versionLine = streamReader.ReadLine();

            if (versionLine.Contains("version=11"))
            {
                return FileVersion.ArmA2;
            }
            if(versionLine.Contains("version=12"))
            {
                return FileVersion.ArmA3;
            }

            throw new SqmParseException("File is missing or has an unknown version");
        }
    }
}