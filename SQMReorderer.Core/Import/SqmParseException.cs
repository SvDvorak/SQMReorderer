using System;

namespace SQMReorderer.Core.Import
{
    [Serializable]
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}