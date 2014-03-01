using System.IO;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Core.Export.ArmA3
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}