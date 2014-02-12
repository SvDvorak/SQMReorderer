using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}