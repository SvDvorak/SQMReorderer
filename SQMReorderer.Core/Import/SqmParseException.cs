using System;

namespace SQMReorderer.Core.Import
{
    [Serializable]
    internal class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}