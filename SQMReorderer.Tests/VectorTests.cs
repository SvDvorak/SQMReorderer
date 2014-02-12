using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMReorderer.Core;

namespace SQMReorderer
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Expect_same_values_in_properties_when_creating_a_vector()
        {
            var vector = new Vector(1, 2, 3);

            Assert.AreEqual(1, vector.X);
            Assert.AreEqual(2, vector.Y);
            Assert.AreEqual(3, vector.Z);
        }
    }
}
