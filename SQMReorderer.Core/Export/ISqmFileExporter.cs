using System.IO;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Core.Export
{
    public interface ISqmFileExporter
    {
        void Export(Stream stream, SqmContents contents);
    }
}