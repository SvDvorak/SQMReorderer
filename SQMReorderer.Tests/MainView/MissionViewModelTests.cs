using System.Collections.ObjectModel;
using NUnit.Framework;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    class MissionViewModelTests
    {
        [Test]
        public void Expect_mission_with_empty_groups_to_have_no_groups()
        {
            var itemViewModels = new ObservableCollection<VehicleViewModel>();

            var missionViewModel = new MissionViewModel();

            Assert.AreEqual(0, missionViewModel.Groups.Count);
        }
    }
}
