using System.Collections.Generic;
using NUnit.Framework;

namespace SQMReorderer.SqmParser
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

            var sqmParseResult = _parser.Parse(new SqmStream(inputText));

            Assert.AreEqual(11, sqmParseResult.Version);
        }

        [Test]
        public void Expect_parser_to_parse_mission()
        {
            var inputText = new List<string>
                {
                    "version=11;\n",
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var sqmParseResult = _parser.Parse(new SqmStream(inputText));

            Assert.IsNotNull(sqmParseResult.Mission);
        }
    }
}
