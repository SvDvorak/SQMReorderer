using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Effects
{
    public class EffectsParserFactory : IItemParserFactory<List<string>>
    {
        public IParser<List<string>> CreateParser()
        {
            return new EffectsParser();
        }
    }
}