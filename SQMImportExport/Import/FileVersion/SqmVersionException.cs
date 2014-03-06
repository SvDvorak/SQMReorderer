using System;

namespace SQMImportExport.Import.FileVersion
{
    internal class SqmVersionException : Exception
    {
        public SqmVersionException(string message) : base(message)
        {
        }
    }
}