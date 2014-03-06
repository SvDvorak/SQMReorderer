using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class ViewModelToContentReordererVisitorTests
    {
        private Gui.ViewModels.ArmA2.IViewModelToContentReorderer _arma2Reorderer;
        private Gui.ViewModels.ArmA3.IViewModelToContentReorderer _arma3Reorderer;

        [SetUp]
        public void Setup()
        {
            _arma2Reorderer = Substitute.For<Gui.ViewModels.ArmA2.IViewModelToContentReorderer>();
            _arma3Reorderer = Substitute.For<Gui.ViewModels.ArmA3.IViewModelToContentReorderer>();
        }

        [Test]
        public void Calls_arma_2_reorderer_when_visiting_with_arma_2_contents()
        {
            var teamViewModel = new Gui.ViewModels.ArmA2.TeamViewModel();
            var teamViewModels = new List<Gui.ViewModels.ArmA2.TeamViewModel>() { teamViewModel };

            var sut = new ViewModelToContentReordererVisitor(teamViewModels.Cast<ITeamViewModel>().ToList(), _arma2Reorderer, _arma3Reorderer);

            var arma2Contents = new SqmContents() { Mission = new MissionState() };
            sut.Visit(arma2Contents);

            _arma2Reorderer.Received().Reorder(
                arma2Contents.Mission,
                Arg.Is<List<Gui.ViewModels.ArmA2.TeamViewModel>>(x => teamViewModels[0] == teamViewModel));
        }

        [Test]
        public void Calls_arma_3_reorderer_when_visiting_with_arma_3_contents()
        {
            var teamViewModel = new Gui.ViewModels.ArmA3.TeamViewModel();
            var teamViewModels = new List<Gui.ViewModels.ArmA3.TeamViewModel>() { teamViewModel };

            var sut = new ViewModelToContentReordererVisitor(teamViewModels.Cast<ITeamViewModel>().ToList(), _arma2Reorderer, _arma3Reorderer);

            var arma3Contents = new SQMImportExport.Import.ArmA3.ResultObjects.SqmContents() { Mission = new SQMImportExport.Import.ArmA3.ResultObjects.MissionState() };
            sut.Visit(arma3Contents);

            _arma3Reorderer.Received().Reorder(
                arma3Contents.Mission,
                Arg.Is<List<Gui.ViewModels.ArmA3.TeamViewModel>>(x => teamViewModels[0] == teamViewModel));
        }
    }
}
