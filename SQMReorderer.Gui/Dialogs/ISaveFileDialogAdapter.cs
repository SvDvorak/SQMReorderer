using System.IO;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISaveFileDialogAdapter
    {
        bool? ShowDialog();
        Stream OpenFile();
    }
}