﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA3.Parsers
{
    public abstract class ParserBase<TParseResult> : IParser<TParseResult>
        where TParseResult : new()
    {
        public List<IContextSetter> ContextSetters { get; private set; }
        public List<LineSetterBase> PropertySetters { get; private set; }

        protected TParseResult ParseResult { get; set; }

        protected abstract Regex HeaderRegex { get; }

        protected ParserBase()
        {
            ContextSetters = new List<IContextSetter>();
            PropertySetters = new List<LineSetterBase>();
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
                    var resultTypeName = typeof(TParseResult).Name;
                    throw new SqmParseException(string.Format("Unknown context in {0}: {1}", resultTypeName, subContext.Header.Trim()));
                }
            }

            foreach (var line in context.Lines)
            {
                var parseResult = new Result();

                foreach (var propertySetter in PropertySetters)
                {
                    parseResult = propertySetter.SetValueIfLineMatches(line);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    var resultTypeName = typeof (TParseResult).Name;
                    throw new SqmParseException(string.Format("Unknown property in {0}: {1}", resultTypeName, line.ToString().Trim()));
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
