using System.Collections.Generic;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class IntelParserTests
    {
        private IntelParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new IntelParser();
        }

        [Test]
        public void Expect_is_intel_to_return_true_on_correct_intel_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Intel" });

            var isItemElement = _parser.IsIntelElement(stream);

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Expect_is_intel_to_return_false_on_incorrect_intel_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Markers" });

            var isItemElement = _parser.IsIntelElement(stream);

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void Expect_parser_to_parse_all_properties()
        {
            var inputText = new List<string>
                {
                    @"class Intel\n",
                    @"{\n",
                    @"briefingName=""[co04]local_hostility_v2_oa"";\n",
                    @"briefingDescription=""Destroy stolen ammocrates and truck"";\n",
                    @"startWeather=0.19207704;\n",
                    @"forecastWeather=0.25;\n",
                    @"year=2008;\n",
                    @"month=10;\n",
                    @"day=11;\n",
                    @"hour=16;\n",
                    @"minute=0;\n",
                    @"};\n",
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            var intelResult = _parser.ParseIntel(stream);

            Assert.AreEqual("[co04]local_hostility_v2_oa", intelResult.BriefingName);
            Assert.AreEqual("Destroy stolen ammocrates and truck", intelResult.BriefingDescription);
            Assert.AreEqual(0.19207704, intelResult.StartWeather);
            Assert.AreEqual(0.25, intelResult.ForecastWeather);
            Assert.AreEqual(2008, intelResult.Year);
            Assert.AreEqual(10, intelResult.Month);
            Assert.AreEqual(11, intelResult.Day);
            Assert.AreEqual(16, intelResult.Hour);
            Assert.AreEqual(0, intelResult.Minute);
        }

        [Test]
        public void Expect_exception_if_property_not_found()
        {
            var inputText = new List<string>
                {
                    "class Intel",
                    "{",
                    "derpderp=\"herpderp\"",
                    "};"
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            Assert.Throws<SqmParseException>(() => _parser.ParseIntel(stream));
        }
    }
}
