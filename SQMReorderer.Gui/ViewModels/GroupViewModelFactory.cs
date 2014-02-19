using System.Collections.Generic;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public interface IGroupViewModelsFactory
    {
        List<GroupViewModel> Create(List<Vehicle> vehicles);
    }

    public class GroupViewModelsFactory : IGroupViewModelsFactory
    {
        public List<GroupViewModel> Create(List<Vehicle> vehicles)
        {
            return new List<GroupViewModel>();
        }
    }
}