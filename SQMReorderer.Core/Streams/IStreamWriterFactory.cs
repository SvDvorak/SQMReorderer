using System.IO;

namespace SQMReorderer.Core.Streams
{
    public interface IStreamWriterFactory
    {
        IStreamWriterAdapter Create(Stream stream);
    }
}