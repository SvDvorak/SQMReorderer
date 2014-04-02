using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using SQMImportExport.Import.ArmA3.ResultObjects;
using SQMReorderer.Gui.ViewModels;
using SQMReorderer.Gui.ViewModels.ArmA3;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class ViewModelToContentReordererTests
    {
        [Test]
        public void Returns_contents_unchanged_if_view_model_is_unchanged()
        {
            var vehicle1 = CreateVehicle(0);
            var vehicle2 = CreateVehicle(1);

            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle1
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle2
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel
                {
                    Groups = new ObservableCollection<IGroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[0],
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(vehicle1,
                                                new List<VehicleViewModelBase>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[1],
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(vehicle2,
                                                new List<VehicleViewModelBase>())
                                        }
                                }
                        }
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<ITeamViewModel> { teamViewModel });

            Assert.AreEqual(0, mission.Groups[0].Vehicles.First().Id);
            Assert.AreEqual(1, mission.Groups[1].Vehicles.First().Id);
        }

        [Test]
        public void Moves_vehicle_if_it_has_been_moved_in_view_model()
        {
            var vehicle1 = CreateVehicle(0);
            var vehicle2 = CreateVehicle(1);

            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle1
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle2
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel
                {
                    Groups = new ObservableCollection<IGroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[0],
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(vehicle2,
                                                new List<VehicleViewModelBase>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[1],
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(vehicle1,
                                                new List<VehicleViewModelBase>())
                                        }
                                }
                        }
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<ITeamViewModel> { teamViewModel });

            Assert.AreEqual(1, mission.Groups[0].Vehicles.First().Id);
            Assert.AreEqual(0, mission.Groups[1].Vehicles.First().Id);
        }

        [Test]
        public void Moves_vehicles_in_all_team_view_models()
        {
            var vehicle1 = CreateVehicle(0);
            var vehicle2 = CreateVehicle(1);
            var vehicle3 = CreateVehicle(2);
            var vehicle4 = CreateVehicle(3);

            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle1
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle2
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle3
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle4
                                        }
                                }
                        }
                };

            var teamViewModels = new List<ITeamViewModel>
                {
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<IGroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[0],
                                            Vehicles = new ObservableCollection<VehicleViewModelBase>
                                                {
                                                    new VehicleViewModel(vehicle2,
                                                        new List<VehicleViewModelBase>())
                                                }
                                        },
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[1],
                                            Vehicles = new ObservableCollection<VehicleViewModelBase>
                                                {
                                                    new VehicleViewModel(vehicle1,
                                                        new List<VehicleViewModelBase>())
                                                }
                                        }
                                }
                        },
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<IGroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[2],
                                            Vehicles = new ObservableCollection<VehicleViewModelBase>
                                                {
                                                    new VehicleViewModel(vehicle4,
                                                        new List<VehicleViewModelBase>())
                                                }
                                        },
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[3],
                                            Vehicles = new ObservableCollection<VehicleViewModelBase>
                                                {
                                                    new VehicleViewModel(vehicle3,
                                                        new List<VehicleViewModelBase>())
                                                }
                                        }
                                }
                        },
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, teamViewModels);

            Assert.AreEqual(1, mission.Groups[0].Vehicles.First().Id);
            Assert.AreEqual(0, mission.Groups[1].Vehicles.First().Id);
            Assert.AreEqual(3, mission.Groups[2].Vehicles.First().Id);
            Assert.AreEqual(2, mission.Groups[3].Vehicles.First().Id);
        }

        [Test]
        public void Item_numbers_are_updated_when_order_is_changed()
        {
            var vehicle1 = CreateVehicle(0);
            var vehicle2 = CreateVehicle(1);

            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            vehicle1,
                                            vehicle2
                                        }
                                }
                        }
                };

            var teamViewModels = new List<ITeamViewModel>
                {
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<IGroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[0],
                                            Vehicles = new ObservableCollection<VehicleViewModelBase>
                                                {
                                                    new VehicleViewModel(vehicle2,
                                                        new List<VehicleViewModelBase>()),
                                                    new VehicleViewModel(vehicle1,
                                                        new List<VehicleViewModelBase>())
                                                }
                                        },
                                }
                        },
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, teamViewModels);

            Assert.AreEqual(0, mission.Groups[0].Vehicles.First().Number);
            Assert.AreEqual(1, mission.Groups[0].Vehicles.First().Id);
            Assert.AreEqual(1, mission.Groups[0].Vehicles.Second().Number);
            Assert.AreEqual(0, mission.Groups[0].Vehicles.Second().Id);
        }

        [Test]
        public void Item_numbers_are_continous_over_different_teams()
        {
            var group1 = CreateVehicle(0);
            var group2 = CreateVehicle(1);

            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            group1,
                            group2
                        }
                };

            var teamViewModels = new List<ITeamViewModel>
                {
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<IGroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = group1,
                                        },
                                }
                        },
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<IGroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = group2,
                                        },
                                }
                        }
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, teamViewModels);

            Assert.AreEqual(0, mission.Groups[0].Number);
            Assert.AreEqual(1, mission.Groups[1].Number);
        }

        [Test]
        public void Vehicle_without_matching_view_model_item_is_not_updated()
        {
            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            CreateVehicle(0)
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel();

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<ITeamViewModel> { teamViewModel });

            Assert.AreEqual(0, mission.Groups[0].Vehicles.First().Id);
        }

        private Vehicle CreateVehicle(int id)
        {
            return new Vehicle
                {
                    Number = id,
                    Id = id
                };
        }
    }
}