namespace SQMReorderer.Core.StreamHelpers
{
    public interface IStreamWriterAdapter
    {
        void Write(string text);
        void Flush();
    }
}