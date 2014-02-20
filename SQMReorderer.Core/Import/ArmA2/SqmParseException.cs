using System;

namespace SQMReorderer.Core.Import.ArmA2
{
    [Serializable]
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}