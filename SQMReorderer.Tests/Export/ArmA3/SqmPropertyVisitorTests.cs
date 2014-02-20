using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core;
using SQMReorderer.Core.Export.ArmA3;

namespace SQMReorderer.Tests.Export.ArmA3
{
    [TestFixture]
    public class SqmPropertyVisitorTests
    {
        [Test]
        public void Expect_all_property_visitors_to_print_correctly()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var stringPropertyText = propertyVisitor.Visit("side", "WEST");
            var vectorPropertyText = propertyVisitor.Visit("position", new Vector(1, 2, 3));
            var intPropertyText = propertyVisitor.Visit("leader", 1);
            var doublePropertyText = propertyVisitor.Visit("skill", 0.60000002);
            var intListPropertyText = propertyVisitor.Visit("synchronizations", new List<int>() { 1, 2, 3 });
            var stringListPropertyText = propertyVisitor.Visit("addOns", new List<string>() {"brown", "blur"});

            const string correctStringListText = 
                "addOns[]=\n" +
                "{\n" +
                "\"brown\",\n" +
                "\"blur\"\n" +
                "};\n";

            Assert.AreEqual("side=\"WEST\";\n", stringPropertyText);
            Assert.AreEqual("position[]={1,2,3};\n", vectorPropertyText);
            Assert.AreEqual("leader=1;\n", intPropertyText);
            Assert.AreEqual("skill=0.60000002;\n", doublePropertyText);
            Assert.AreEqual("synchronizations[]={1,2,3};\n", intListPropertyText);
            Assert.AreEqual(correctStringListText, stringListPropertyText);
        }

        [Test]
        public void Expect_empty_strings_when_passed_missing_property_value()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var stringPropertyText = propertyVisitor.Visit("side", (string)null);
            var vectorPropertyText = propertyVisitor.Visit("position", (Vector)null);
            var intPropertyText = propertyVisitor.Visit("leader", (int?)null);
            var doublePropertyText = propertyVisitor.Visit("skill", (double?)null);
            var intListPropertyText = propertyVisitor.Visit("synchronizations", (List<int>)null);
            var stringListPropertyText = propertyVisitor.Visit("Effects", (List<string>)null);

            Assert.AreEqual("", stringPropertyText);
            Assert.AreEqual("", vectorPropertyText);
            Assert.AreEqual("", intPropertyText);
            Assert.AreEqual("", doublePropertyText);
            Assert.AreEqual("", intListPropertyText);
            Assert.AreEqual("", stringListPropertyText);
        }

        [Test]
        public void Expect_empty_strings_when_passed_empty_lists()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var intListPropertyText = propertyVisitor.Visit("synchronizations", new List<int>());
            var stringListPropertyText = propertyVisitor.Visit("Effects", new List<int>());

            Assert.AreEqual("", intListPropertyText);
            Assert.AreEqual("", stringListPropertyText);
        }
    }
}
