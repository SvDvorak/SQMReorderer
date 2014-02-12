using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}