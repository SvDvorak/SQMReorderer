using NUnit.Framework;

namespace SQMReorderer
{
    [TestFixture]
    public class MultiLineTextBuilderTests
    {
        [Test]
        public void NoLinesAddedReturnsEmptyString()
        {
            var textBuilder = new MultiLineTextBuilder();

            Assert.AreEqual("", textBuilder.ToString());
        }

        [Test]
        public void SingleLineReturnsSingleLineOutput()
        {
            var textBuilder = new MultiLineTextBuilder();
            textBuilder.AddLine("firstLine");

            Assert.AreEqual("firstLine", textBuilder.ToString());
        }

        [Test]
        public void MultipleLinesReturnLinesWithLineBreaks()
        {
            var textBuilder = new MultiLineTextBuilder();

            textBuilder.AddLine("firstLine");
            textBuilder.AddLine("secondLine");

            Assert.AreEqual("firstLine\nsecondLine", textBuilder.ToString());
        }

        [Test]
        public void AddingNullAsLineGetsIgnored()
        {
            var textBuilder = new MultiLineTextBuilder();

            textBuilder.AddLine("firstLine");
            textBuilder.AddLine(null);
            textBuilder.AddLine("secondLine");

            Assert.AreEqual("firstLine\nsecondLine", textBuilder.ToString());
        }
    }
}