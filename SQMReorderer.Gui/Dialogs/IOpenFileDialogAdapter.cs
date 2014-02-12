using System.IO;

namespace SQMReorderer.Gui.Dialogs
{
    public interface IOpenFileDialogAdapter
    {
        Stream OpenFile();
        string FileName { get; set; }
        string Filter { get; set; }
        bool? ShowDialog();
    }
}