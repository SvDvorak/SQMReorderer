using System;

namespace SQMReorderer.Command
{
    public class ActionCanNotBeNullException : Exception
    {
        public ActionCanNotBeNullException() : base("Action passed to DelegateCommand cannot be null.") { }
    }
}