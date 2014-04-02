using System.IO;

namespace SQMReorderer.Gui.Dialogs
{
    public interface IStreamFactory
    {
        Stream Create(string filePath);
    }
}