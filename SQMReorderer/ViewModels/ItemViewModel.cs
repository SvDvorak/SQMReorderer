﻿using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<ItemViewModel> Items { get; set; }

        public static event Action<bool, ItemViewModel> SelectedItemChanged;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if(SelectedItemChanged != null)
                {
                    SelectedItemChanged(_isSelected, this);
                }
            }
        }

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
