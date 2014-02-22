using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Export
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

            _memoryStream = Substitute.For<MemoryStream>();
            _saveFileDialogAdapter.OpenFile().Returns(_memoryStream);
        }

        [Test]
        public void Shows_save_file_dialog()
        {
            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _saveFileDialogAdapter.Received().ShowDialog();
        }

        [Test]
        public void Opens_selected_path_when_user_selects_file_path()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _saveFileDialogAdapter.Received().OpenFile();
        }

        [Test]
        public void Exports_sqm_contents_using_stream()
        {
            var sqmContents = new SqmContents();
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmFileDialog.ShowDialog(sqmContents);

            _sqmFileExporter.Received().Export(_memoryStream, sqmContents);
        }

        [Test]
        public void Closes_stream_after_exporting()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _memoryStream.Received().Close();
        }

        [Test]
        public void Does_nothing_if_user_presses_cancel()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(false);

            _saveSqmFileDialog.ShowDialog(new SqmContents());

            _saveFileDialogAdapter.DidNotReceive().OpenFile();
        }

        [Test]
        public void Automatically_appends_sqm_file_ending()
        {
            Assert.AreEqual(true, _saveFileDialogAdapter.AddExtension);
            Assert.AreEqual("SQM File (*.sqm)|*.sqm", _saveFileDialogAdapter.Filter);
        }
    }
}