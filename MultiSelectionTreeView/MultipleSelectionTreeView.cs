using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiSelectionTreeView
{
    public enum SelectionModalities
    {
        SingleSelectionOnly,
        MultipleSelectionOnly,
        KeyboardModifiersMode
    }

    public class SelectedItemsCollection : ObservableCollection<MultipleSelectionTreeViewItem> { }

    public class MultipleSelectionTreeView : ItemsControl
    {
        #region Properties
        private MultipleSelectionTreeViewItem _lastClickedItem = null;
        private SelectedItemsCollection _selectedItemsViewModels;

        public SelectionModalities SelectionMode
        {
            get { return (SelectionModalities)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionModalities), typeof(MultipleSelectionTreeView), new UIPropertyMetadata(SelectionModalities.SingleSelectionOnly));

        public SelectedItemsCollection SelectedItemsViewModels
        {
            get { return _selectedItemsViewModels; }
            set
            {
                _selectedItemsViewModels = value;
                UpdateSelectedItems();
            }
        }

        private static readonly DependencyPropertyKey SelectedItemsPropertyKey =
            DependencyProperty.RegisterReadOnly("SelectedItems", typeof(ObservableCollection<object>), typeof(MultipleSelectionTreeView), new PropertyMetadata(new ObservableCollection<object>()));

        public static readonly DependencyProperty SelectedItemsProperty = SelectedItemsPropertyKey.DependencyProperty;

        public ObservableCollection<object> SelectedItems
        {
            get { return (ObservableCollection<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
        #endregion

        #region Constructors
        static MultipleSelectionTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                    typeof(MultipleSelectionTreeView), new FrameworkPropertyMetadata(typeof(MultipleSelectionTreeView)));
        }

        public MultipleSelectionTreeView()
        {
            SelectedItemsViewModels = new SelectedItemsCollection();
            SelectedItemsViewModels.CollectionChanged += (sender, args) => UpdateSelectedItems();
        }
        #endregion

        private void UpdateSelectedItems()
        {
            SelectedItems.Clear();

            foreach (var selectedItemData in SelectedItemsViewModels.Select(x => x.DataContext))
            {
                SelectedItems.Add(selectedItemData);
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MultipleSelectionTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MultipleSelectionTreeViewItem;
        }

        internal void OnSelectionChanges(MultipleSelectionTreeViewItem viewItem)
        {
            MultipleSelectionTreeViewItem newItem = viewItem;
            if (newItem == null)
                return;

            bool isNewItemMultipleSelected = viewItem.IsSelected;

            if (isNewItemMultipleSelected)
                AddItemToSelection(viewItem);
            else
                RemoveItemFromSelection(viewItem);
        }

        internal void OnViewItemMouseDown(MultipleSelectionTreeViewItem viewItem)
        {
            MultipleSelectionTreeViewItem newItem = viewItem;
            if (newItem == null)
                return;
            
            switch (this.SelectionMode)
            {
                case SelectionModalities.MultipleSelectionOnly:
                    ManageCtrlSelection(newItem);
                    break;
                case SelectionModalities.SingleSelectionOnly:
                    ManageSingleSelection(newItem);
                    break;
                case SelectionModalities.KeyboardModifiersMode:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        // ... TODO ... right now we use the same behavior of Shit Keyword
                        ManageCtrlSelection(newItem);
                        //ManageShiftSelection(viewItem);
                    }
                    else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                    {
                        ManageCtrlSelection(newItem);
                    }
                    else
                    {
                        ManageSingleSelection(newItem);
                    }
                    break;
            }

            _lastClickedItem = viewItem.IsSelected ? viewItem : null;
        }

        public void OnItemDrop(MultipleSelectionTreeViewItem sourceItem, MultipleSelectionTreeViewItem targetItem)
        {
            foreach (var selectedItem in SelectedItemsViewModels)
            {
                var targetParentItems = (IList)targetItem.ItemsSource;
                targetParentItems.Add(selectedItem.DataContext);

                var sourceParentItems = (IList)selectedItem.ParentMultipleSelectionTreeViewItem.ItemsSource;
                sourceParentItems.Remove(selectedItem.DataContext);
            }

            SelectedItemsViewModels.Clear();
        }

        #region Methods
        public void UnselectAll()
        {
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item is MultipleSelectionTreeViewItem)
                    {
                        ((MultipleSelectionTreeViewItem)item).UnselectAllChildren();
                    }
                    else
                    {
                        MultipleSelectionTreeViewItem tvItem = this.ItemContainerGenerator.ContainerFromItem(item) as MultipleSelectionTreeViewItem;

                        if (tvItem != null)
                            tvItem.UnselectAllChildren();
                    }
                }
            }
        }

        public void SelectAllExpandedItems()
        {
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item is MultipleSelectionTreeViewItem)
                    {
                        ((MultipleSelectionTreeViewItem)item).SelectAllExpandedChildren();
                    }
                    else
                    {
                        MultipleSelectionTreeViewItem tvItem = this.ItemContainerGenerator.ContainerFromItem(item) as MultipleSelectionTreeViewItem;

                        if (tvItem != null)
                            tvItem.SelectAllExpandedChildren();
                    }
                }
            }
        }
        #endregion

        #region Helper Methods
        private void AddItemToSelection(MultipleSelectionTreeViewItem newItem)
        {
            if (!SelectedItemsViewModels.Contains(newItem))
                SelectedItemsViewModels.Add(newItem);
        }

        private void RemoveItemFromSelection(MultipleSelectionTreeViewItem newItem)
        {
            if (SelectedItemsViewModels.Contains(newItem))
                SelectedItemsViewModels.Remove(newItem);
        }

        private void ManageCtrlSelection(MultipleSelectionTreeViewItem viewItem)
        {
            viewItem.IsSelected = !viewItem.IsSelected;

            if (viewItem.IsSelected)
                AddItemToSelection(viewItem);
            else if (!viewItem.IsSelected)
                RemoveItemFromSelection(viewItem);
        }

        private void ManageSingleSelection(MultipleSelectionTreeViewItem viewItem)
        {
            UnselectAll();

            viewItem.IsSelected = true;
            AddItemToSelection(viewItem);
        }

        /// <summary>
        /// ... TODO ...
        /// </summary>
        /// <param name="viewItem"></param>
        private void ManageShiftSelection(MultipleSelectionTreeViewItem viewItem)
        {
            bool isViewItemMultipleSelected = viewItem.IsSelected;

            if (_lastClickedItem != null)
            {
                // BEGIN TODO
                IsItem1ListedBeforeItem2(_lastClickedItem, viewItem);
                // END TODO
            }

            if (isViewItemMultipleSelected)
            {
                viewItem.SelectAllExpandedChildren();
                //viewItem.IsExpanded = true; // TO BE CLARIFY: this expand only item children, does not expand children of children
                AddItemToSelection(viewItem);
            }
            else
            {
                viewItem.UnselectAllChildren();
            }          
        }

        /// <summary>
        /// ... TODO ...
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        private bool IsItem1ListedBeforeItem2(MultipleSelectionTreeViewItem item1,
                                              MultipleSelectionTreeViewItem item2)
        {
            /*
            // Perform a Backword search (up)
            if (item1.ParentMultipleSelectionTreeViewItem != null) // item1 has a brother!
            {
                ItemCollection brothers = item1.ParentMultipleSelectionTreeViewItem.Items;
                int indexOfItem1 = brothers.IndexOf(item1);
                int indexOfItem2 = brothers.IndexOf(item2);
                if (indexOfItem2 >= 0) //item1 and item2 are brothers
                {
                    return indexOfItem1 < indexOfItem2 ? true : false;
                }

                
            }
            */
            
            return true;
        }

        /// <summary>
        /// ... TODO ...
        /// </summary>
        /// <param name="fromItem"></param>
        /// <param name="toItem"></param>
        private void SelectRange(MultipleSelectionTreeViewItem fromItem, 
                                 MultipleSelectionTreeViewItem toItem)
        {
        }
        #endregion
    }
}
