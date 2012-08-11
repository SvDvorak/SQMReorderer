﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Point _startPoint;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
        }

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void Tree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        private void Tree_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePos = e.GetPosition(null);
                var diff = _startPoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var treeView = sender as TreeView;
                    //var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    //if (treeView == null || treeViewItem == null)
                    //    return;

                    //var folderViewModel = treeView.SelectedItem as FolderViewModel;
                    //if (folderViewModel == null)
                    //    return;

                    var dragData = new DataObject(treeView.SelectedItem);
                    DragDrop.DoDragDrop(treeView, dragData, DragDropEffects.Move);
                }
            }
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetType() == typeof(ItemViewModel))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void TreeView_Drop(object sender, DragEventArgs e)
        {
            // Trololol
            var itemViewModels = ViewModel.Mission.Groups;

            var originalSource = (TextBlock) e.OriginalSource;
            var currentHoverIndex = itemViewModels.IndexOf((ItemViewModel)originalSource.DataContext);

            var movedItem = (ItemViewModel) e.Data.GetData(typeof (ItemViewModel));
            itemViewModels.Remove(movedItem);
            itemViewModels.Insert(currentHoverIndex, movedItem);
        }
    }
}
