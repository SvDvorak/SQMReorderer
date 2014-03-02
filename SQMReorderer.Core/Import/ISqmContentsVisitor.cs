namespace SQMReorderer.Core.Import
{
    public interface ISqmContentsVisitor
    {
        void Visit(ArmA2.ResultObjects.SqmContents arma2Contents);
        void Visit(ArmA3.ResultObjects.SqmContents arma3Contents);
    }
}