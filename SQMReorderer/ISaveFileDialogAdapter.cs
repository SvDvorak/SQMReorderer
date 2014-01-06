using System.IO;

namespace SQMReorderer
{
    public interface ISaveFileDialogAdapter
    {
        bool? ShowDialog();
        Stream OpenFile();
    }
}