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
        private ItemProvider _itemProvider;
        public DirectoryTreeViewControl()
        {
            InitializeComponent();

            _itemProvider = new ItemProvider();
            _itemProvider.PropertyChanged += reloadTreeView;
          //  itemProvider.strPath = "E:\\Burn";

          //  var items = itemProvider.GetItems("D:\\Dave");

          //  DataContext = items;
        }

        public void reloadTreeView(object sender, PropertyChangedEventArgs e)
        {
            //var i = new PropertyChangedEventArgs(e);

            var items = _itemProvider.GetItems(e.PropertyName);
//            var items = _itemProvider.GetItems(_itemProvider.strPath);

            DataContext = items;
        }
    }
}
