using System.IO;

namespace SQMReorderer.Dialogs
{
    public interface IOpenFileDialogAdapter
    {
        Stream OpenFile();
        string FileName { get; set; }
        bool? ShowDialog();
    }
}