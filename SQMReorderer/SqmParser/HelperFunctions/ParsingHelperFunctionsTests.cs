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
        public void Expect_to_return_failure_on_no_brackets_found()
        {
            var inputText = new string[0];

            var nextBracketsPositions = _parsingHelperFunctions.GetNextBracketsPositions(inputText, 0);

            Assert.IsFalse(nextBracketsPositions.Success);
        }

        [Test]
        public void Expect_success_on_matching_brackets_in_text()
        {
            var inputText = new[]
                {
                    "{",
                    "}"
                };

            var nextBracketsPositions = _parsingHelperFunctions.GetNextBracketsPositions(inputText, 0);
            
            Assert.IsTrue(nextBracketsPositions.Success);
            Assert.AreEqual(0, nextBracketsPositions.StartBracketPosition);
            Assert.AreEqual(1, nextBracketsPositions.EndBracketPosition);
        }

        [Test]
        public void Expect_success_on_matching_brackets_and_ignoring_noise_in_text()
        {
            var inputText = new[]
                {
                    "version=1",
                    "class   {",
                    "name=Mission",
                    "description }",
                    "END OF LINE"
                };

            var nextBracketsPositions = _parsingHelperFunctions.GetNextBracketsPositions(inputText, 0);

            Assert.IsTrue(nextBracketsPositions.Success);
            Assert.AreEqual(1, nextBracketsPositions.StartBracketPosition);
            Assert.AreEqual(3, nextBracketsPositions.EndBracketPosition);
        }

        [Test]
        public void Expect_to_return_failure_on_only_finding_start_bracket()
        {
            var inputText = new[]
                {
                    "{"
                };

            var nextBracketsPositions = _parsingHelperFunctions.GetNextBracketsPositions(inputText, 0);

            Assert.IsFalse(nextBracketsPositions.Success);
        }

        [Test]
        public void Expect_to_return_failure_on_only_finding_end_bracket()
        {
            var inputText = new[]
                {
                    "}"
                };

            var nextBracketsPositions = _parsingHelperFunctions.GetNextBracketsPositions(inputText, 0);

            Assert.IsFalse(nextBracketsPositions.Success);
        }
    }
}
