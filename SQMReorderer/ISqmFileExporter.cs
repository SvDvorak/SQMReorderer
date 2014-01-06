using System.IO;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}