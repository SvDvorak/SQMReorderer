using System;
using System.IO;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Export
{
    public class SqmFileExporter : ISqmFileExporter
    {
        private readonly ArmA2.ISqmFileExporter _arma2Exporter;
        private readonly ArmA3.ISqmFileExporter _arma3Exporter;
        private readonly IFileVersionRetriever _fileVersionRetriever;

        public SqmFileExporter(ArmA2.ISqmFileExporter arma2Exporter, ArmA3.ISqmFileExporter arma3Exporter, IFileVersionRetriever fileVersionRetriever)
        {
            _arma2Exporter = arma2Exporter;
            _arma3Exporter = arma3Exporter;
            _fileVersionRetriever = fileVersionRetriever;
        }

        public void Export(Stream stream, SqmContents contents)
        {
            try
            {
                var contentsVersion = _fileVersionRetriever.GetVersion(contents.Version.Value);
                if (contentsVersion == FileVersion.ArmA2)
                {
                    _arma2Exporter.Export(stream, contents);
                }
                else if (contentsVersion == FileVersion.ArmA3)
                {
                    _arma3Exporter.Export(stream, contents);
                }
            }
            catch (Exception)
            {
                throw new SqmExportException("Unable to export version " + contents.Version + ", version is unknown");
            }
        }
    }
}