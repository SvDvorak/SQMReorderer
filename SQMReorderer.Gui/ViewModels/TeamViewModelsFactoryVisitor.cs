using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.ViewModels.ArmA2;

namespace SQMReorderer.Gui.ViewModels
{
    public class TeamViewModelsFactoryVisitor : ISqmContentsVisitor<List<ITeamViewModel>>
    {
        private readonly ITeamViewModelsFactory _arma2TeamViewModelsFactory;
        private readonly ArmA3.ITeamViewModelsFactory _arma3TeamViewModelsFactory;

        public TeamViewModelsFactoryVisitor(ITeamViewModelsFactory arma2TeamViewModelsFactory, ArmA3.ITeamViewModelsFactory arma3TeamViewModelsFactory)
        {
            _arma2TeamViewModelsFactory = arma2TeamViewModelsFactory;
            _arma3TeamViewModelsFactory = arma3TeamViewModelsFactory;
        }

        public List<ITeamViewModel> Visit(SqmContents arma2Contents)
        {
            return _arma2TeamViewModelsFactory.Create(arma2Contents.Mission.Groups).Cast<ITeamViewModel>().ToList();
        }

        public List<ITeamViewModel> Visit(Core.Import.ArmA3.ResultObjects.SqmContents arma3Contents)
        {
            return _arma3TeamViewModelsFactory.Create(arma3Contents.Mission.Groups).Cast<ITeamViewModel>().ToList();
        }
    }
}