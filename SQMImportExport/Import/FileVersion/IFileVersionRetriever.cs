using System.IO;

namespace SQMImportExport.Import.FileVersion
{
    internal interface IFileVersionRetriever
    {
        FileVersion GetVersion(int version);
        FileVersion GetVersion(Stream stream);
    }
}