using System.Collections.Generic;
using SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Gui.ViewModels.ArmA3
{
    public interface IViewModelToContentReorderer
    {
        void Reorder(MissionState mission, List<TeamViewModel> teamViewModels);
    }
}