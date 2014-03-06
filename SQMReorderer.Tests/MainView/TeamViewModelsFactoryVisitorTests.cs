﻿using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Import.ArmA2.ResultObjects;
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
            var exectedTeamViewModel = new Gui.ViewModels.ArmA2.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA2.ITeamViewModelsFactory>();
            teamViewModelsFactory.Create(missionGroups).Returns(new List<Gui.ViewModels.ArmA2.TeamViewModel>() { exectedTeamViewModel });

            var arma2Contents = new SqmContents()
                {
                    Mission = new MissionState()
                        {
                            Groups = missionGroups
                        }
                };

            var sut = new TeamViewModelsFactoryVisitor(teamViewModelsFactory, null);
            var teamViewModels = sut.Visit(arma2Contents);

            Assert.AreEqual(exectedTeamViewModel, teamViewModels[0]);
        }

        [Test]
        public void Visits_with_arma_3_contents()
        {
            var missionGroups = new List<SQMImportExport.Import.ArmA3.ResultObjects.Vehicle>();
            var exectedTeamViewModel = new Gui.ViewModels.ArmA3.TeamViewModel();
            var teamViewModelsFactory = Substitute.For<Gui.ViewModels.ArmA3.ITeamViewModelsFactory>();
            teamViewModelsFactory.Create(missionGroups).Returns(new List<Gui.ViewModels.ArmA3.TeamViewModel>() { exectedTeamViewModel });

            var arma3Contents = new SQMImportExport.Import.ArmA3.ResultObjects.SqmContents()
                {
                    Mission = new SQMImportExport.Import.ArmA3.ResultObjects.MissionState()
                        {
                            Groups = missionGroups
                        }
                };

            var sut = new TeamViewModelsFactoryVisitor(null, teamViewModelsFactory);
            var teamViewModels = sut.Visit(arma3Contents);

            Assert.AreEqual(exectedTeamViewModel, teamViewModels[0]);
        }
    }
}
