using System.IO;

namespace SQMImportExport.Export
{
    internal interface IStreamFactory
    {
        Stream Create(string filePath);
    }
}