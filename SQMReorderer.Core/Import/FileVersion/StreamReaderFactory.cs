using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public IStreamReaderAdapter Create(Stream stream)
        {
            return new StreamReaderAdapter(stream);
        }
    }
}