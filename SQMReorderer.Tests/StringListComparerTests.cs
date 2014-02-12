using System.Collections.Generic;
using NUnit.Framework;

namespace SQMReorderer
{
    [TestFixture]
    public class StringListComparerTests
    {
        private List<string> _list1;
        private List<string> _list2;

        private StringListComparer _stringListComparer;

        [SetUp]
        public void Setup()
        {
            _list1 = new List<string>();
            _list2 = new List<string>();

            _stringListComparer = new StringListComparer();
        }

        [Test]
        public void Expect_empty_lists_to_be_same()
        {
            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsTrue(comparisonResult.IsSame);
        }

        [Test]
        public void Expect_list_with_more_rows_to_not_be_the_same()
        {
            _list1.Add("grool");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(0, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("grool", comparisonResult.ErrorRowInList1);
        }

        [Test]
        public void Expect_list_with_more_rows_to_not_be_the_same_when_lists_are_swapped()
        {
            _list2.Add("kewl");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(0, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("kewl", comparisonResult.ErrorRowInList2);
        }

        [Test]
        public void Expect_lists_with_one_different_row_to_not_be_the_same()
        {
            _list1.Add("c#");
            _list2.Add("java");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(0, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("c#", comparisonResult.ErrorRowInList1);
            Assert.AreEqual("java", comparisonResult.ErrorRowInList2);
        }

        [Test]
        public void Expect_list_with_differing_row_further_into_list_to_not_be_the_same()
        {
            _list1.Add("gordon");
            _list2.Add("gordon");
            _list1.Add("quake 2 engine");
            _list2.Add("source engine");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(1, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("quake 2 engine", comparisonResult.ErrorRowInList1);
            Assert.AreEqual("source engine", comparisonResult.ErrorRowInList2);
        }

        [Test]
        public void Expect_difference_to_be_found_first_even_if_one_list_is_longer()
        {
            _list1.Add("ACE");
            _list2.Add("ACE");
            _list1.Add("ACRE");
            _list2.Add("VON");
            _list1.Add("voice");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(1, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("ACRE", comparisonResult.ErrorRowInList1);
            Assert.AreEqual("VON", comparisonResult.ErrorRowInList2);
        }

        [Test]
        public void Expect_difference_to_be_found_first_even_if_one_list_is_longer_swapped()
        {
            _list1.Add("ACE");
            _list2.Add("ACE");
            _list1.Add("ACRE");
            _list2.Add("VON");
            _list2.Add("voice");

            var comparisonResult = _stringListComparer.Compare(_list1, _list2);

            Assert.IsFalse(comparisonResult.IsSame);
            Assert.AreEqual(1, comparisonResult.ErrorRowNumber);
            Assert.AreEqual("ACRE", comparisonResult.ErrorRowInList1);
            Assert.AreEqual("VON", comparisonResult.ErrorRowInList2);
        }
    }
}
