using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    [TestFixture]
    public class VehicleViewModelTests
    {
        [Test]
        public void Expect_working_item_viewmodel_value_properties()
        {
            var vehicle = new Vehicle();

            vehicle.Side = "SIDE";
            vehicle.VehicleName = "VEHICLE";
            vehicle.Rank = "RANK";
            vehicle.Text = "TEXT";
            vehicle.Description = "DESC";

            var itemViewModel = new VehicleViewModel(vehicle);

            Assert.AreEqual("SIDE", itemViewModel.Side);
            Assert.AreEqual("VEHICLE", itemViewModel.Vehicle);
            Assert.AreEqual("RANK", itemViewModel.Rank);
            Assert.AreEqual("TEXT", itemViewModel.Text);
            Assert.AreEqual("DESC", itemViewModel.Description);
        }
    }
}
