using System;

namespace SQMReorderer.Core.Export
{
    internal class SqmExportException : Exception
    {
        public SqmExportException(string message) : base(message)
        {
        }
    }
}