using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                    //var treeView = sender as TreeView;
                    //var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    //if (treeView == null || treeViewItem == null)
                    //    return;

                    //var folderViewModel = treeView.SelectedItem as FolderViewModel;
                    //if (folderViewModel == null)
                    //    return;

                    //var dragData = new DataObject(folderViewModel);
                    //DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }
        }
    }
}
