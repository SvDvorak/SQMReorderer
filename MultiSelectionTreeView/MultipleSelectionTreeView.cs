using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            DependencyPropertyDescriptor
                .FromProperty(ItemsSourceProperty, typeof(ItemsControl))
                .AddValueChanged(this, (s, e) => ClearSelectedItems());
        }

        private void ClearSelectedItems()
        {
            SelectedItems.Clear();
            SelectedItemsViewModels.Clear();
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

            if (viewItem.IsSelectable && viewItem.IsSelected)
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
                    SetLastClicked(viewItem);
                    break;
                case SelectionModalities.KeyboardModifiersMode:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        ManageShiftSelection(viewItem);
                    }
                    else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                    {
                        ManageCtrlSelection(newItem);
                    }
                    else
                    {
                        ManageSingleSelection(newItem);
                        SetLastClicked(newItem);
                    }
                    break;
            }
        }

        private void SetLastClicked(MultipleSelectionTreeViewItem viewItem)
        {
            _lastClickedItem = viewItem.IsSelected ? viewItem : null;
        }

        public void OnItemDrop(MultipleSelectionTreeViewItem sourceItem, MultipleSelectionTreeViewItem targetItem)
        {
            if (sourceItem.GetDepth() == targetItem.GetDepth())
            {
                foreach (var selectedItem in SelectedItemsViewModels)
                {
                    var dataContext = selectedItem.DataContext;
                    var sourceParentItems = (IList)selectedItem.ParentMultipleSelectionTreeViewItem.ItemsSource;
                    var targetParentItems = (IList)targetItem.ParentMultipleSelectionTreeViewItem.ItemsSource;

                    sourceParentItems.Remove(dataContext);
                    targetParentItems.Insert(targetItem.GetIndexInParent(), dataContext);
                }
            }
            else
            {
                foreach (var selectedItem in SelectedItemsViewModels)
                {
                    var dataContext = selectedItem.DataContext;
                    var targetParentItems = (IList)targetItem.ItemsSource;
                    var sourceParentItems = (IList)selectedItem.ParentMultipleSelectionTreeViewItem.ItemsSource;

                    sourceParentItems.Remove(dataContext);
                    targetParentItems.Insert(0, dataContext);
                }
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
            if(viewItem.IsSelectable)
            {
                viewItem.IsSelected = !viewItem.IsSelected;

                if (viewItem.IsSelected)
                    AddItemToSelection(viewItem);
                else if (!viewItem.IsSelected)
                    RemoveItemFromSelection(viewItem);
                
            }
        }

        private void ManageSingleSelection(MultipleSelectionTreeViewItem viewItem)
        {
            if(viewItem.IsSelectable)
            {
                UnselectAll();

                viewItem.IsSelected = true;
                AddItemToSelection(viewItem);
            }
        }

        private void ManageShiftSelection(MultipleSelectionTreeViewItem viewItem)
        {
            if (_lastClickedItem == null)
            {
                return;
            }

            if (viewItem.IsSelectable)
            {
                UnselectAll();


                ItemsControl parentControl = viewItem.ParentItemsControl;
                if (parentControl == _lastClickedItem.ParentItemsControl)
                {
                    var startRangeIndex = parentControl.ItemContainerGenerator.IndexFromContainer(_lastClickedItem);
                    var endRangeIndex = parentControl.ItemContainerGenerator.IndexFromContainer(viewItem);
                    //viewItem.ParentMultipleSelectionTreeViewItem.SelectAllExpandedChildren();

                    var direction = (endRangeIndex - startRangeIndex).Clamp(-1, 1);
                    for (int i = 0; i <= Math.Abs(endRangeIndex - startRangeIndex); i++)
                    {
                        var nextItemIndex = startRangeIndex + i * direction;
                        var nextItemInRange = parentControl.ItemContainerGenerator.ContainerFromIndex(nextItemIndex) as MultipleSelectionTreeViewItem;
                        nextItemInRange.IsSelected = true;
                        AddItemToSelection(nextItemInRange);
                    }
                }
                //var itemsOnSameLevel = viewItem.ParentMultipleSelectionTreeViewItem.Items;

                //if (!itemsOnSameLevel.Contains(_lastClickedItem))
                //{
                //    return;
                //}

                //var startRangeIndex = itemsOnSameLevel.IndexOf(_lastClickedItem);
                //var endRangeIndex = itemsOnSameLevel.IndexOf(viewItem);

                
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
