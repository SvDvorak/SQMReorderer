using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace SQMReorderer.SqmParser
{
    [TestFixture]
    public class SqmStreamTests
    {
        private string[] _singleContextInput = new[]
        {
            "class Item0",
            "{",
            "side=\"SomeText1\";",
            "text=\"SomeText2\";",
            "};"
        };

        private string[] _multipleContextsInput = new[]
        {
            "class Item0",
            "{",
            "text=\"OuterContextText\";",
            "class Item1",
            "{",
            "text=\"InnerContextText\";",
            "};",
            "};"
        };

        [Test]
        public void Expect_stream_to_return_correct_header()
        {
            var stream = new SqmStream(_singleContextInput);

            var isHeaderMatch = stream.IsHeaderMatch(new Regex("class Item0"));
            Assert.IsTrue(isHeaderMatch);

            var headerMatchValue = "";
            stream.MatchHeader(new Regex("class Item0"), x => headerMatchValue = x.Value);
            Assert.AreEqual("class Item0", headerMatchValue);
        }

        [Test]
        public void Expect_non_empty_stream_to_not_be_at_end_of_context()
        {
            var stream = new SqmStream(_singleContextInput);

            Assert.IsTrue(stream.IsAtStartOfContext);
            Assert.IsFalse(stream.IsAtEndOfContext);
        }

        [Test]
        public void Expect_stream_to_stop_at_end_of_context()
        {
            var stream = new SqmStream(_singleContextInput);

            var expectedFinalRowInContext = "text=\"SomeText2\";";

            stream.NextLineInContext();

            var isCurrentLineMatch = stream.IsCurrentLineMatch(new Regex(expectedFinalRowInContext));
            Assert.IsTrue(stream.IsAtEndOfContext);
            Assert.IsTrue(isCurrentLineMatch);

            stream.NextLineInContext();
            stream.NextLineInContext();

            isCurrentLineMatch = stream.IsCurrentLineMatch(new Regex(expectedFinalRowInContext));
            Assert.IsFalse(stream.IsAtStartOfContext);
            Assert.IsTrue(stream.IsAtEndOfContext);
            Assert.IsTrue(isCurrentLineMatch);
        }

        [Test]
        public void Expect_stream_to_return_all_lines_in_context_with_single_context()
        {
            var stream = new SqmStream(_singleContextInput);

            var row1Expected = "side=\"SomeText1\";";
            var row1Actual = "";
            var row2Expected = "text=\"SomeText2\";";
            var row2Actual = "";

            var row1Regex = new Regex(row1Expected);
            var row2Regex = new Regex(row2Expected);

            stream.MatchCurrentLine(row1Regex, x => row1Actual = x.Value);
            Assert.IsTrue(stream.IsCurrentLineMatch(row1Regex));
            Assert.AreEqual(row1Expected, row1Actual);
            stream.NextLineInContext();

            stream.MatchCurrentLine(row2Regex, x => row2Actual = x.Value);
            Assert.IsTrue(stream.IsCurrentLineMatch(row2Regex));
            Assert.AreEqual(row2Expected, row2Actual);
        }

        [Test]
        public void Expect_stream_to_be_able_to_step_into_context()
        {
            var stream = new SqmStream(_multipleContextsInput);

            var outerContextRegex = new Regex("text=\"OuterContextText\";");
            var innerContextRegex = new Regex("text=\"InnerContextText\";");

            Assert.IsTrue(stream.IsCurrentLineMatch(outerContextRegex));
            Assert.IsTrue(stream.CanStepIntoNextContext);
            
            stream.StepIntoInnerContext();

            Assert.IsTrue(stream.IsCurrentLineMatch(innerContextRegex));
        }
    }
}
