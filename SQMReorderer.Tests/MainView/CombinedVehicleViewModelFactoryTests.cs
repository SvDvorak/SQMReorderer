using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class CombinedVehicleViewModelFactoryTests
    {
        [Test]
        public void Creates_arma_2_combined_view_model_when_using_arma_2_vehicles()
        {
            var sut = new CombinedVehicleViewModelFactory();

            var combinedVehicleViewModel = sut.Create(new List<IVehicleViewModel>()
                {
                    new Gui.ViewModels.ArmA2.VehicleViewModel(null, new List<Gui.ViewModels.ArmA2.VehicleViewModel>())
                });

            Assert.IsInstanceOf<Gui.ViewModels.ArmA2.CombinedVehicleViewModel>(combinedVehicleViewModel);
        }

        [Test]
        public void Creates_arma_3_combined_view_model_when_using_arma_3_vehicles()
        {
            var sut = new CombinedVehicleViewModelFactory();

            var combinedVehicleViewModel = sut.Create(new List<IVehicleViewModel>()
                {
                    new Gui.ViewModels.ArmA3.VehicleViewModel(null, new List<Gui.ViewModels.ArmA3.VehicleViewModel>())
                });

            Assert.IsInstanceOf<Gui.ViewModels.ArmA3.CombinedVehicleViewModel>(combinedVehicleViewModel);
        }

        [Test]
        public void Returns_null_when_no_elements_are_in_list()
        {
            var sut = new CombinedVehicleViewModelFactory();

            var combinedVehicleViewModel = sut.Create(new List<IVehicleViewModel>());

            Assert.IsNull(combinedVehicleViewModel);
        }
    }
}
