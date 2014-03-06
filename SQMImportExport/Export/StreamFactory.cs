using System.IO;

namespace SQMReorderer.Core.Export
{
    internal class StreamFactory : IStreamFactory
    {
        public Stream Create(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate);
        }
    }
}