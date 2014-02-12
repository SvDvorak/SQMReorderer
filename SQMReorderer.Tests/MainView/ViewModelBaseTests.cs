using System;
using NUnit.Framework;

namespace SQMReorderer.ViewModels
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
        }

        [Test]
        public void ValueIsSetWhenCallingSet()
        {
            var viewModel = new TestViewModel();

            viewModel.Value = 42;

            Assert.AreEqual(42, viewModel.Value);
        }

        [Test]
        public void PropertyChangedIsCalledWhenUsingSet()
        {
            var viewModel = new TestViewModel();
            var propertyChangedCalled = false;

            viewModel.PropertyChanged += (sender, args) => propertyChangedCalled = true;
            viewModel.Value = 42;

            Assert.IsTrue(propertyChangedCalled);
        }

        [Test]
        public void PropertyChangedIsCalledWithCorrectPropertyName()
        {
            var viewModel = new TestViewModel();
            string propertyName = "";

            viewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;
            viewModel.Value = 42;

            Assert.AreEqual("Value", propertyName);
        }

        [Test]
        public void ExceptionWhenCallingSetWithAMethod()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAMethod = 42);
        }

        [Test]
        public void ExceptionWhenCallingSetWithAField()
        {
            var viewModel = new TestViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.PassingSetAField = 42);
        }
    }
}