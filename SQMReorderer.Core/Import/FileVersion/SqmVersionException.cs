using System;

namespace SQMReorderer.Core.Import.FileVersion
{
    internal class SqmVersionException : Exception
    {
        public SqmVersionException(string message) : base(message)
        {
        }
    }
}