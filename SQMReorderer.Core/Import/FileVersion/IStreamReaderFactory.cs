using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public interface IStreamReaderFactory
    {
        IStreamReaderAdapter Create(Stream stream);
    }
}