using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Gui.ViewModels;
using ArmA2Objects = SQMImportExport.Import.ArmA2.ResultObjects;
using ArmA3Objects = SQMImportExport.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class TeamViewModelsFactoryVisitorTests
    {
        [Test]
        public void Visits_with_arma_2_contents()
        {
            var missionGroups = new List<ArmA2Objects.Vehicle>();
            var expectedTeamViewModel = new Gui.ViewModels.ArmA2.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA2.ITeamViewModelsFactory>();
            teamViewModelsFactory
                .Create(Arg.Is<List<ArmA2Objects.Vehicle>>(x => x.SequenceEqual(missionGroups)))
                .Returns(new List<Gui.ViewModels.ArmA2.TeamViewModel>() { expectedTeamViewModel });

            var arma2Contents = new ArmA2Objects.SqmContents()
                {
                    Mission = new ArmA2Objects.MissionState()
                        {
                            Groups = missionGroups
                        }
                };

            var sut = new TeamViewModelsFactoryVisitor(teamViewModelsFactory, null);
            var teamViewModels = sut.Visit(arma2Contents);

            Assert.AreEqual(expectedTeamViewModel, teamViewModels[0]);
        }

        [Test]
        public void Visits_with_arma_3_contents()
        {
            var missionGroups = new List<ArmA3Objects.Vehicle>();
            var expectedTeamViewModel = new Gui.ViewModels.ArmA3.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA3.ITeamViewModelsFactory>();
            teamViewModelsFactory
                .Create(Arg.Is<List<ArmA3Objects.Vehicle>>(x => x.SequenceEqual(missionGroups)))
                .Returns(new List<Gui.ViewModels.ArmA3.TeamViewModel>() { expectedTeamViewModel });

            var arma3Contents = new ArmA3Objects.SqmContents()
                {
                    Mission = new ArmA3Objects.MissionState()
                        {
                            Groups = missionGroups
                        }
                };

            var sut = new TeamViewModelsFactoryVisitor(null, teamViewModelsFactory);
            var teamViewModels = sut.Visit(arma3Contents);

            Assert.AreEqual(expectedTeamViewModel, teamViewModels[0]);
        }
    }
}
