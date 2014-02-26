using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Effects
{
    public class EffectsParser : ParserBase<List<string>>
    {
        private readonly Regex _effectsRegex = new Regex(@"class Effects", RegexOptions.Compiled);

        protected override Regex HeaderRegex
        {
            get { return _effectsRegex; }
        }
    }
}