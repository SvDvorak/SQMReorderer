namespace SQMReorderer.Core.Import.ResultObjects
{
    public interface ISqmContents
    {
        int? Version { get; set; }

        void Accept(ISqmContentsVisitor visitor);
    }

    public interface ISqmContentsVisitor
    {
        void Visit(ArmA2.ResultObjects.SqmContents arma2Contents);
        void Visit(ArmA3.ResultObjects.SqmContents arma3Contents);
    }
}