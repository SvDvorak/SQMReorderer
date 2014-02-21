using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public interface IFileVersionRetriever
    {
        FileVersion GetVersion(Stream stream);
    }
}