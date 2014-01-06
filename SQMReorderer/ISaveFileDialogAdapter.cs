using System.IO;

namespace SQMReorderer
{
    public interface ISaveFileDialogAdapter
    {
        void ShowDialog();
        Stream OpenFile();
    }
}