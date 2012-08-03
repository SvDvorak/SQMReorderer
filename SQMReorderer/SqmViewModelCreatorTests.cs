using NUnit.Framework;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    [TestFixture]
    public class SqmViewModelCreatorTests
    {
        private SqmViewModelCreator _viewModelCreator = new SqmViewModelCreator();

        [Test]
        public void Expect_single_item_to_return_single_viewmodel()
        {
            var mission = new Mission();

            var item = new Item();
            item.Text = "TEXT";

            mission.Groups.Add(item);

            var itemViewModel = _viewModelCreator.CreateMissionViewModel(mission);

            Assert.AreEqual("TEXT", itemViewModel.Groups[0].Text);
        }

        [Test]
        public void Expect_multiple_item_structure_to_return_corresponding_viewmodels()
        {
            var mission = new Mission();

            var topItem = new Item();
            var subItem1 = new Item();
            var subItem2 = new Item();
            var subSubItem = new Item();

            subSubItem.Text = "TEXT";

            topItem.Items.Add(subItem1);
            topItem.Items.Add(subItem2);
            subItem2.Items.Add(subSubItem);
            mission.Groups.Add(topItem);

            MissionViewModel missionViewModel = _viewModelCreator.CreateMissionViewModel(mission);

            Assert.AreEqual("TEXT", missionViewModel.Groups[0].Items[1].Items[0].Text);
        }
    }
}
