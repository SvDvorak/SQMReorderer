using System;
using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    [TestFixture]
    public class VehicleViewModelTests
    {
        [Test]
        public void Expect_working_item_viewmodel_value_properties()
        {
            var vehicle = new Vehicle();

            vehicle.VehicleName = "VEHICLE";
            vehicle.Rank = "RANK";
            vehicle.Text = "TEXT";
            vehicle.Description = "DESC";

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModel>());

            Assert.AreEqual("VEHICLE", itemViewModel.VehicleName);
            Assert.AreEqual("RANK", itemViewModel.Rank);
            Assert.AreEqual("TEXT", itemViewModel.Text);
            Assert.AreEqual("DESC", itemViewModel.Description);
        }
    }
}
