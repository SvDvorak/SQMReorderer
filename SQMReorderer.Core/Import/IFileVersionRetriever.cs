using System.IO;

namespace SQMReorderer.Core.Import
{
    public interface IFileVersionRetriever
    {
        FileVersion GetVersion(Stream stream);
    }
}