using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.ArmA2;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class TeamViewModelsFactoryVisitorTests
    {
        [Test]
        public void Visits_with_arma_2_contents()
        {
            var missionGroups = new List<Vehicle>();
            var expectedTeamViewModel = new Gui.ViewModels.ArmA2.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA2.ITeamViewModelsFactory>();
            teamViewModelsFactory
                .Create(Arg.Is<List<Vehicle>>(x => x.SequenceEqual(missionGroups)))
                .Returns(new List<Gui.ViewModels.ArmA2.TeamViewModel>() { expectedTeamViewModel });

            var arma2Contents = new SqmContents()
                {
                    Mission = new MissionState()
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
            var missionGroups = new List<SQMImportExport.ArmA3.Vehicle>();
            var expectedTeamViewModel = new Gui.ViewModels.ArmA3.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA3.ITeamViewModelsFactory>();
            teamViewModelsFactory
                .Create(Arg.Is<List<SQMImportExport.ArmA3.Vehicle>>(x => x.SequenceEqual(missionGroups)))
                .Returns(new List<Gui.ViewModels.ArmA3.TeamViewModel>() { expectedTeamViewModel });

            var arma3Contents = new SQMImportExport.ArmA3.SqmContents()
                {
                    Mission = new SQMImportExport.ArmA3.MissionState()
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
