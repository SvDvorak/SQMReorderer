using System.IO;

namespace SQMReorderer
{
    internal class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriterAdapter Create(Stream stream)
        {
            return new StreamWriterAdapter(stream);
        }
    }
}