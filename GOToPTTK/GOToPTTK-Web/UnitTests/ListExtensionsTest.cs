using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GOToPTTK.Model.Extensions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class ListExtensionsTest
    {
        [Test]
        public void ThrowExceptionWhenListArgumentIsNull()
        {
            IList<string> list = null;
            Assert.Throws<ArgumentNullException>(() => ListExtensions.PaginateList(list));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void ThrowExceptionWhenPageSizeArgumentIsEqualOrLessThanZero(int pageSize)
        {
            IList<string> list = new List<string>(){"a", "b"};
            Assert.Throws<ArgumentException>(() => ListExtensions.PaginateList(list, pageSize));
        }
        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void ThrowExceptionWhenPageArgumentIsLessThanOne(int page)
        {
            IList<string> list = new List<string>() { "a", "b" };
            Assert.Throws<ArgumentException>(() => ListExtensions.PaginateList(list, page: page));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(10)]
        public void ReturnListWithSizeLessOrEqualThanPageSizeArgument(int pageSize)
        {
            IList<int> list = new List<int>() {1, 2, 3, 4, 5, 6};
            Assert.IsTrue(ListExtensions.PaginateList(list, pageSize).Count <= pageSize);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void ReturnCorrectPage(int page)
        {
            int pageSize = 2;
            var pages = new List<List<int>>()
            {
                new List<int>() {1, 2},
                new List<int>() {3, 4}
            };
            var list = pages[0].Concat(pages[1]).ToList(); 
            Assert.IsTrue(ListExtensions.PaginateList(list, pageSize, page).SequenceEqual(pages[page - 1]));

        }
    }
}
