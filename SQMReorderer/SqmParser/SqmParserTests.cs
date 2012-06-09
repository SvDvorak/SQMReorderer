using NUnit.Framework;

namespace SQMReorderer.SqmParser
{
    [TestFixture]
    public class SqmParserTests
    {
        private SqmParser _parser = new SqmParser();

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void Expect_parser_to_parse_version()
        {
            var inputText = new[]
                {
                    "version=11;\n",
                    "class Mission\n",
                    "{\n",
                    "}\n"
                };

            var sqmParseResult = _parser.Parse(inputText);

            Assert.AreEqual(11, sqmParseResult.Version);
        }

        [Test]
        public void Expect_parser_to_parse_intel()
        {
            var inputText = new[]
                {
                    "class Mission\n",
                    "{\n",
                    "}\n"
                };

            var sqmParseResult = _parser.Parse(inputText);

            Assert.AreEqual(11, sqmParseResult.Intel);
        }
    }
}
