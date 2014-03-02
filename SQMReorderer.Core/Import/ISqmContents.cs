namespace SQMReorderer.Core.Import
{
    public interface ISqmContents
    {
        int? Version { get; set; }

        void Accept(ISqmContentsVisitor visitor);
    }
}