using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
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

        [Test]
        public void Description_is_header_when_it_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle()
                {
                    Description = "desc",
                    VehicleName = "vehicleName",
                    Side = "Side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModel>());

            Assert.AreEqual("desc", itemViewModel.Header);
        }

        [Test]
        public void Vehicle_name_is_header_when_description_is_empty_or_null_and_vehicle_name_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle()
                {
                    VehicleName = "vehicleName",
                    Side = "Side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModel>());

            Assert.AreEqual("vehicleName", itemViewModel.Header);
        }

        [Test]
        public void Side_is_header_when_vehicle_name_is_empty_or_null_and_side_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle()
                {
                    Side = "side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModel>());

            Assert.AreEqual("side", itemViewModel.Header);
        }
    }
}
