using System;

namespace SQMReorderer.Core.Import.FileVersion
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("Empty file")
        {
        }
    }
}