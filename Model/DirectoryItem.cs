using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDirectoryTreeView.Model
{
    /// <summary>
    /// Extension of base class as directory item.
    /// Contains a list of items representing directories and files.
    /// </summary>
    class DirectoryItem : Item
    {
        // Create item list.
        public List<Item> Items { get; set; }

        /// <summary>
        /// Constructor creating empty item list.
        /// </summary>
        public DirectoryItem()
        {
            Items = new List<Item>();
        }
    }
}
