using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class SqmViewModelCreator
    {
        public ItemViewModel CreateItemViewModel(Item item)
        {
            return new ItemViewModel(item);
        }
    }
}
