﻿using System;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.Parsers;

namespace SQMReorderer.Core.Import.DataSetters
{
    public class ContextSetter<TParseResult> : IContextSetter
    {
        private readonly IParser<TParseResult> _parser;
        private readonly Action<TParseResult> _contextSetter;

        public ContextSetter(IParser<TParseResult> parser, Action<TParseResult> contextSetter)
        {
            _parser = parser;
            _contextSetter = contextSetter;
        }

        public Result SetContextIfMatch(SqmContext context)
        {
            if(_parser.IsCorrectContext(context))
            {
                var contextResult = _parser.ParseContext(context);

                _contextSetter(contextResult);

                return Result.Success;
            }
            else
            {
                return Result.Failure;
            }
        }
    }
}