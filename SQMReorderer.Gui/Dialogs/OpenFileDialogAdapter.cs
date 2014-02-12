using System.IO;
using Microsoft.Win32;

namespace SQMReorderer.Gui.Dialogs
{
    public class OpenFileDialogAdapter : IOpenFileDialogAdapter
    {
        private readonly OpenFileDialog _openFileDialog;

        public OpenFileDialogAdapter()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public string FileName
        {
            get { return _openFileDialog.FileName; }
            set { _openFileDialog.FileName = value; }
        }

        public string Filter
        {
            get { return _openFileDialog.Filter; }
            set { _openFileDialog.Filter = value; }
        }

        public Stream OpenFile()
        {
            return _openFileDialog.OpenFile();
        }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }
    }
}