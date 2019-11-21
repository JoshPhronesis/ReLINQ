using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ReLINQ.UnitTests
{
    public class ReLinqTests
    {
        [Test]
        public void Where_NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(x => x > 5));
        }
        [Test]
        public void Where_NullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
        }
        [Test]
        public void Where_SimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = source.Where(x => x < 4);
            CollectionAssert.AreEqual(result, new int[]{1,3,2,1}); 
        }

        [Test]
        public void Where_QueryExpressionSimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = from x in source
                where x < 4
                select x;
            CollectionAssert.AreEqual(result, new int[] { 1, 3, 2, 1 });
        }

        [Test]
        public void Select_SimpleProjectionToDifferentType()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(x => x.ToString());
            CollectionAssert.AreEqual(result, new String[] { "1", "5", "2" });
        }
    }
}