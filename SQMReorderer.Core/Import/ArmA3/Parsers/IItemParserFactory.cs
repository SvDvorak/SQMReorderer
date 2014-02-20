namespace SQMReorderer.Core.Import.ArmA3.Parsers
{
    // The item parser factory is required because we need to instantiate item parsers when they're used.
    // If they are instatiated in the constructor we will run into infinite loops.
    public interface IItemParserFactory<TParseResult>
    {
        IParser<TParseResult> CreateParser();
    }
}
