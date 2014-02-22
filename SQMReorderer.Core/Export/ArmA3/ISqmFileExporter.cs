using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Export.ArmA3
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}