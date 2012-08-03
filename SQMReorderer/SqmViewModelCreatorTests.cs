using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    [TestFixture]
    public class SqmViewModelCreatorTests
    {
        [Test]
        public void Expect_single_item_to_return_single_item_viewmodel()
        {
            var viewModelCreator = new SqmViewModelCreator();

            var item = new Item();
            item.Text = "Test text";

            var itemViewModel = viewModelCreator.CreateItemViewModel(item);
        }
    }
}
