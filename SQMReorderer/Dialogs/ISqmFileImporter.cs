using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Dialogs
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}