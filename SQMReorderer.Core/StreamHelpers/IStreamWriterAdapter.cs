namespace SQMReorderer.Core.StreamHelpers
{
    internal interface IStreamWriterAdapter
    {
        void Write(string text);
        void Flush();
    }
}