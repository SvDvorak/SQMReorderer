using System;
using SQMImportExport.Common;
using SQMImportExport.Import;

namespace SQMReorderer.Gui.Dialogs
{
    public class OpenSqmFileDialog
    {
        private readonly IOpenFileDialogAdapter _openFileDialog;
        private readonly ISqmImporter _sqmImporter;
        private readonly IMessageBoxPresenter _messageBoxPresenter;

        public OpenSqmFileDialog(
            IOpenFileDialogAdapter openFileDialog,
            ISqmImporter sqmImporter,
            IMessageBoxPresenter messageBoxPresenter)
        {
            _openFileDialog = openFileDialog;
            _sqmImporter = sqmImporter;
            _messageBoxPresenter = messageBoxPresenter;

            _openFileDialog.Filter = "SQM Files (*.sqm)|*.sqm";
        }

        public string SelectedPath { get { return _openFileDialog.FileName; } }

        public SqmContentsBase ShowDialog()
        {
            SqmContentsBase sqmContents = null;
            var shouldSave = _openFileDialog.ShowDialog();

            if (shouldSave.HasValue && shouldSave.Value)
            {
                var fileStream = _openFileDialog.OpenFile();

                try
                {
                    if (fileStream.Length == 0)
                    {
                        throw new EmptyFileException();
                    }

                    sqmContents = _sqmImporter.Import(fileStream);
                }
                catch (Exception exception)
                {
                    _messageBoxPresenter.ShowError("Unable to read file: " + exception.Message);
                }

                fileStream.Close();
            }

            return sqmContents;
        }
    }
}