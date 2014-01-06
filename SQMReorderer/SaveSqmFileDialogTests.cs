using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    [TestFixture]
    public class SaveSqmFileDialogTests
    {
        private SaveSqmFileDialog _saveSqmFileDialog;
        private ISaveFileDialogAdapter _saveFileDialogAdapter;
        private ISqmFileExporter _sqmFileExporter;

        private MemoryStream _memoryStream;

        [SetUp]
        public void Setup()
        {
            _saveFileDialogAdapter = Substitute.For<ISaveFileDialogAdapter>();
            _sqmFileExporter = Substitute.For<ISqmFileExporter>();
            _saveSqmFileDialog = new SaveSqmFileDialog(_saveFileDialogAdapter, _sqmFileExporter);

            _memoryStream = new MemoryStream();
        }

        [Test]
        public void Shows_save_file_dialog()
        {
            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _saveFileDialogAdapter.Received().ShowDialog();
        }

        [Test]
        public void Opens_selected_path()
        {
            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _saveFileDialogAdapter.Received().OpenFile();
        }

        [Test]
        public void Exports_sqm_contents_using_stream()
        {
            _saveFileDialogAdapter.OpenFile().Returns(_memoryStream);
            var sqmContents = new SqmContents();

            _saveSqmFileDialog.ShowDialog(sqmContents);

            _sqmFileExporter.Received().Export(_memoryStream, sqmContents);
        }
    }
}