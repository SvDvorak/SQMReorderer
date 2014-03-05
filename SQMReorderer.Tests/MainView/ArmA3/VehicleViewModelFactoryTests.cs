using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;
using SQMReorderer.Gui.ViewModels.ArmA3;

namespace SQMReorderer.Tests.MainView.ArmA3
{
    [TestFixture]
    public class VehicleViewModelFactoryTests
    {
        [Test]
        public void Empty_list_when_no_vehicles_are_passed()
        {
            var sut = new VehicleViewModelsFactory();
            var vehicleViewModels = sut.Create(new List<Vehicle>());

            Assert.IsEmpty(vehicleViewModels);
        }

        [Test]
        public void Creates_view_model_for_each_vehicle()
        {
            var vehicle1 = new Vehicle();
            var vehicle2 = new Vehicle();
            var vehicle3 = new Vehicle();

            var vehicles = new List<Vehicle>()
                {
                    vehicle1,
                    vehicle2,
                    vehicle3
                };

            var sut = new VehicleViewModelsFactory();
            var vehicleViewModels = sut.Create(vehicles);

            Assert.AreEqual(vehicle1, vehicleViewModels[0].Vehicle);
            Assert.AreEqual(vehicle2, vehicleViewModels[1].Vehicle);
            Assert.AreEqual(vehicle3, vehicleViewModels[2].Vehicle);
        }
    }
}
