using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.HelperFunctions
{
    [TestFixture]
    public class ParsingHelperFunctionsTests
    {
        private ParsingHelperFunctions _parsingHelperFunctions;

        [SetUp]
        public void SetUp()
        {
            _parsingHelperFunctions = new ParsingHelperFunctions();
        }

        [Test]
        public void Expect_line_to_be_start_bracket_given_start_bracket()
        {
            var isLineStartBracket = _parsingHelperFunctions.IsLineStartBracket("  {  ");

            Assert.IsTrue(isLineStartBracket);
        }

        [Test]
        public void Expect_line_to_be_end_bracket_given_end_bracket()
        {
            var isLineEndBracket = _parsingHelperFunctions.IsLineEndBracket("  }  ");

            Assert.IsTrue(isLineEndBracket);
        }
    }
}
