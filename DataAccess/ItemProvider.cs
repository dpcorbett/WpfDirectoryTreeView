using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using WpfDirectoryTreeView.Model;

namespace WpfDirectoryTreeView.DataAccess
{
    class ItemProvider : INotifyPropertyChanged
    {

        private string m_strPath;

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemProvider()
        {
        }

        public ItemProvider(string p_strPath)
        {
            this.m_strPath = p_strPath;
        }


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


        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string m_strPath)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(m_strPath));
            }
        }

        public List<Item> GetItems(string path)
        {
            var items = new List<Item>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GetItems(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }
}
