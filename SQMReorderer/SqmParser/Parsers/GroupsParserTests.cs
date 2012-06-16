using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class GroupsParserTests
    {
        [Test]
        public void Expect_single_item_group_to_return_single_item()
        {
            var parser = new GroupsParser();

            var inputText = new[]
                              {
                                  "class Groups",
                                  "{",
                                  "items=25;",
                                  "class Item0",
                                  "{",
                                  "side=\"LOGIC\";",
                                  "};",
                                  "};"
                              };

            var stream = new SqmStream(inputText);
            var groupElement = parser.ParseGroupElement(stream);

            Assert.AreEqual(1, groupElement.Items.Count);
        }
    }
}
