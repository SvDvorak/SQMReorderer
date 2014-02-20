using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3;
using SQMReorderer.Core.Import.ArmA3.Context;
using SQMReorderer.Core.Import.ArmA3.DataSetters;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class IntegerPropertySetterTests
    {
        private int? _value;
        private IntegerPropertySetter _integerPropertySetter;

        [SetUp]
        public void Setup()
        {
            _integerPropertySetter = new IntegerPropertySetter("camelot", x => _value = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = @"camelot=5";

            var matchResult = _integerPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(5, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = @"model=32";

            var matchResult = _integerPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual(32, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = @"camelot=itsonlyamodel";

            var matchResult = _integerPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
        }
    }
}
