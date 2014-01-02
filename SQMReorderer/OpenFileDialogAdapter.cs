using System.IO;
using Microsoft.Win32;

namespace SQMReorderer
{
    public class OpenFileDialogAdapter : IOpenFileDialogAdapter
    {
        private readonly OpenFileDialog _openFileDialog;

        public OpenFileDialogAdapter()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public Stream OpenFile()
        {
            return _openFileDialog.OpenFile();
        }

        public string FileName
        {
            get { return _openFileDialog.FileName; }
            set { _openFileDialog.FileName = value; }
        }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }
    }
}