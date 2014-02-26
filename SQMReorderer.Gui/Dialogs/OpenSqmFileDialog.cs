using System;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.Dialogs
{
    public class OpenSqmFileDialog
    {
        private readonly IOpenFileDialogAdapter _openFileDialog;
        private readonly ISqmFileImporter _sqmFileImporter;
        private readonly IMessageBoxPresenter _messageBoxPresenter;

        public OpenSqmFileDialog(
            IOpenFileDialogAdapter openFileDialog,
            ISqmFileImporter sqmFileImporter,
            IMessageBoxPresenter messageBoxPresenter)
        {
            _openFileDialog = openFileDialog;
            _sqmFileImporter = sqmFileImporter;
            _messageBoxPresenter = messageBoxPresenter;

            _openFileDialog.Filter = "SQM Files (*.sqm)|*.sqm";
        }

        public SqmContents ShowDialog()
        {
            SqmContents sqmContents = null;
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

                    sqmContents = _sqmFileImporter.Import(fileStream);
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