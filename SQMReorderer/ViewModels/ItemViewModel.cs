using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.ViewModels
{
    public class ItemViewModel
    {
        private readonly Item _item;

        public ItemViewModel(Item item)
        {
            _item = item;
        }

        public List<ItemViewModel> Items { get; set; }

        public bool IsSelected { get; set; }

        public string Side
        {
            get { return _item.Side; }
        }

        public string Vehicle
        {
            get { return _item.Vehicle; }
        }

        public string Rank
        {
            get { return _item.Rank; }
        }

        public string Text
        {
            get { return _item.Text; }
        }

        public string Description
        {
            get { return _item.Description; }
        }
    }
}
