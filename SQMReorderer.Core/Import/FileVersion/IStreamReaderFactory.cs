using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    internal interface IStreamReaderFactory
    {
        IStreamReaderAdapter Create(Stream stream);
    }
}