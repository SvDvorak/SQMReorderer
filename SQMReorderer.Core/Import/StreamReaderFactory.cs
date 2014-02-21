using System.IO;

namespace SQMReorderer.Core.Import
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public IStreamReaderAdapter Create(Stream stream)
        {
            return new StreamReaderAdapter(stream);
        }
    }
}