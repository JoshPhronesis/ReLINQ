using System;
using System.Text;
using NUnit.Framework;

namespace ReLINQ.UnitTests
{
    public class SelectTests
    {
        [Test]
        public void SimpleProjectionToDifferentType()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(x => x.ToString());
            CollectionAssert.AreEqual(result, new String[] { "1", "5", "2" });
        }
    }
}
