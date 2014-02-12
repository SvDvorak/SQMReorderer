using System.IO;

namespace SQMReorderer.Dialogs
{
    public interface ISaveFileDialogAdapter
    {
        bool? ShowDialog();
        Stream OpenFile();
    }
}