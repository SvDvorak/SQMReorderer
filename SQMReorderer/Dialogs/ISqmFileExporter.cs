using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Dialogs
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}