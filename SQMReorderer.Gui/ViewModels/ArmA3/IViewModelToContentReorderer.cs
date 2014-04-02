using System.Collections.Generic;
using SQMImportExport.Common;
using SQMImportExport.Import;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IViewModelToContentReorderer
    {
        void Reorder(MissionStateBase mission, List<ITeamViewModel> teamViewModels);
    }
}