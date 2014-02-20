using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class GroupViewModelsFactoryTests
    {
        private GroupViewModelsFactory _sut;
        private IVehicleViewModelsFactory _vehicleViewModelsFactory;

        [SetUp]
        public void Setup()
        {
            _vehicleViewModelsFactory = Substitute.For<IVehicleViewModelsFactory>();
            _vehicleViewModelsFactory.Create(Arg.Any<List<Vehicle>>()).Returns(new List<VehicleViewModel>());

            _sut = new GroupViewModelsFactory(_vehicleViewModelsFactory);
        }

        [Test]
        public void Creates_group_view_model_for_each_vehicle()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle(),
                    new Vehicle(),
                    new Vehicle()
                };

            var groupViewModel = _sut.Create(vehicles);

            Assert.AreEqual(3, groupViewModel.Count);
        }

        [Test]
        public void Creates_vehicle_view_models_for_multiple_groups()
        {
            var vehicle1 = new Vehicle();
            var vehicle2 = new Vehicle();
            var vehicle3 = new Vehicle();
            var vehicle4 = new Vehicle();

            var vehicles = new List<Vehicle>
                {
                    new Vehicle
                        {
                            Vehicles = new List<Vehicle>
                                {
                                    vehicle1,
                                    vehicle2,
                                }
                        },
                    new Vehicle
                        {
                            Vehicles = new List<Vehicle>
                                {
                                    vehicle3,
                                    vehicle4,
                                }
                        }
                };

            _vehicleViewModelsFactory.Create(vehicles[0].Vehicles).Returns(new List<VehicleViewModel>
                {
                    new VehicleViewModel(vehicle1, new List<VehicleViewModel>()),
                    new VehicleViewModel(vehicle2, new List<VehicleViewModel>())
                });

            _vehicleViewModelsFactory.Create(vehicles[1].Vehicles).Returns(new List<VehicleViewModel>
                {
                    new VehicleViewModel(vehicle3, new List<VehicleViewModel>()),
                    new VehicleViewModel(vehicle4, new List<VehicleViewModel>())
                });

            var groupViewModels = _sut.Create(vehicles);

            Assert.AreEqual(2, groupViewModels[0].Vehicles.Count);
            Assert.AreEqual(vehicle1, groupViewModels[0].Vehicles[0].Vehicle);
            Assert.AreEqual(vehicle2, groupViewModels[0].Vehicles[1].Vehicle);

            Assert.AreEqual(2, groupViewModels[1].Vehicles.Count);
            Assert.AreEqual(vehicle3, groupViewModels[1].Vehicles[0].Vehicle);
            Assert.AreEqual(vehicle4, groupViewModels[1].Vehicles[1].Vehicle);
        }

        [Test]
        public void Uses_enumerated_group_names_when_multiple_groups_are_missing_vehicles()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle(),
                    new Vehicle(),
                    new Vehicle()
                };

            var groupViewModels = _sut.Create(vehicles);

            Assert.AreEqual("Group 1", groupViewModels[0].Name);
            Assert.AreEqual("Group 2", groupViewModels[1].Name);
            Assert.AreEqual("Group 3", groupViewModels[2].Name);
        }

        [Test]
        public void Sets_connected_vehicle_for_group()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle(),
                    new Vehicle(),
                    new Vehicle()
                };

            var groupViewModels = _sut.Create(vehicles);

            Assert.AreEqual(vehicles[0], groupViewModels[0].ConnectedVehicle);
            Assert.AreEqual(vehicles[1], groupViewModels[1].ConnectedVehicle);
            Assert.AreEqual(vehicles[2], groupViewModels[2].ConnectedVehicle);
        }
    }
}