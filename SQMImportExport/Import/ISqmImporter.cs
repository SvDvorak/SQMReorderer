using System.IO;

namespace SQMImportExport.Import
{
    public interface ISqmImporter
    {
        SqmContentsBase Import(Stream stream);
    }
}