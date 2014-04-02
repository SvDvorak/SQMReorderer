using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMImportExport.ArmA2;
using SQMReorderer.Gui.ViewModels;
using SQMReorderer.Gui.ViewModels.ArmA2;

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
                    Groups = new ObservableCollection<IGroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    Name = "Alpha",
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha FTL"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman (AT)"},
                                                new List<VehicleViewModelBase>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    Name = "Bravo",
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo FTL"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman (AT)"},
                                                new List<VehicleViewModelBase>())
                                        }
                                }
                        }
                });

            teams.Add(new TeamViewModel
                {
                    Side = "OPFOR",
                    Groups = new ObservableCollection<IGroupViewModel>
                        {
                            new GroupViewModel
                                {
                                    Name = "Alpha",
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha FTL"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Alpha Rifleman (AT)"},
                                                new List<VehicleViewModelBase>())
                                        }
                                },
                            new GroupViewModel
                                {
                                    Name = "Bravo",
                                    Vehicles = new ObservableCollection<VehicleViewModelBase>
                                        {
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo FTL"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman"},
                                                new List<VehicleViewModelBase>()),
                                            new VehicleViewModel(new Vehicle {VehicleName = "Bravo Rifleman (AT)"},
                                                new List<VehicleViewModelBase>())
                                        }
                                }
                        }
                });

            Teams = teams;
        }
    }
}