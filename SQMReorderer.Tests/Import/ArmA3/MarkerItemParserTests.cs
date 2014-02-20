using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3.Context;
using SQMReorderer.Core.Import.ArmA3.Parsers.Marker;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class MarkerItemParserTests
    {
        private MarkerItemParser _parser;

        private readonly List<string> completeSimpleMarkerItemText = new List<string>
            {
                "class Item0\n",
                "{\n",
                "position[]={414,16,412};\n",
                "name=\"TargetAreaCenter\";\n",
                "text=\"Destroy equipment\";",
                "markerType=\"ELLIPSE\";\n",
                "type=\"Empty\";\n",
                "fillName=\"Border\";\n",
                "a=40;\n",
                "b=30;\n",
                "drawBorder=1;\n",
                "angle=202.98199;\n",
                "};\n"
            };

        private SqmContext _completeSimpleMarkerContext;

        [SetUp]
        public void Setup()
        {
            _parser = new MarkerItemParser();

            var contextCreator = new SqmContextCreator();

            _completeSimpleMarkerContext = contextCreator.CreateContext(completeSimpleMarkerItemText);
        }

        [Test]
        public void Expect_parser_to_parse_all_marker_item_properties()
        {
            var markerResult = _parser.ParseContext(_completeSimpleMarkerContext);

            Assert.AreEqual(0, markerResult.Number);
            Assert.AreEqual(414, markerResult.Position.X);
            Assert.AreEqual(16, markerResult.Position.Y);
            Assert.AreEqual(412, markerResult.Position.Z);
            Assert.AreEqual("TargetAreaCenter", markerResult.Name);
            Assert.AreEqual("Destroy equipment", markerResult.Text);
            Assert.AreEqual("ELLIPSE", markerResult.MarkerType);
            Assert.AreEqual("Empty", markerResult.Type);
            Assert.AreEqual("Border", markerResult.FillName);
            Assert.AreEqual(40, markerResult.A);
            Assert.AreEqual(30, markerResult.B);
            Assert.AreEqual(1, markerResult.DrawBorder);
            Assert.AreEqual(202.98199, markerResult.Angle);
        }
    }
}
