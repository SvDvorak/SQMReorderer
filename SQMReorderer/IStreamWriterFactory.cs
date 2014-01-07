using System.IO;

namespace SQMReorderer
{
    public interface IStreamWriterFactory
    {
        IStreamWriterAdapter Create(Stream stream);
    }
}