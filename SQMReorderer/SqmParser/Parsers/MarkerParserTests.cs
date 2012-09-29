using System.Collections.Generic;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class MarkerParserTests
    {
        private MarkerParser _parser;

        private readonly List<string> completeSimpleMarkerItemText = new List<string>
            {
                "class Item0\n",
                "{\n",
                "position[]={414,16,412};\n",
                "name=\"TargetAreaCenter\";\n",
                "markerType=\"ELLIPSE\";\n",
                "type=\"Empty\";\n",
                "text=\"Destroy equipment\";",
                "fillName=\"Border\";\n",
                "a=40;\n",
                "b=30;\n",
                "drawBorder=1;\n",
                "angle=202.98199;\n",
                "};\n"
            };

        private SqmStream _completeSimpleMarkerStream;

        [SetUp]
        public void Setup()
        {
            _parser = new MarkerParser();

            _completeSimpleMarkerStream = new SqmStream(completeSimpleMarkerItemText);
        }

        [Test]
        public void Expect_parser_to_parse_all_marker_item_properties()
        {
            _completeSimpleMarkerStream.StepIntoInnerContext();

            var markerResult = _parser.ParseItemElement(_completeSimpleMarkerStream);

            Assert.AreEqual(414, markerResult.Position.X);
            Assert.AreEqual(16, markerResult.Position.Y);
            Assert.AreEqual(412, markerResult.Position.Z);
            Assert.AreEqual(0, markerResult.Number);
            Assert.AreEqual("TargetAreaCenter", markerResult.Name);
            Assert.AreEqual("ELLIPSE", markerResult.MarkerType);
            Assert.AreEqual("Empty", markerResult.Type);
            Assert.AreEqual("Destroy equipment", markerResult.Text);
            Assert.AreEqual("Border", markerResult.FillName);
            Assert.AreEqual(40, markerResult.A);
            Assert.AreEqual(30, markerResult.B);
            Assert.AreEqual(1, markerResult.DrawBorder);
            Assert.AreEqual(202.98199, markerResult.Angle);
        }
    }
}
