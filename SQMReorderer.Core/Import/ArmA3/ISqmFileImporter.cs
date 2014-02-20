using System.IO;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA3
{
    public interface ISqmFileImporter
    {
        SqmContents Import(Stream fileStream);
    }
}