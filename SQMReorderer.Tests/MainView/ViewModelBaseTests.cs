using System;
using NUnit.Framework;
using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Tests.MainView
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        private class TestViewModel : ViewModelBase
        {
            private int _value;
            public int Value
            {
                get { return _value; }
                set { Set(value, () => Value, () => _value = value); }
            }

            public int PassingSetAMethod
            {
                set { Set(value, () => EmptyMethod(), () => {}); }
            }

            public int PassingSetAField
            {
                set { Set(value, () => _value, () => {});}
            }

            private object EmptyMethod()
            {
                throw new NotImplementedException();
            }

            public int PropertyChangedFiringProperty
            {
                get { return 0; }
                set { FirePropertyChanged(() => PropertyChangedFiringProperty); }
            }
        }

        [Test]
        public void Value_is_set_when_calling_set()
        {
            var viewModel = new TestViewModel();

            viewModel.Value = 42;

            Assert.AreEqual(42, viewModel.Value);
        }

        [Test]
        public void Property_changed_is_called_when_using_set()
        {
            var viewModel = new TestViewModel();
            var propertyChangedCalled = false;

            viewModel.PropertyChanged += (sender, args) => propertyChangedCalled = true;
            viewModel.Value = 42;

            Assert.IsTrue(propertyChangedCalled);
        }

        [Test]
        public void Property_changed_is_called_with_correct_property_name()
        {
            var viewModel = new TestViewModel();
            string propertyName = "";

            viewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;
            viewModel.Value = 42;

            Assert.AreEqual("Value", propertyName);
        }

        [Test]
        public void Exception_when_calling_set_with_a_method()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAMethod = 42);
        }

        [Test]
        public void Exception_when_calling_set_with_a_field()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAField = 42);
        }

        [Test]
        public void Property_changed_on_property_named_is_called_when_firing_property_changed()
        {
            var viewModel = new TestViewModel();
            bool wasPropertyChangedFired = false;
            viewModel.PropertyChanged += (sender, args) => wasPropertyChangedFired = true;

            viewModel.PropertyChangedFiringProperty = 1;

	        Assert.IsTrue(wasPropertyChangedFired);
        }

        [Test]
        public void Exception_when_calling_fire_property_changed_with_a_method()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAMethod = 42);
        }

        [Test]
        public void Exception_when_calling_fire_property_changed_with_a_field()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAField = 42);
        }
    }
}