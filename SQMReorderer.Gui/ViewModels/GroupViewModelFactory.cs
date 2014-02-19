using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class GroupViewModelsFactory : IGroupViewModelsFactory
    {
        private readonly IUnitViewModelFactory _unitViewModelFactory;

        private int _groupEnumerator = 0;

        public GroupViewModelsFactory(IUnitViewModelFactory unitViewModelFactory)
        {
            _unitViewModelFactory = unitViewModelFactory;
        }

        public List<GroupViewModel> Create(List<Vehicle> vehicles)
        {
            _groupEnumerator = 0;

            return vehicles
                .Select(vehicle => new GroupViewModel
                    {
                        Name = GetGroupName(),
                        Units = new ObservableCollection<VehicleViewModel>(
                            _unitViewModelFactory.Create(vehicle.Vehicles))
                    })
                .ToList();
        }

        private string GetGroupName()
        {
            _groupEnumerator += 1;
            return "Group " + _groupEnumerator;
        }
    }
}