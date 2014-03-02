using System.IO;

namespace SQMReorderer.Core.Import
{
    public interface ISqmFileImporter
    {
        ISqmContents Import(Stream fileStream);
    }
}