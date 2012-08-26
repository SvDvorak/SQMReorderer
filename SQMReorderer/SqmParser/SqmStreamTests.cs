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
        private List<string> _singleContextInput = new List<string>
        {
            "class Item0",
            "{",
            "side=\"SomeText1\";",
            "text=\"SomeText2\";",
            "};"
        };

        private List<string> _multipleContextsInput = new List<string>
        {
            "class Item0",
            "{",
            "text=\"OuterContextText1\";",
            "class Item1",
            "{",
            "text=\"InnerContextText1\";",
            "};",
            "text=\"OuterContextText2\";",
            "};"
        };

        [Test]
        public void Expect_stream_to_return_correct_header()
        {
            var stream = new SqmStream(_singleContextInput);

            stream.StepIntoInnerContext();

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

            stream.StepIntoInnerContext();

            Assert.IsFalse(stream.IsAtEndOfContext);
        }

        [Test]
        public void Expect_stream_to_ignore_end_brackets_where_start_bracket_is_on_same_line()
        {
            var bracketedInput = new List<string>
            {
                @"position[]={4860.271,265.40967,6457.3115};",
                "side=\"SomeText1\";"
            };

            var stream = new SqmStream(bracketedInput);

            Assert.IsFalse(stream.IsAtEndOfContext);
        }

        [Test]
        public void Expect_stream_to_stop_at_end_of_context()
        {
            var stream = new SqmStream(_singleContextInput);

            stream.StepIntoInnerContext();

            stream.NextLineInContext();
            stream.NextLineInContext();

            Assert.IsTrue(stream.IsAtEndOfContext);

            stream.NextLineInContext();
            stream.NextLineInContext();

            Assert.IsTrue(stream.IsAtEndOfContext);
        }

        [Test]
        public void Expect_stream_to_return_all_lines_in_context_with_single_context()
        {
            var stream = new SqmStream(_singleContextInput);

            stream.StepIntoInnerContext();

            var row1Expected = "side=\"SomeText1\";";
            var row1Actual = "";
            var row2Expected = "text=\"SomeText2\";";
            var row2Actual = "";

            var row1Regex = new Regex(row1Expected);
            var row2Regex = new Regex(row2Expected);

            var matchResult = stream.MatchCurrentLine(row1Regex, x => row1Actual = x.Value);
            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(row1Expected, row1Actual);
            stream.NextLineInContext();

            matchResult = stream.MatchCurrentLine(row2Regex, x => row2Actual = x.Value);
            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(row2Expected, row2Actual);
        }

        [Test]
        public void Expect_stream_to_be_able_to_step_into_context()
        {
            var stream = new SqmStream(_multipleContextsInput);

            stream.StepIntoInnerContext();

            var outerContextRegex = new Regex("text=\"OuterContextText1\";");
            var innerContextRegex = new Regex("text=\"InnerContextText1\";");

            Assert.IsTrue(stream.IsCurrentLineMatch(outerContextRegex));

            stream.NextLineInContext();

            stream.StepIntoInnerContext();

            Assert.IsTrue(stream.IsCurrentLineMatch(innerContextRegex));
        }

        [Test]
        public void Expect_stream_to_skip_over_sub_item_when_going_to_next_line()
        {
            var stream = new SqmStream(_multipleContextsInput);

            stream.StepIntoInnerContext();

            var outerContext2Regex = new Regex("text=\"OuterContextText2\";");

            stream.NextLineInContext();
            stream.NextLineInContext();

            Assert.IsTrue(stream.IsCurrentLineMatch(outerContext2Regex));
        }

        [Test]
        public void Expect_stream_to_not_skip_property_line_with_brackets_at_end_of_item()
        {
            var bracketedPropertyInput = new List<string>
            {
                "class Item0",
                "{",
                "side=\"SomeText1\";",
                "bracketProperty={116};",
                "};"
            };

            var stream = new SqmStream(bracketedPropertyInput);

            stream.StepIntoInnerContext();

            var bracketPropertyLineRegex = new Regex("bracketProperty");

            stream.NextLineInContext();

            Assert.IsTrue(stream.IsCurrentLineMatch(bracketPropertyLineRegex));
        }
    }
}
