using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    public interface IStreamWriterFactory
    {
        IStreamWriterAdapter Create(Stream stream);
    }
}