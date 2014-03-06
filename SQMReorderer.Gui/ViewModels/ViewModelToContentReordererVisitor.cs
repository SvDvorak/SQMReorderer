using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class ViewModelToContentReordererVisitor : ISqmContentsVisitor
    {
        private readonly List<ITeamViewModel> _teamViewModels;
        private readonly ArmA2.IViewModelToContentReorderer _arma2Reorderer;
        private readonly ArmA3.IViewModelToContentReorderer _arma3Reorderer;

        public ViewModelToContentReordererVisitor(
            List<ITeamViewModel> teamViewModels,
            ArmA2.IViewModelToContentReorderer arma2Reorderer,
            ArmA3.IViewModelToContentReorderer arma3Reorderer)
        {
            _teamViewModels = teamViewModels;
            _arma2Reorderer = arma2Reorderer;
            _arma3Reorderer = arma3Reorderer;
        }

        public void Visit(SqmContents arma2Contents)
        {
            _arma2Reorderer.Reorder(arma2Contents.Mission, _teamViewModels.Cast<ArmA2.TeamViewModel>().ToList());
        }

        public void Visit(SQMImportExport.Import.ArmA3.ResultObjects.SqmContents arma3Contents)
        {
            _arma3Reorderer.Reorder(arma3Contents.Mission, _teamViewModels.Cast<ArmA3.TeamViewModel>().ToList());
        }
    }
}
