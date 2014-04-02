using System.IO;

namespace SQMReorderer.Gui.Dialogs
{
    internal class StreamFactory : IStreamFactory
    {
        public Stream Create(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate);
        }
    }
}