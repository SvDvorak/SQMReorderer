using System.IO;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA2
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}