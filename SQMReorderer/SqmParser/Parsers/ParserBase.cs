using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.DataSetters;

namespace SQMReorderer.SqmParser.Parsers
{
    public abstract class ParserBase<TParseResult> : IParser<TParseResult>
        where TParseResult : new()
    {
        public List<IContextSetter> ContextSetters { get; private set; }
        public List<PropertySetterBase> PropertySetters { get; private set; }

        protected TParseResult ParseResult { get; set; }

        protected abstract Regex HeaderRegex { get; }

        protected ParserBase()
        {
            ContextSetters = new List<IContextSetter>();
            PropertySetters = new List<PropertySetterBase>();
        }

        public bool IsCorrectContext(SqmContext context)
        {
            return context.IsHeaderMatch(HeaderRegex);
        }

        public virtual TParseResult ParseContext(SqmContext context)
        {
            ParseResult = new TParseResult();

            foreach (var subContext in context.SubContexts)
            {
                var parseResult = CustomParseContext(subContext);

                if (parseResult != Result.Failure)
                {
                    continue;
                }

                foreach (var contextSetter in ContextSetters)
                {
                    parseResult = contextSetter.SetContextIfMatch(subContext);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown context: " + subContext.Header);
                }
            }

            foreach (var line in context.Lines)
            {
                var parseResult = new Result();

                foreach (var propertySetter in PropertySetters)
                {
                    parseResult = propertySetter.SetPropertyIfMatch(line);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + line);
                }
            }

            return ParseResult;
        }

        protected virtual Result CustomParseContext(SqmContext context)
        {
            return Result.Failure;
        }
    }
}
