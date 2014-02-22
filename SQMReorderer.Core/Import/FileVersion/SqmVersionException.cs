using System;

namespace SQMReorderer.Core.Import.FileVersion
{
    public class SqmVersionException : Exception
    {
        public SqmVersionException(string message) : base(message)
        {
        }
    }
}