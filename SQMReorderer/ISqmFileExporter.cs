using System.IO;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}