using System.Windows;
using System.Windows.Controls;
using WpfDirectoryTreeView.DataAccess;

namespace WpfDirectoryTreeView
{
    /// <summary>
    /// Interaction logic for DirectoryTreeViewControl.xaml
    /// </summary>
    public partial class DirectoryTreeViewControl : UserControl
    {
        public DirectoryTreeViewControl()
        {
            InitializeComponent();

            ItemProvider itemProvider = new ItemProvider();
            itemProvider.PropertyChanged += reloadTreeView;

            var items = itemProvider.GetItems("D:\\Dave");

            DataContext = items;
        }

        private void reloadTreeView(object sender, RoutedEventArgs e)
        {
            var itemProvider = new ItemProvider();

            var items = itemProvider.GetItems("D:\\Dave");

            DataContext = items;
        }
    }
}
