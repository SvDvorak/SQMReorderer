using System.IO;

namespace SQMReorderer.Core.Export
{
    public interface IStreamFactory
    {
        Stream Create(string filePath);
    }
}