using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    [TestFixture]
    public class ItemViewModelTests
    {
        [Test]
        public void Expect_working_item_viewmodel_value_properties()
        {
            var item = new Item();

            item.Side = "SIDE";
            item.Vehicle = "VEHICLE";
            item.Rank = "RANK";
            item.Text = "TEXT";
            item.Description = "DESC";

            var itemViewModel = new ItemViewModel(item);

            Assert.AreEqual("SIDE", itemViewModel.Side);
            Assert.AreEqual("VEHICLE", itemViewModel.Vehicle);
            Assert.AreEqual("RANK", itemViewModel.Rank);
            Assert.AreEqual("TEXT", itemViewModel.Text);
            Assert.AreEqual("DESC", itemViewModel.Description);
        }
    }
}
