using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmExport
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
            var synchronizationsPropertyText = propertyVisitor.Visit("synchronizations", new List<int>() { 1, 2, 3 });

            Assert.AreEqual("side=\"WEST\";\n", stringPropertyText);
            Assert.AreEqual("position[]={1,2,3};\n", vectorPropertyText);
            Assert.AreEqual("leader=1;\n", intPropertyText);
            Assert.AreEqual("skill=0.60000002;\n", doublePropertyText);
            Assert.AreEqual("synchronizations[]={1,2,3};\n", synchronizationsPropertyText);
        }
    }
}
