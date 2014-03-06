using System.IO;

namespace SQMImportExport.Import
{
    internal interface ISqmFileImporter
    {
        ISqmContents Import(Stream fileStream);
    }
}