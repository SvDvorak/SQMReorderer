using System.IO;

namespace SQMImportExport.Import
{
    internal interface ISqmFileImporter
    {
        SqmContentsBase Import(Stream fileStream);
    }
}