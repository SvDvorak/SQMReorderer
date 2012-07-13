using System;

namespace SQMReorderer.SqmParser
{
    [Serializable]
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}