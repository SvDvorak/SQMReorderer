namespace SQMImportExport.Import
{
    public interface ISqmContentsVisitor
    {
        void Visit(ArmA2.ResultObjects.SqmContents arma2Contents);
        void Visit(ArmA3.ResultObjects.SqmContents arma3Contents);
    }

    public interface ISqmContentsVisitor<out T>
    {
        T Visit(ArmA2.ResultObjects.SqmContents arma2Contents);
        T Visit(ArmA3.ResultObjects.SqmContents arma3Contents);
    }
}