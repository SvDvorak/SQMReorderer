using System.IO;

namespace SQMReorderer.Core.Streams
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriterAdapter Create(Stream stream)
        {
            return new StreamWriterAdapter(stream);
        }
    }
}