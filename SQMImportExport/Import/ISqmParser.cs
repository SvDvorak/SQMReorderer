using SQMImportExport.Import.Context;

namespace SQMImportExport.Import
{
    internal interface ISqmParser
    {
        ISqmContents ParseContext(SqmContext context);
    }
}