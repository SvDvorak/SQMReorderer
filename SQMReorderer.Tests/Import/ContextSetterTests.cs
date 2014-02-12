﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.SqmParser;
using SQMReorderer.Core.SqmParser.Context;
using SQMReorderer.Core.SqmParser.DataSetters;
using SQMReorderer.Core.SqmParser.Parsers.Intel;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class ContextSetterTests
    {
        private Intel _result;
        private ContextSetter<Intel> _intelContextSetter;

        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _result = null;
            _contextCreator = new SqmContextCreator();

            _intelContextSetter = new ContextSetter<Intel>(new IntelParser(), x => _result = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = new List<string>
                {
                    "class Intel\n",
                    "{\n",
                    "year=2008;\n",
                    "};\n",
                };

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = _intelContextSetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(2008, _result.Year);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_context()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = _intelContextSetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.IsNull(_result);
        }
    }
}
