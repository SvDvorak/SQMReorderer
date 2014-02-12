using System;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public class OpenSqmFileDialog
    {
        private readonly IOpenFileDialogAdapter _openFileDialog;
        private readonly ISqmFileImporter _sqmFileImporter;

        public OpenSqmFileDialog(
            IOpenFileDialogAdapter openFileDialog,
            ISqmFileImporter sqmFileImporter)
        {
            _openFileDialog = openFileDialog;
            _sqmFileImporter = sqmFileImporter;
        }

        public SqmContents ShowDialog()
        {
            _openFileDialog.ShowDialog();

            var fileStream = _openFileDialog.OpenFile();

            return _sqmFileImporter.Import(fileStream);
        }
    }
}