using NUnit.Framework;
using SQMReorderer.Core.SqmParser;
using SQMReorderer.Core.SqmParser.Context;
using SQMReorderer.Core.SqmParser.DataSetters;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class StringPropertySetterTests
    {
        private string _value;
        private StringPropertySetter _stringPropertySetter;

        [SetUp]
        public void Setup()
        {
            _stringPropertySetter = new StringPropertySetter("camelot", x => _value = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = "camelot=\"bravesirrobin\"";

            var matchResult = _stringPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual("bravesirrobin", _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = "model=32.42";

            var matchResult = _stringPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual(32.42, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = "camelot=itsonlyamodel";

            var matchResult = _stringPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual("itsonlyamodel", _value);
        }
    }
}
