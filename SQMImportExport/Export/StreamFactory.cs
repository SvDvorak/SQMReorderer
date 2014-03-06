using System.IO;

namespace SQMImportExport.Export
{
    internal class StreamFactory : IStreamFactory
    {
        public Stream Create(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate);
        }
    }
}