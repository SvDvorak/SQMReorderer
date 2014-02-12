namespace SQMReorderer.Core.Streams
{
    public interface IStreamWriterAdapter
    {
        void Write(string text);
        void Flush();
    }
}