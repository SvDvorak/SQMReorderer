using System.IO;

namespace SQMReorderer
{
    public interface IOpenFileDialogAdapter
    {
        Stream OpenFile();
        string FileName { get; set; }
        bool? ShowDialog();
    }
}