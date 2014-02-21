using System.IO;

namespace SQMReorderer.Core.Import
{
    public interface IStreamReaderFactory
    {
        IStreamReaderAdapter Create(Stream stream);
    }
}