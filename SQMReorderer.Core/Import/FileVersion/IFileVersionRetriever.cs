using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    internal interface IFileVersionRetriever
    {
        FileVersion GetVersion(int version);
        FileVersion GetVersion(Stream stream);
    }
}