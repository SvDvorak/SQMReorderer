using System.Collections.Generic;

namespace SQMReorderer.Gui.ViewModels
{
    public class TeamViewModel
    {
        public string Side { get; set; }

        public IEnumerable<GroupViewModel> Groups { get; set; }
    }
}