using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using WpfDirectoryTreeView.Model;

namespace WpfDirectoryTreeView.DataAccess
{
    /// <summary>
    /// Provide a directory and file list with event driven refresh.
    /// </summary>
    class ItemProvider : INotifyPropertyChanged
    {
        // Store root directory.
        private string m_strPath;

        // Exposed event.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        /// <returns></returns>
        public ItemProvider()
        {
        }

        /// <summary>
        /// Constructor taking root directory.
        /// </summary>
        /// <param name="p_strPath"></param>
        public ItemProvider(string p_strPath)
        {
            m_strPath = p_strPath;
        }

        /// <summary>
        /// Exposed root directory property.
        /// </summary>
        public string strPath
        {
            get { return m_strPath; }
            set
            {
                m_strPath = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("strPath");
            }
        }

        /// <summary>
        /// Create OnPropertyChanged method to raise the event.
        /// </summary>
        /// <param name="m_strPath"></param>
        protected void OnPropertyChanged(string m_strPath)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(m_strPath));

        }

        /// <summary>
        /// Create list of items (directory and file) for XAML HierarchicalDataTemplate.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Item> GetItems(string path)
        {
            // Create list.
            var items = new List<Item>();

            try
            {
                // Set root directory on DirectoryInfo object.
                var dirInfo = new DirectoryInfo(path);
                // Add sub directories with recursive calls.
                foreach (var directory in dirInfo.GetDirectories())
                {
                    // Create DirectoryItem.
                    var item = new DirectoryItem
                    {
                        Name = directory.Name,
                        Path = directory.FullName,
                        Items = GetItems(directory.FullName)
                    };
                    // Add to collection.
                    items.Add(item);
                }
                // Add files contained in root directory.
                foreach (var file in dirInfo.GetFiles())
                {
                    // Create new FileItem.
                    var item = new FileItem
                    {
                        Name = file.Name,
                        Path = file.FullName
                    };
                    // Add to collection.
                    items.Add(item);
                }
                return items;
            }
            catch (UnauthorizedAccessException uae)
            {
                // Log and ignore directory access denied attempts.
                Console.WriteLine(uae.Message);
                return items;
            }
            catch (Exception e)
            {
                // Log and ignore standard exceptions.
                Console.WriteLine(e.Message);
                return items;
            }
        }
    }
}
