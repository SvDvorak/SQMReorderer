﻿using System.Collections.Generic;
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
        private IUnitViewModelFactory _unitViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _unitViewModelFactory = Substitute.For<IUnitViewModelFactory>();
            _unitViewModelFactory.Create(Arg.Any<List<Vehicle>>()).Returns(new List<VehicleViewModel>());

            _sut = new GroupViewModelsFactory(_unitViewModelFactory);
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
        public void Creates_unit_view_models_for_multiple_groups()
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

            _unitViewModelFactory.Create(vehicles[0].Vehicles).Returns(new List<VehicleViewModel>
                {
                    new VehicleViewModel(vehicle1, new List<VehicleViewModel>()),
                    new VehicleViewModel(vehicle2, new List<VehicleViewModel>())
                });

            _unitViewModelFactory.Create(vehicles[1].Vehicles).Returns(new List<VehicleViewModel>
                {
                    new VehicleViewModel(vehicle3, new List<VehicleViewModel>()),
                    new VehicleViewModel(vehicle4, new List<VehicleViewModel>())
                });

            var groupViewModels = _sut.Create(vehicles);

            Assert.AreEqual(2, groupViewModels[0].Units.Count);
            Assert.AreEqual(vehicle1, groupViewModels[0].Units[0].Vehicle);
            Assert.AreEqual(vehicle2, groupViewModels[0].Units[1].Vehicle);

            Assert.AreEqual(2, groupViewModels[1].Units.Count);
            Assert.AreEqual(vehicle3, groupViewModels[1].Units[0].Vehicle);
            Assert.AreEqual(vehicle4, groupViewModels[1].Units[1].Vehicle);
        }

        [Test]
        public void Uses_enumerated_group_names_when_multiple_groups_are_missing_units()
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
    }
}