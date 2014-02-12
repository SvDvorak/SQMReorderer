namespace SQMReorderer
{
    public interface IStreamWriterAdapter
    {
        void Write(string text);
        void Flush();
    }
}