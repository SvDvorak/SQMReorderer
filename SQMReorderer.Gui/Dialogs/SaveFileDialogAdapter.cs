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

        public string Filter
        {
            get { return _saveFileDialog.Filter; }
            set { _saveFileDialog.Filter = value; }
        }

        public bool AddExtension
        {
            get { return _saveFileDialog.AddExtension; }
            set { _saveFileDialog.AddExtension = value; }
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