using NUnit.Framework;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class SqmViewModelCreatorTests
    {
        private SqmViewModelCreator _viewModelCreator = new SqmViewModelCreator();

        [Test]
        public void Expect_single_item_to_return_single_viewmodel()
        {
            var mission = new MissionState();

            var item = new Vehicle();
            item.Text = "TEXT";

            mission.Groups.Add(item);

            var itemViewModel = _viewModelCreator.CreateMissionViewModel(mission);

            Assert.AreEqual("TEXT", itemViewModel.Groups[0].Text);
        }

        [Test]
        public void Expect_multiple_item_structure_to_return_corresponding_viewmodels()
        {
            var mission = new MissionState();

            var topItem = new Vehicle();
            var subItem1 = new Vehicle();
            var subItem2 = new Vehicle();
            var subSubItem = new Vehicle();

            subSubItem.Text = "TEXT";

            topItem.Vehicles.Add(subItem1);
            topItem.Vehicles.Add(subItem2);
            subItem2.Vehicles.Add(subSubItem);
            mission.Groups.Add(topItem);

            MissionViewModel missionViewModel = _viewModelCreator.CreateMissionViewModel(mission);

            Assert.AreEqual("TEXT", missionViewModel.Groups[0].Children[1].Children[0].Text);
        }

        [Test]
        public void Does_not_create_view_models_for_logic_groups()
        {
            var mission = new MissionState();

            var item = new Vehicle();
            item.Side = "LOGIC";

            mission.Groups.Add(item);

            var itemViewModel = _viewModelCreator.CreateMissionViewModel(mission);

            Assert.IsEmpty(itemViewModel.Groups);
        }
    }
}
