using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public interface IFileVersionRetriever
    {
        FileVersion GetVersion(int version);
        FileVersion GetVersion(Stream stream);
    }
}