using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Gui
{
    public class TestMainViewModel : MainViewModel
    {
        public TestMainViewModel()
        {
            var teams = new List<TeamViewModel>();

            teams.Add(new TeamViewModel
                {
                    Side = "BLUFOR",
                    Groups = new ObservableCollection<GroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    Name = "Alpha",
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha FTL"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman (AT)"},
                                                new List<VehicleViewModel>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    Name = "Bravo",
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo FTL"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman (AT)"},
                                                new List<VehicleViewModel>())
                                        }
                                }
                        }
                });

            teams.Add(new TeamViewModel
                {
                    Side = "OPFOR",
                    Groups = new ObservableCollection<GroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    Name = "Alpha",
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha FTL"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman (AT)"},
                                                new List<VehicleViewModel>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    Name = "Bravo",
                                    Vehicles = new ObservableCollection<VehicleViewModel>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo FTL"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman"},
                                                new List<VehicleViewModel>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman (AT)"},
                                                new List<VehicleViewModel>())
                                        }
                                }
                        }
                });

            Teams = teams;
        }
    }
}