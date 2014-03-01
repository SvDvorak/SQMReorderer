using System.IO;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA3
{
    public interface ISqmFileImporter
    {
        ISqmContents Import(Stream fileStream);
    }
}