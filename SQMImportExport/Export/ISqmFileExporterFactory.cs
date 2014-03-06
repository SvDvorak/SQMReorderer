using System.IO;
using SQMReorderer.Core.Import;

namespace SQMReorderer.Core.Export
{
    public interface ISqmFileExporterFactory
    {
        ISqmContentsVisitor Create(Stream stream);
    }
}