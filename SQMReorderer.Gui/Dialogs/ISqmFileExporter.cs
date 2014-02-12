using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}