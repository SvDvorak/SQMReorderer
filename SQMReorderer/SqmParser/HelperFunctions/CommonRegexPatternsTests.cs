using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.HelperFunctions
{
    [TestFixture]
    public class CommonRegexPatternsTests
    {
        private Regex _doubleRegex;

        [SetUp]
        public void Setup()
        {
            _doubleRegex = new Regex(CommonRegexPatterns.DoublePattern);
        }

        [Test]
        public void Expect_non_double_value_to_not_match_double_pattern()
        {
            Assert.IsFalse(_doubleRegex.IsMatch("shrubbery"));
        }

        [Test]
        public void Expect_integer_value_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("15"));
        }

        [Test]
        public void Expect_simple_decimal_double_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("1.5"));
        }

        [Test]
        public void Expect_string_with_decimal_but_no_significant_number_to_not_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch(".15"));
        }

        [Test]
        public void Expect_negative_double_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("-1.5"));
        }
    }
}
