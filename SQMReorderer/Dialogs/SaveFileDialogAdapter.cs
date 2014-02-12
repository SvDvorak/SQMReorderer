using System.IO;
using Microsoft.Win32;

namespace SQMReorderer.Gui.Dialogs
{
    public class SaveFileDialogAdapter : ISaveFileDialogAdapter
    {
        private readonly SaveFileDialog _saveFileDialog;

        public SaveFileDialogAdapter()
        {
            _saveFileDialog = new SaveFileDialog();
        }

        public bool? ShowDialog()
        {
            return _saveFileDialog.ShowDialog();
        }

        public Stream OpenFile()
        {
            return _saveFileDialog.OpenFile();
        }
    }
}