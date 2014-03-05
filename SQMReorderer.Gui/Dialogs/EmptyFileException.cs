using System;

namespace SQMReorderer.Gui.Dialogs
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("Empty file")
        {
        }
    }
}