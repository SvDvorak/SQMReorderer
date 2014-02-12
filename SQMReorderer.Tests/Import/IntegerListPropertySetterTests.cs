using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.SqmParser;
using SQMReorderer.Core.SqmParser.Context;
using SQMReorderer.Core.SqmParser.DataSetters;
using SQMReorderer.SqmParser.Context;

namespace SQMReorderer.SqmParser.DataSetters
{
    [TestFixture]
    class IntegerListPropertySetterTests
    {
        private List<int> _values;
        private IntegerListPropertySetter _integerListPropertySetter;

        [SetUp]
        public void Setup()
        {
            _values = null;
            _integerListPropertySetter = new IntegerListPropertySetter("thedarkknight", x => _values = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = @"thedarkknight[]={116,117,120};";

            var matchResult = _integerListPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(116, _values[0]);
            Assert.AreEqual(117, _values[1]);
            Assert.AreEqual(120, _values[2]);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = @"model=32";

            var matchResult = _integerListPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreEqual(null, _values);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = @"thedarkknight=itsonlyamodel";

            var matchResult = _integerListPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreEqual(null, _values);
        }
    }
}
