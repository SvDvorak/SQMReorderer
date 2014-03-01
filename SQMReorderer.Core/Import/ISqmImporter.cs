using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public interface ISqmImporter
    {
        ISqmContents Import(Stream stream);
    }
}