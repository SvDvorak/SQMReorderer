using NUnit.Framework;

namespace SQMReorderer.Command
{
    [TestFixture]
    public class DelegateCommandTests
    {
        [Test]
        public void ActionExecutedWhenExecutingCommand()
        {
            var valueSet = false;

            var delegateCommand = new DelegateCommand(() => valueSet = true);
            delegateCommand.Execute();

            Assert.AreEqual(true, valueSet, "DelegateCommand does not fire supplied action.");
        }

        [Test]
        public void ExceptionWhenSupplyingNullAsAction()
        {
            Assert.Throws<ActionCanNotBeNullException>(() => new DelegateCommand(null));
        }
    }
}