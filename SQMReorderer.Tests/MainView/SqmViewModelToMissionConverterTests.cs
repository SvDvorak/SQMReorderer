using NUnit.Framework;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    [TestFixture]
    internal class SqmViewModelToMissionConverterTests
    {
        [Test]
        public void Returns_empty_mission_when_initial_mission_is_empty()
        {
            var sut = new SqmViewModelToMissionConverter();

            //var missionStatesut.Convert(new MissionState(), new MissionViewModel());

        }
    }
}