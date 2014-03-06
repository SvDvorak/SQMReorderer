using System;

namespace SQMImportExport.Import
{
    [Serializable]
    internal class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}