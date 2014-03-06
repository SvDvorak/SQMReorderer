using System.Collections.Generic;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA2
{
    public interface IViewModelToContentReorderer
    {
        void Reorder(MissionState mission, List<TeamViewModel> teamViewModels);
    }
}