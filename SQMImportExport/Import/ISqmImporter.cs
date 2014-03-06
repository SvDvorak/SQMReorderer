using System.IO;

namespace SQMImportExport.Import
{
    public interface ISqmImporter
    {
        ISqmContents Import(Stream stream);
    }
}