using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    internal class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriterAdapter Create(Stream stream)
        {
            return new StreamWriterAdapter(stream);
        }
    }
}