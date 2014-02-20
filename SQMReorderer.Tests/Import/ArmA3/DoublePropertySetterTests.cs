﻿using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3;
using SQMReorderer.Core.Import.ArmA3.Context;
using SQMReorderer.Core.Import.ArmA3.DataSetters;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class DoublePropertySetterTests
    {
        private double? _value;
        private DoublePropertySetter _doublePropertySetter;

        [SetUp]
        public void Setup()
        {
            _doublePropertySetter = new DoublePropertySetter("camelot", x => _value = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = @"camelot=5.45";

            var matchResult = _doublePropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(5.45, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = @"model=32.42";

            var matchResult = _doublePropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual(32.42, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = @"camelot=itsonlyamodel";

            var matchResult = _doublePropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual("itsonlyamodel", _value);
        }

        [Test]
        public void Expect_failure_with_odd_string_value()
        {
            var inputText = @"She'sAWitch!";

            var matchResult = _doublePropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
        }
    }
}
