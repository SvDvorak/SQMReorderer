using System;

namespace SQMImportExport.Export
{
    internal class SqmExportException : Exception
    {
        public SqmExportException(string message) : base(message)
        {
        }
    }
}