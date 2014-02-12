using System;

namespace SQMReorderer.Core.SqmParser
{
    [Serializable]
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}