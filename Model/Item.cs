using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDirectoryTreeView.Model
{
    /// <summary>
    /// Base tree view item class.
    /// </summary>
    class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
