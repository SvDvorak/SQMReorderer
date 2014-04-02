using System.IO;
using SQMImportExport.Common;
using SQMImportExport.Import;

namespace SQMImportExport.Export
{
    public interface ISqmFileExporterFactory
    {
        ISqmContentsVisitor Create(Stream stream);
    }
}