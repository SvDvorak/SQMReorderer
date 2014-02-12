using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}