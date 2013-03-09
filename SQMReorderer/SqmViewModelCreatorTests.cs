using NUnit.Framework;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
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

            Assert.AreEqual("TEXT", ((VehicleViewModel)itemViewModel.Groups[0]).Text);
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

            Assert.AreEqual("TEXT", ((VehicleViewModel)missionViewModel.Groups[0].Items[1].Items[0]).Text);
        }
    }
}
