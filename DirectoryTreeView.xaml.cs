using System;
using System.ComponentModel;
using System.Windows.Controls;
using WpfDirectoryTreeView.DataAccess;

namespace WpfDirectoryTreeView
{
    /// <summary>
    /// Interaction logic for DirectoryTreeViewControl.xaml
    /// </summary>
    public partial class DirectoryTreeViewControl : UserControl
    {
        // Create list for directories and files.
        private ItemProvider _itemProvider;

        /// <summary>
        /// Initialise gui and item provider, then attaches method to reload tree view.
        /// </summary>
        public DirectoryTreeViewControl()
        {
            InitializeComponent();
            _itemProvider = new ItemProvider();
            _itemProvider.PropertyChanged += populateTreeView;
        }

        /// <summary>
        /// Load and display directory and file list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void populateTreeView(object sender, PropertyChangedEventArgs e)
        {
            var items = _itemProvider.GetItems(e.PropertyName);
            DataContext = items;
        }
    }
}
