using System.IO;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}