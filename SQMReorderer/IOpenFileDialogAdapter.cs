using System.IO;

namespace SQMReorderer
{
    public interface IOpenFileDialogAdapter
    {
        Stream OpenFile();
    }
}