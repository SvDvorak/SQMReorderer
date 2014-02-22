using System;

namespace SQMReorderer.Core.Export
{
    public class SqmExportException : Exception
    {
        public SqmExportException(string message) : base(message)
        {
        }
    }
}