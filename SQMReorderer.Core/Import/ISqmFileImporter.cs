using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public interface ISqmFileImporter
    {
        ISqmContents Import(Stream fileStream);
    }
}