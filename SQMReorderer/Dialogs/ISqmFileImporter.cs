using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}