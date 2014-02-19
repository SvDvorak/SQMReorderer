using System.Collections.Generic;

namespace SQMReorderer.Gui.ViewModels
{
    public class GroupViewModel
    {
        public string Name { get; set; }

        public IEnumerable<VehicleViewModel> Units { get; set; } 
    }
}