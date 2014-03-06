using System.IO;

namespace SQMReorderer.Core.Import
{
    public interface ISqmImporter
    {
        ISqmContents Import(Stream stream);
    }
}