using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class CombinedVehicleViewModelTests
    {
        [Test]
        public void Null_is_returned_for_properties_when_no_vehicles_are_available()
        {
            var sut = new CombinedVehicleViewModel(new List<VehicleViewModel>());

            Assert.AreEqual(null, sut.VehicleName);
            Assert.AreEqual(null, sut.Rank);
            Assert.AreEqual(null, sut.Text);
            Assert.AreEqual(null, sut.Description);
        }

        [Test]
        public void Null_is_returned_when_not_all_vehicles_have_same_property_value()
        {
            var vehicle1 = new Vehicle()
                {
                    VehicleName = "name1",
                    Rank = "rank1",
                    Text = "text1",
                    Description = "description1"
                };

            var vehicle2 = new Vehicle()
                {
                    VehicleName = "name2",
                    Rank = "rank2",
                    Text = "text2",
                    Description = "description2"
                };

            var vehicles = CreateViewModelList(vehicle1, vehicle2);
            var sut = new CombinedVehicleViewModel(vehicles);

            Assert.AreEqual(null, sut.VehicleName);
            Assert.AreEqual(null, sut.Rank);
            Assert.AreEqual(null, sut.Text);
            Assert.AreEqual(null, sut.Description);
        }

        [Test]
        public void Value_is_returned_when_all_vehicles_have_same_property_value()
        {
            var vehicle1 = new Vehicle()
                {
                    VehicleName = "name",
                    Rank = "rank",
                    Text = "text",
                    Description = "description"
                };

            var vehicle2 = new Vehicle()
                {
                    VehicleName = "name",
                    Rank = "rank",
                    Text = "text",
                    Description = "description"
                };

            var vehicles = CreateViewModelList(vehicle1, vehicle2);
            var sut = new CombinedVehicleViewModel(vehicles);

            Assert.AreEqual(vehicle1.VehicleName, sut.VehicleName);
            Assert.AreEqual(vehicle1.Rank, sut.Rank);
            Assert.AreEqual(vehicle1.Text, sut.Text);
            Assert.AreEqual(vehicle1.Description, sut.Description);
        }

        [Test]
        public void Setting_value_sets_on_all_vehicles()
        {
            var vehicle1 = new Vehicle();
            var vehicle2 = new Vehicle();

            var vehicles = CreateViewModelList(vehicle1, vehicle2);
            var sut = new CombinedVehicleViewModel(vehicles);

            sut.VehicleName = "name";
            sut.Rank = "rank";
            sut.Text = "text";
            sut.Description = "description";

            Assert.AreEqual("name", vehicle1.VehicleName);
            Assert.AreEqual("name", vehicle2.VehicleName);

            Assert.AreEqual("rank", vehicle1.Rank);
            Assert.AreEqual("rank", vehicle2.Rank);

            Assert.AreEqual("text", vehicle1.Text);
            Assert.AreEqual("text", vehicle2.Text);

            Assert.AreEqual("description", vehicle1.Description);
            Assert.AreEqual("description", vehicle2.Description);
        }

        private List<VehicleViewModel> CreateViewModelList(Vehicle vehicle1, Vehicle vehicle2)
        {
            return new List<VehicleViewModel>()
            {
                new VehicleViewModel(vehicle1, new List<VehicleViewModel>()),
                new VehicleViewModel(vehicle2, new List<VehicleViewModel>())
            };
        }
    }
}
