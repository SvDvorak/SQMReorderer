using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class ViewModelToContentReordererTests
    {
        [Test]
        public void Returns_contents_unchanged_if_view_model_is_unchanged()
        {
            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 1
                                                }
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 2
                                                }
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel
                {
                    Groups = new ObservableCollection<GroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[0],
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(mission.Groups[0].Vehicles[0],
                                                new List<VehicleViewModel>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[1],
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(mission.Groups[1].Vehicles[0],
                                                new List<VehicleViewModel>())
                                        }
                                }
                        }
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<TeamViewModel> { teamViewModel });

            Assert.AreEqual(1, mission.Groups[0].Vehicles[0].Id);
            Assert.AreEqual(2, mission.Groups[1].Vehicles[0].Id);
        }

        [Test]
        public void Moves_vehicle_if_it_has_been_moved_in_view_model()
        {
            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 1
                                                }
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 2
                                                }
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel
                {
                    Groups = new ObservableCollection<GroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[0],
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(mission.Groups[1].Vehicles[0],
                                                new List<VehicleViewModel>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    ConnectedVehicle = mission.Groups[1],
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(mission.Groups[0].Vehicles[0],
                                                new List<VehicleViewModel>())
                                        }
                                }
                        }
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<TeamViewModel> { teamViewModel });

            Assert.AreEqual(2, mission.Groups[0].Vehicles[0].Id);
            Assert.AreEqual(1, mission.Groups[1].Vehicles[0].Id);
        }

        [Test]
        public void Moves_vehicles_in_all_team_view_models()
        {
            var mission = new MissionState
                {
                    Groups = new List<Vehicle>
                        {
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 1
                                                }
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 2
                                                }
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 3
                                                }
                                        }
                                },
                            new Vehicle
                                {
                                    Vehicles = new List<Vehicle>
                                        {
                                            new Vehicle
                                                {
                                                    Id = 4
                                                }
                                        }
                                }
                        }
                };

            var teamViewModels = new List<TeamViewModel>
                {
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<GroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[0],
                                            Vehicles = new ObservableCollection<VehicleViewModel>
                                                {
                                                    new VehicleViewModel(mission.Groups[1].Vehicles[0],
                                                        new List<VehicleViewModel>())
                                                }
                                        },
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[1],
                                            Vehicles = new ObservableCollection<VehicleViewModel>
                                                {
                                                    new VehicleViewModel(mission.Groups[0].Vehicles[0],
                                                        new List<VehicleViewModel>())
                                                }
                                        }
                                }
                        },
                    new TeamViewModel
                        {
                            Groups = new ObservableCollection<GroupViewModel>
                                {
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[2],
                                            Vehicles = new ObservableCollection<VehicleViewModel>
                                                {
                                                    new VehicleViewModel(mission.Groups[3].Vehicles[0],
                                                        new List<VehicleViewModel>())
                                                }
                                        },
                                    new GroupViewModel
                                        {
                                            ConnectedVehicle = mission.Groups[3],
                                            Vehicles = new ObservableCollection<VehicleViewModel>
                                                {
                                                    new VehicleViewModel(mission.Groups[2].Vehicles[0],
                                                        new List<VehicleViewModel>())
                                                }
                                        }
                                }
                        },
                };

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, teamViewModels);

            Assert.AreEqual(2, mission.Groups[0].Vehicles[0].Id);
            Assert.AreEqual(1, mission.Groups[1].Vehicles[0].Id);
            Assert.AreEqual(4, mission.Groups[2].Vehicles[0].Id);
            Assert.AreEqual(3, mission.Groups[3].Vehicles[0].Id);

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
                                            new Vehicle
                                                {
                                                    Id = 1,
                                                }
                                        }
                                }
                        }
                };

            var teamViewModel = new TeamViewModel();

            var sut = new ViewModelToContentReorderer();

            sut.Reorder(mission, new List<TeamViewModel> { teamViewModel });

            Assert.AreEqual(1, mission.Groups[0].Vehicles[0].Id);
        }
    }
}