using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Effects
{
    public class EffectsParser : ParserBase<List<string>>
    {
        private readonly Regex _effectsRegex = new Regex(@"class Effects", RegexOptions.Compiled);

        public EffectsParser()
        {
            LineSetters.Add(new StringLineSetter("(?<value>.*)", x => ParseResult.Add(x)));
        }

        protected override Regex HeaderRegex
        {
            get { return _effectsRegex; }
        }
    }
}