﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA2.Parsers.Effects;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA2
{
    [TestFixture]
    public class EffectsParserTests
    {
        private EffectsParser _sut;
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _sut = new EffectsParser();
            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Context_is_correct_when_passed_effects()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Effects", "{\n", "};\n" });
            var isCorrectContext = _sut.IsCorrectContext(context);

            Assert.IsTrue(isCorrectContext);
        }

        [Test]
        public void Context_is_incorrect_when_passed_something_other_than_effects()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Markers", "{\n", "};\n" });
            var isCorrectContext = _sut.IsCorrectContext(context);

            Assert.IsFalse(isCorrectContext);
        }

        [Test]
        public void Empty_list_when_effects_is_empty()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Effects", "{\n", "};\n" });
            var parsedEffects = _sut.ParseContext(context);

            Assert.IsEmpty(parsedEffects);
        }

        [Test]
        public void Reads_all_items_in_effects()
        {
            var context = _contextCreator.CreateContext(new List<string>
                {
                    "class Effects",
                    "{\n",
                    "one line with text",
                    "and another one",
                    "text=otherText",
                    "};\n"
                });
            var parsedEffects = _sut.ParseContext(context);

            Assert.IsEmpty(parsedEffects);
        }
    }
}
