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
            var inputText = new[]
                {
                    "version=11;\n",
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var sqmParseResult = _parser.Parse(inputText);

            Assert.AreEqual(11, sqmParseResult.Version);
        }

        [Test]
        public void Expect_parser_to_parse_single_group()
        {
            var inputText = new[]
                {
                    "version=11;\n",
                    "class Mission\n",
                    "{\n",
                    "class Groups\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "side=\"LOGIC\";\n",
                    "class Vehicles\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "};\n",
                    "};\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var sqmParseResult = _parser.Parse(inputText);

            Assert.AreEqual(1, sqmParseResult.Mission.Groups.Count);
            Assert.AreEqual("LOGIC", sqmParseResult.Mission.Groups[0].Side);
        }

        [Ignore]
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
