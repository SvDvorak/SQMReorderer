using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.ArmA3.ResultObjects;
using SQMReorderer.Gui.ViewModels;
using SQMReorderer.Gui.ViewModels.ArmA3;

namespace SQMReorderer.Tests.MainView.ArmA3
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
            vehicle.Init = "INIT";

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModelBase>());

            Assert.AreEqual("VEHICLE", itemViewModel.VehicleName);
            Assert.AreEqual("RANK", itemViewModel.Rank);
            Assert.AreEqual("TEXT", itemViewModel.Text);
            Assert.AreEqual("DESC", itemViewModel.Description);
            Assert.AreEqual("INIT", itemViewModel.Init);
        }

        [Test]
        public void Text_is_header_when_it_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle
                {
                    Text = "text",
                    VehicleName = "vehicleName",
                    Side = "side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModelBase>());

            Assert.AreEqual("text", itemViewModel.Header);
        }

        [Test]
        public void Vehicle_name_is_header_when_text_is_empty_or_null_and_vehicle_name_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle
                {
                    VehicleName = "vehicleName",
                    Side = "side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModelBase>());

            Assert.AreEqual("vehicleName", itemViewModel.Header);
        }

        [Test]
        public void Side_is_header_when_vehicle_name_is_empty_or_null_and_side_is_not_null_or_whitespace()
        {
            var vehicle = new Vehicle
                {
                    Side = "side"
                };

            var itemViewModel = new VehicleViewModel(vehicle, new List<VehicleViewModelBase>());

            Assert.AreEqual("side", itemViewModel.Header);
        }

        [Test]
        public void Fires_property_changed_for_header_when_property_is_changed()
        {
            var itemViewModel = new VehicleViewModel(new Vehicle(), new List<VehicleViewModelBase>());
            int headerChangedCount = 0;

            itemViewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "Header")
                    {
                        headerChangedCount += 1;
                    }
                };

            itemViewModel.VehicleName = "text";
            itemViewModel.Rank = "text";
            itemViewModel.Text = "text";
            itemViewModel.Description = "text";
            itemViewModel.Init = "text";

            Assert.AreEqual(5, headerChangedCount);
        }
    }
}