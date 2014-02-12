using System.IO;

namespace SQMReorderer.Gui.Dialogs
{
    public interface ISaveFileDialogAdapter
    {
        string Filter { get; set; }
        bool AddExtension { get; set; }
        bool? ShowDialog();
        Stream OpenFile();
    }
}