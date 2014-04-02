using System.Collections.ObjectModel;

namespace SQMReorderer.Gui.ViewModels
{
    public interface ITeamViewModel
    {
        ObservableCollection<IGroupViewModel> Groups { get; set; }
    }
}