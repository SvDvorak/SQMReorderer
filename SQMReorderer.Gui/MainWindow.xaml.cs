using SQMReorderer.Gui.ViewModels;

namespace SQMReorderer.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            GroupsTreeView.SelectedItems.CollectionChanged +=
                (sender, args) => ViewModel.SelectedItems = GroupsTreeView.SelectedItems;
        }

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
