﻿using NUnit.Framework;
using SQMReorderer.Core;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class VectorPropertySetterTests
    {
        private Vector _value;
        private VectorPropertySetter _vectorPropertySetter;

        [SetUp]
        public void Setup()
        {
            _vectorPropertySetter = new VectorPropertySetter("position", x => _value = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = "position[]={5533.8467,143.18413,6350.1045}";

            var matchResult = _vectorPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(5533.8467, _value.X);
            Assert.AreEqual(143.18413, _value.Y);
            Assert.AreEqual(6350.1045, _value.Z);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = "model={5533.8467,143.18413,6350.1045}";

            var matchResult = _vectorPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = "position[]=itsonlyamodel";

            var matchResult = _vectorPropertySetter.SetPropertyIfMatch(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
        }
    }
}