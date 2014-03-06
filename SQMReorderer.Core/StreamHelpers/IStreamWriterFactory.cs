using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    internal interface IStreamWriterFactory
    {
        IStreamWriterAdapter Create(Stream stream);
    }
}