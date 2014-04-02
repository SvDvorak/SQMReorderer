using System.Collections.Generic;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IViewModelToContentReorderer
    {
        void Reorder(MissionState mission, List<ITeamViewModel> teamViewModels);
    }
}