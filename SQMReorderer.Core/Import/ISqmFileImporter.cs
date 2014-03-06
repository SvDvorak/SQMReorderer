using System.IO;

namespace SQMReorderer.Core.Import
{
    internal interface ISqmFileImporter
    {
        ISqmContents Import(Stream fileStream);
    }
}