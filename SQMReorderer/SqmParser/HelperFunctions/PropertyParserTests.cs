using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.HelperFunctions
{
    [TestFixture]
    public class PropertyParserTests
    {
        [Test]
        public void Expect_parser_to_read_string_property()
        {
            var parser = new PropertyParser();

            var propertyName = "stringProperty";
            var line = "stringProperty=\"Text\"";
            var isPropertyRead = false;

            parser.ParseStringProperty(propertyName, line, x => isPropertyRead = true);

            Assert.IsTrue(isPropertyRead);
        }
    }
}
