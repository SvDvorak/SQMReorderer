using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    [TestFixture]
    class MissionViewModelTests
    {
        [Test]
        public void Expect_mission_with_empty_groups_to_have_no_groups()
        {
            var itemViewModels = new List<ItemViewModel>();

            var missionViewModel = new MissionViewModel(itemViewModels);

            Assert.AreEqual(0, missionViewModel.Groups.Count);
        }
    }
}
