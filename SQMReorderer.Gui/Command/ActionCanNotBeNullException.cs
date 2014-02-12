using System;

namespace SQMReorderer.Gui.Command
{
    public class ActionCanNotBeNullException : Exception
    {
        public ActionCanNotBeNullException() : base("Action passed to DelegateCommand cannot be null.") { }
    }
}