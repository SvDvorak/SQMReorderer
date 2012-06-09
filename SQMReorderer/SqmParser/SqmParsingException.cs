using System;

namespace SQMReorderer.SqmParser
{
    public class SqmParsingException : Exception
    {
        public SqmParsingException(string errorMessage) : base(errorMessage)
        {
        }
    }
}