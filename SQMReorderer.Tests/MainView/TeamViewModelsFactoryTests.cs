using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class TeamViewModelsFactoryTests
    {
        private TeamViewModelsFactory _sut;
        private IGroupViewModelsFactory _groupViewModelsFactory;

        [SetUp]
        public void Setup()
        {
            _groupViewModelsFactory = Substitute.For<IGroupViewModelsFactory>();
            _groupViewModelsFactory.Create(Arg.Any<List<Vehicle>>()).Returns(new List<GroupViewModel>());
            _sut = new TeamViewModelsFactory(_groupViewModelsFactory);
        }

        [Test]
        public void No_teams_are_created_when_there_are_no_group_items()
        {
            var vehicles = new List<Vehicle>();

            var teamViewModels = _sut.Create(vehicles);

            Assert.IsEmpty(teamViewModels);
        }

        [Test]
        public void Blufor_team_is_created_when_there_is_one_west_vehicle()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle
                        {
                            Side = "WEST"
                        }
                };

            var teamViewModels = _sut.Create(vehicles);

            Assert.AreEqual(1, teamViewModels.Count);
            Assert.AreEqual("BLUFOR", teamViewModels[0].Side);
        }

        [Test]
        public void Opfor_team_is_created_when_there_is_one_east_vehicle()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle
                        {
                            Side = "EAST"
                        }
                };

            var teamViewModels = _sut.Create(vehicles);

            Assert.AreEqual(1, teamViewModels.Count);
            Assert.AreEqual("OPFOR", teamViewModels[0].Side);
        }

        [Test]
        public void Independent_team_is_created_when_there_is_one_guer_vehicle()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle
                        {
                            Side = "GUER"
                        }
                };

            var teamViewModels = _sut.Create(vehicles);

            Assert.AreEqual(1, teamViewModels.Count);
            Assert.AreEqual("INDEPENDENT", teamViewModels[0].Side);
        }

        [Test]
        public void Civilian_team_is_created_when_there_is_one_civ_vehicle()
        {
            var vehicles = new List<Vehicle>
                {
                    new Vehicle
                        {
                            Side = "CIV"
                        }
                };

            var teamViewModels = _sut.Create(vehicles);

            Assert.AreEqual(1, teamViewModels.Count);
            Assert.AreEqual("CIVILIAN", teamViewModels[0].Side);
        }

        [Test]
        public void Creates_multiple_groups_in_team_when_there_are_multiple_vehicles()
        {
            var vehicle1 = new Vehicle();
            var vehicle2 = new Vehicle();
            var vehicle3 = new Vehicle();

            var vehicles = new List<Vehicle>
                {
                    vehicle1,
                    vehicle2,
                    vehicle3
                };

            var groupViewModels = new List<GroupViewModel>()
                {
                    new GroupViewModel(),
                    new GroupViewModel(),
                    new GroupViewModel()
                };

            _groupViewModelsFactory.Create(Arg.Is<List<Vehicle>>(x => x.SequenceEqual(vehicles))).Returns(groupViewModels);

            var teamViewModels = _sut.Create(vehicles);

            Assert.AreEqual(3, teamViewModels[0].Groups.Count());
            Assert.AreEqual(groupViewModels, teamViewModels[0].Groups);
        }
    }
}