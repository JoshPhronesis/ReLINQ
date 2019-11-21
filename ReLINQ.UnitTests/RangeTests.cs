using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ReLINQ.UnitTests
{
    public class RangeTests
    {
        [Test]
        public void ReturnsElementsInRange()
        {
            var sequence = ReLinq.Range(2, 3);
            CollectionAssert.AreEqual(new int[]{2,3,4}, sequence);
        }

        [Test]
        public void ThrowsArgumentOutOfRangeException()
        {
            var count =  -1;
            Assert.Throws<ArgumentOutOfRangeException>(() => ReLinq.Range(1, count));
        }
    }
}
