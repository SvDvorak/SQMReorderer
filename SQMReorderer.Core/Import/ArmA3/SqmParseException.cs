using System;

namespace SQMReorderer.Core.Import.ArmA3
{
    [Serializable]
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}