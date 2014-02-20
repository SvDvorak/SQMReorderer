using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace MultiSelectionTreeView
{
    public class MultipleSelectionTreeViewItem : TreeViewItem
    {
        #region Properties
        private Point _leftClickPosition;

        /// <summary>
        /// Get the UI Parent Control of this node.
        /// </summary>
        public ItemsControl ParentItemsControl
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this);
            }
        }

        /// <summary>
        /// Get the MultipleSelectionTreeView in which this node is hosted in.
        /// Null value means that this node is not hosted into a MultipleSelectionTreeView control.
        /// </summary>
        public MultipleSelectionTreeView ParentMultipleSelectionTreeView
        {
            get
            {
                for (ItemsControl container = this.ParentItemsControl; container != null; container = ItemsControl.ItemsControlFromItemContainer(container))
                {
                    MultipleSelectionTreeView view = container as MultipleSelectionTreeView;
                    if (view != null)
                    {
                        return view;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Get the Parent MultipleSelectionTreeViewItem of this node.
        /// Remark: Null value means that this node is hosted into a control (e.g. MultipleSelectionTreeView).
        /// </summary>
        public MultipleSelectionTreeViewItem ParentMultipleSelectionTreeViewItem
        {
            get
            {
                return (this.ParentItemsControl as MultipleSelectionTreeViewItem);
            }
        }

        private static readonly DependencyProperty IsSelectableProperty =
            DependencyProperty.Register("IsSelectable", typeof(bool), typeof(MultipleSelectionTreeViewItem), new PropertyMetadata(true));

        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }

        private static readonly DependencyProperty CanTakeDropProperty =
            DependencyProperty.Register("CanTakeDrop", typeof(bool), typeof(MultipleSelectionTreeViewItem), new PropertyMetadata(true));

        public bool CanTakeDrop
        {
            get { return (bool)GetValue(CanTakeDropProperty); }
            set { SetValue(CanTakeDropProperty, value); }
        }

        private static readonly DependencyProperty TakesDroppedTypesProperty =
            DependencyProperty.Register("TakesDroppedTypes", typeof(List<Type>), typeof(MultipleSelectionTreeViewItem), new PropertyMetadata(new List<Type>()));

        public List<Type> TakesDroppedTypes
        {
            get { return (List<Type>)GetValue(TakesDroppedTypesProperty); }
            set { SetValue(TakesDroppedTypesProperty, value); }
        }
        #endregion

        #region Constructors
        static MultipleSelectionTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                    typeof(MultipleSelectionTreeViewItem), new FrameworkPropertyMetadata(typeof(MultipleSelectionTreeViewItem)));
        }

        public MultipleSelectionTreeViewItem()
        {
            ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;

            AllowDrop = true;
            IsExpanded = true;
            DragOver += OnDragOver;
            Drop += OnDrop;
        }

        void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        private void OnDragOver(object sender, DragEventArgs dragEventArgs)
        {
            dragEventArgs.Effects = DragDropEffects.None;

            var dragItem = dragEventArgs.Data.GetData(typeof(MultipleSelectionTreeViewItem)) as MultipleSelectionTreeViewItem;

            if (dragItem != null && CanTakeDrop && TakesDroppedTypes.Exists(x => x == dragItem.DataContext.GetType()))
            {
                dragEventArgs.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            }

            dragEventArgs.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs dragEventArgs)
        {
            ParentMultipleSelectionTreeView.OnItemDrop((MultipleSelectionTreeViewItem) dragEventArgs.Data.GetData(typeof(MultipleSelectionTreeViewItem)), this);

            dragEventArgs.Handled = true;
        }

        #region Overrides
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MultipleSelectionTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MultipleSelectionTreeViewItem;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (ParentMultipleSelectionTreeView == null)
                return;

            if (e.LeftButton == MouseButtonState.Released &&
                e.RightButton == MouseButtonState.Pressed)
                return;

            _leftClickPosition = e.GetPosition(null);

            ParentMultipleSelectionTreeView.OnViewItemMouseDown(this);

            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var isNotGroupNode = ParentMultipleSelectionTreeViewItem != null;

            if(isNotGroupNode && IsSelected && e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePosition = e.GetPosition(null);
                var diff = _leftClickPosition - mousePosition;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var dragData = new DataObject(this);
                    DragDrop.DoDragDrop(ParentMultipleSelectionTreeView, dragData, DragDropEffects.Move);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                MultipleSelectionTreeViewItem itemToSelect = null;

                if (e.Key == Key.Left)
                {
                    IsExpanded = false;
                }
                else if (e.Key == Key.Right)
                {
                    IsExpanded = true;
                }
                else if (e.Key == Key.Up)
                {
                    // In this case we need to select the last child of the last expandend node of
                    // - the previous at the same level (if this index node is NOT 0)
                    // - the parent node (if this index node is 0)

                    int currentNodeIndex = ParentItemsControl.ItemContainerGenerator.IndexFromContainer(this);

                    if (currentNodeIndex == 0)
                    {
                        itemToSelect = ParentMultipleSelectionTreeViewItem;
                    }
                    else
                    {
                        var previousNodeAtSameLevel = GetPreviousNodeAtSameLevel(this);
                        itemToSelect = GetLastVisibleChildNodeOf(previousNodeAtSameLevel);
                    }
                }
                else if (e.Key == Key.Down)
                {
                    // In this case we need to select:
                    // - the first child node (if this node is expanded)
                    // - the next at the same level (if this not the last child)
                    // - the next at the same level of the parent node (if this is the last child)

                    if (IsExpanded && Items.Count > 0)
                    { // Select first Child
                        itemToSelect = ItemContainerGenerator.ContainerFromIndex(0) as MultipleSelectionTreeViewItem;
                    }
                    else
                    {
                        itemToSelect = GetNextNodeAtSameLevel(this);

                        if (itemToSelect == null) // current node has no subsequent node at the same level
                        {
                            MultipleSelectionTreeViewItem tmp = ParentMultipleSelectionTreeViewItem;

                            while (itemToSelect == null && tmp != null) // searhing for the first parent that has a subsequent node at the same level
                            {
                                itemToSelect = GetNextNodeAtSameLevel(tmp);
                                tmp = tmp.ParentMultipleSelectionTreeViewItem;
                            }

                        }
                    }
                }

                if (itemToSelect != null)
                {
                    itemToSelect.Focus();
                    itemToSelect.IsSelected = true;
                    ParentMultipleSelectionTreeView.OnViewItemMouseDown(itemToSelect);
                }
            }
            catch (Exception) { /* Silently ignore */ }

            // Don't bubble up the event. (I'm sure you know difference between Bubble and Tunnel events in WPF...)
            e.Handled = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retrieve the last displayed child node of the given one.
        /// </summary>
        /// <param name="item">The node starting with you want to retrieve the last visible node.</param>
        /// <returns>The last child node that is displayed, or the node itself in case it is not expanded.</returns>
        public MultipleSelectionTreeViewItem GetLastVisibleChildNodeOf(MultipleSelectionTreeViewItem item)
        {
            MultipleSelectionTreeViewItem lastVisibleNode = item;

            // Retrieving last child of last expanded node
            while (lastVisibleNode != null && lastVisibleNode.Items.Count > 0 && lastVisibleNode.IsExpanded)
                lastVisibleNode = lastVisibleNode.ItemContainerGenerator.ContainerFromIndex(lastVisibleNode.Items.Count - 1) as MultipleSelectionTreeViewItem;

            return lastVisibleNode;
        }

        /// <summary>
        /// Retrieve the previous node that is at the same level.
        /// </summary>
        /// <param name="item">The node starting with you want to retrieve the previous one.</param>
        /// <returns>Null if there is no previous node at the same level.</returns>
        public MultipleSelectionTreeViewItem GetPreviousNodeAtSameLevel(MultipleSelectionTreeViewItem item)
        {
            if (item == null)
                return null;

            MultipleSelectionTreeViewItem previousNodeAtSameLevel = null;

            ItemsControl parentControl = item.ParentItemsControl;
            if (parentControl != null)
            {
                int index = parentControl.ItemContainerGenerator.IndexFromContainer(item);
                if (index != 0) // if this is not the last item
                {
                    previousNodeAtSameLevel = parentControl.ItemContainerGenerator.ContainerFromIndex(index - 1) as MultipleSelectionTreeViewItem;
                }
            }

            return previousNodeAtSameLevel;
        }

        /// <summary>
        /// Retrieve the subsequent node that is at the same level.
        /// </summary>
        /// <param name="item">The node starting with you want to retrieve the subsequent one.</param>
        /// <returns>Null if there is no subsequent node at the same level.</returns>
        public MultipleSelectionTreeViewItem GetNextNodeAtSameLevel(MultipleSelectionTreeViewItem item)
        {
            if (item == null)
                return null;

            MultipleSelectionTreeViewItem nextNodeAtSameLevel = null;

            ItemsControl parentControl = item.ParentItemsControl;
            if (parentControl != null)
            {
                int index = parentControl.ItemContainerGenerator.IndexFromContainer(item);
                if (index != parentControl.Items.Count - 1) // if this is not the last item
                {
                    nextNodeAtSameLevel = parentControl.ItemContainerGenerator.ContainerFromIndex(index + 1) as MultipleSelectionTreeViewItem;
                }
            }

            return nextNodeAtSameLevel;
        }

        /// <summary>
        /// Recursively un-select all children.
        /// </summary>
        public void UnselectAllChildren()
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

            if (this.IsSelected)
            {
                this.IsSelected = false;
                ParentMultipleSelectionTreeView.OnSelectionChanges(this);
            }
        }

        /// <summary>
        /// Recursively select all children.
        /// </summary>
        public void SelectAllExpandedChildren()
        {
            if (Items != null && Items.Count > 0)
            {
                if (this.IsExpanded)
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

            if (!this.IsSelected)
            {
                this.IsSelected = true;
                ParentMultipleSelectionTreeView.OnSelectionChanges(this);
            }
        }

        /// <summary>
        /// Get the node depth.
        /// </summary>
        /// <returns>The int indicating the node depth.</returns>
        public int GetDepth()
        {
            MultipleSelectionTreeViewItem parent = this;

            while (parent.ParentMultipleSelectionTreeViewItem != null)
            {
                return parent.ParentMultipleSelectionTreeViewItem.GetDepth() + 1;
            }

            return 0;
        }
        #endregion

    }
}
