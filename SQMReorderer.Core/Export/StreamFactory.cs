using System.IO;

namespace SQMReorderer.Core.Export
{
    public class StreamFactory : IStreamFactory
    {
        public Stream Create(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate);
        }
    }
}