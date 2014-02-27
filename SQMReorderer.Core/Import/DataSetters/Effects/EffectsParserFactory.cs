using System.Collections.Generic;

namespace SQMReorderer.Core.Import.DataSetters.Effects
{
    public class EffectsParserFactory : IItemParserFactory<List<string>>
    {
        public IParser<List<string>> CreateParser()
        {
            return new EffectsParser();
        }
    }
}