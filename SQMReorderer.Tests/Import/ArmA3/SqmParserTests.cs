﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3;
using SQMReorderer.Core.Import.ArmA3.Context;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class SqmParserTests
    {
        private readonly SqmParser _parser = new SqmParser();

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Expect_parser_to_parse_version()
        {
            var inputText = new List<string>
                {
                    "version=11;\n",
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var contextCreator = new SqmContextCreator();
            var parseResult = _parser.ParseContext(contextCreator.CreateRootContext(inputText));

            Assert.AreEqual(11, parseResult.Version);
        }

        [Test]
        public void Expect_parser_to_parse_mission()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var contextCreator = new SqmContextCreator();
            var parseResult = _parser.ParseContext(contextCreator.CreateRootContext(inputText));

            Assert.IsNotNull(parseResult.Mission);
        }

        [Test]
        public void Expect_intro_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Intro\n",
                    "{\n",
                    "randomSeed=5875250;\n",
                    "class Intel\n",
                    "{\n",
                    "year=2008;\n",
                    "};\n",
                    "};\n"
                };

            var contextCreator = new SqmContextCreator();
            var parseResult = _parser.ParseContext(contextCreator.CreateRootContext(inputText));

            Assert.IsNotNull(parseResult.Intro);
            Assert.AreEqual(2008, parseResult.Intro.Intel.Year);
        }

        [Test]
        public void Expect_outro_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class OutroWin\n",
                    "{\n",
                    "randomSeed=5875250;\n",
                    "class Intel\n",
                    "{\n",
                    "year=2008;\n",
                    "};\n",
                    "};\n",
                    "class OutroLoose\n",
                    "{\n",
                    "randomSeed=5875250;\n",
                    "class Intel\n",
                    "{\n",
                    "year=2007;\n",
                    "};\n",
                    "};\n"
                };

            var contextCreator = new SqmContextCreator();
            var parseResult = _parser.ParseContext(contextCreator.CreateRootContext(inputText));

            Assert.IsNotNull(parseResult.OutroWin);
            Assert.AreEqual(2008, parseResult.OutroWin.Intel.Year);

            Assert.IsNotNull(parseResult.OutroLose);
            Assert.AreEqual(2007, parseResult.OutroLose.Intel.Year);
        }
    }
}
