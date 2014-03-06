using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Import.ArmA2.ResultObjects;
using SQMReorderer.Gui.Dialogs;
using SQMReorderer.Gui.Dialogs.AddInit;
using SQMReorderer.Gui.ViewModels.ArmA2;

namespace SQMReorderer.Tests.MainView.ArmA2
{
    [TestFixture]
    public class CombinedVehicleViewModelTests
    {
        [TestFixture]
        public class ViewModelProperties : CombinedVehicleViewModelTests
        {
            [Test]
            public void Null_is_returned_for_properties_when_no_vehicles_are_available()
            {
                var sut = new CombinedVehicleViewModel(new List<VehicleViewModel>(), null);

                Assert.AreEqual(null, sut.VehicleName);
                Assert.AreEqual(null, sut.Rank);
                Assert.AreEqual(null, sut.Text);
                Assert.AreEqual(null, sut.Description);
                Assert.AreEqual(null, sut.Init);
            }

            [Test]
            public void Null_is_returned_when_not_all_vehicles_have_same_property_value()
            {
                var vehicle1 = new Vehicle()
                {
                    VehicleName = "name1",
                    Rank = "rank1",
                    Text = "text1",
                    Description = "description1",
                    Init = "init1"
                };

                var vehicle2 = new Vehicle()
                {
                    VehicleName = "name2",
                    Rank = "rank2",
                    Text = "text2",
                    Description = "description2",
                    Init = "init2"
                };

                var vehicles = CreateViewModelList(vehicle1, vehicle2);
                var sut = new CombinedVehicleViewModel(vehicles, null);

                Assert.AreEqual(null, sut.VehicleName);
                Assert.AreEqual(null, sut.Rank);
                Assert.AreEqual(null, sut.Text);
                Assert.AreEqual(null, sut.Description);
                Assert.AreEqual(null, sut.Init);
            }

            [Test]
            public void Value_is_returned_when_all_vehicles_have_same_property_value()
            {
                var vehicle1 = new Vehicle()
                {
                    VehicleName = "name",
                    Rank = "rank",
                    Text = "text",
                    Description = "description",
                    Init = "init"
                };

                var vehicle2 = new Vehicle()
                {
                    VehicleName = "name",
                    Rank = "rank",
                    Text = "text",
                    Description = "description",
                    Init = "init"
                };

                var vehicles = CreateViewModelList(vehicle1, vehicle2);
                var sut = new CombinedVehicleViewModel(vehicles, null);

                Assert.AreEqual(vehicle1.VehicleName, sut.VehicleName);
                Assert.AreEqual(vehicle1.Rank, sut.Rank);
                Assert.AreEqual(vehicle1.Text, sut.Text);
                Assert.AreEqual(vehicle1.Description, sut.Description);
                Assert.AreEqual(vehicle1.Init, sut.Init);
            }

            [Test]
            public void Setting_value_sets_on_all_vehicles()
            {
                var vehicle1 = new Vehicle();
                var vehicle2 = new Vehicle();

                var vehicles = CreateViewModelList(vehicle1, vehicle2);
                var sut = new CombinedVehicleViewModel(vehicles, null);

                sut.VehicleName = "name";
                sut.Rank = "rank";
                sut.Text = "text";
                sut.Description = "description";
                sut.Init = "init";

                Assert.AreEqual("name", vehicle1.VehicleName);
                Assert.AreEqual("name", vehicle2.VehicleName);

                Assert.AreEqual("rank", vehicle1.Rank);
                Assert.AreEqual("rank", vehicle2.Rank);

                Assert.AreEqual("text", vehicle1.Text);
                Assert.AreEqual("text", vehicle2.Text);

                Assert.AreEqual("description", vehicle1.Description);
                Assert.AreEqual("description", vehicle2.Description);

                Assert.AreEqual("init", vehicle1.Init);
                Assert.AreEqual("init", vehicle2.Init);
            }
        }

        [TestFixture]
        public class AddInitContext : CombinedVehicleViewModelTests
        {
            private IAddInitDialogFactory _addInitDialogFactory;
            private IAddInitDialog _addInitDialog;

            [SetUp]
            public void Setup()
            {
                _addInitDialogFactory = Substitute.For<IAddInitDialogFactory>();
                _addInitDialog = Substitute.For<IAddInitDialog>();
                _addInitDialogFactory.Create().Returns(_addInitDialog);
            }

            [Test]
            public void Adds_text_to_init_when_add_init_is_called()
            {
                var sut = new CombinedVehicleViewModel(CreateViewModelList(new Vehicle()), _addInitDialogFactory);

                var addInitResult = new AddInitResult() { DialogResult = DialogResult.Ok, InitToAdd = "added stuff" };
                _addInitDialog.ShowDialog().Returns(addInitResult);
                sut.AddInitCommand.Execute();

                Assert.AreEqual("added stuff", sut.Init);
            }

            [Test]
            public void Does_not_do_anything_when_add_init_is_cancelled()
            {
                var sut = new CombinedVehicleViewModel(CreateViewModelList(new Vehicle() { Init = "old text" }), _addInitDialogFactory);

                var addInitResult = new AddInitResult() { DialogResult = DialogResult.Cancel };
                _addInitDialog.ShowDialog().Returns(addInitResult);
                sut.AddInitCommand.Execute();

                Assert.AreEqual("old text", sut.Init);
            }

            [Test]
            public void Calls_property_changed_when_init_is_added()
            {
                var sut = new CombinedVehicleViewModel(CreateViewModelList(), _addInitDialogFactory);

                var newText = "";
                sut.PropertyChanged += (sender, args) => { newText = args.PropertyName == "Init" ? "new text" : ""; };

                var addInitResult = new AddInitResult() { InitToAdd = "new text", DialogResult = DialogResult.Ok };
                _addInitDialog.ShowDialog().Returns(addInitResult);

                sut.AddInitCommand.Execute();

                Assert.AreEqual("new text", newText);
            }
        }

        private List<VehicleViewModel> CreateViewModelList(params Vehicle[] vehicles)
        {
            return vehicles.Select(x => new VehicleViewModel(x, new List<VehicleViewModel>())).ToList();
        }
    }
}
