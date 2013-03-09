using System.Collections.ObjectModel;
using System.Windows.Input;
using SQMReorderer.Command;

namespace SQMReorderer.ViewModels
{
    public abstract class StructureItemViewModelBase : ViewModelBase
    {
        public ObservableCollection<StructureItemViewModelBase> Items { get; set; }
        public ICommand ExpandChildrenCommand { get; private set; }
        public ICommand SelectCommand { get; private set; }

        public StructureItemViewModelBase()
        {
            ShowChildItems = false;

            ExpandChildrenCommand = new DelegateCommand(() => ShowChildItems = !ShowChildItems);
            SelectCommand = new DelegateCommand(() => IsSelected = !IsSelected);
        }

        private bool _showChildItems;
        public bool ShowChildItems
        {
            get { return _showChildItems; }
            set { Set(value, () => ShowChildItems, () => _showChildItems = value); }
        }

        public bool HasChildItems
        {
            get { return Items.Count > 0; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(value, () => IsSelected, () => _isSelected = value); }
        }

        public abstract override string ToString();
    }
}