using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    internal class StreamReaderFactory : IStreamReaderFactory
    {
        public IStreamReaderAdapter Create(Stream stream)
        {
            return new StreamReaderAdapter(stream);
        }
    }
}