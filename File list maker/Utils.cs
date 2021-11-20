using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace File_list_maker
{
    public static class Utils
    {
        public class MainConfiguration
        {
            public string configFileName;
            public string selfPath;
            public string lastUsedPath;

            public delegate void SavingDelegate(object sender, JObject root);
            public delegate void LoadingDelegate(object sender, JObject root);
            public SavingDelegate Saving;
            public LoadingDelegate Loading;

            public MainConfiguration(string fileName)
            {
                configFileName = fileName;
                selfPath = Application.StartupPath;

                LoadDefaults();
            }

            public void Save()
            {
                if (File.Exists(configFileName))
                {
                    File.Delete(configFileName);
                }
                JObject json = new JObject();
                Saving?.Invoke(this, json);
                File.WriteAllText(configFileName, json.ToString());
            }

            public void LoadDefaults()
            {
                lastUsedPath = selfPath;
            }

            public void Load()
            {
                if (File.Exists(configFileName))
                {
                    JObject json = JObject.Parse(File.ReadAllText(configFileName));
                    if (json != null)
                    {
                        Loading?.Invoke(this, json);
                    }
                }
            }
        }

        public class DiskRootItem
        {
            public long FreeSpace { get; set; }
            public string Label { get; set; }
        }

        public enum ListItemType { Disk, Directory, File }

        public class ListItem
        {
            public string ItemName { get; private set; }
            public string DisplayName { get; set; }
            public long Size { get; set; } = -1L;
            public DateTime DateCreated { get; set; } = DateTime.MinValue;
            public DateTime DateModified { get; set; } = DateTime.MinValue;
            public DateTime DateLastAccess { get; set; } = DateTime.MinValue;
            public string AttributesString { get; set; } = null;
            public ListItemType ItemType { get; private set; }
            public DiskRootItem DiskInfo { get; set; } = null;
            public ListItem Parent { get; private set; }
            public List<ListItem> Children { get; private set; } = new List<ListItem>();

            public ListItem(string itemName, ListItemType itemType, ListItem parent)
            {
                ItemName = itemName;
                DisplayName = itemName;
                ItemType = itemType;
                Parent = parent;
                if (itemType == ListItemType.Disk)
                {
                    DiskInfo = new DiskRootItem();
                }
            }
        }

        public static MainConfiguration config;

        public static string AttributesToString(FileAttributes attributes)
        {
            string str = string.Empty;

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                str += "R";
            if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
                str += "A";
            if ((attributes & FileAttributes.System) == FileAttributes.System)
                str += "S";
            if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                str += "H";
            if ((attributes & FileAttributes.Normal) == FileAttributes.Normal)
                str += "N";
            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                str += "D";
            if ((attributes & FileAttributes.Offline) == FileAttributes.Offline)
                str += "O";
            if ((attributes & FileAttributes.Compressed) == FileAttributes.Compressed)
                str += "C";
            if ((attributes & FileAttributes.Temporary) == FileAttributes.Temporary)
                str += "T";

            return str;
        }

        public static string FormatSize(long n)
        {
            if (n < 0)
            {
                return "<ERROR>";
            }

            const int KB = 1000;
            const int MB = 1000000;
            const int GB = 1000000000;
            const long TB = 1000000000000;
            long b = n % KB;
            long kb = (n % MB) / KB;
            long mb = (n % GB) / MB;
            long gb = (n % TB) / GB;
            long tb = n / TB;

            if (n >= 0 && n < KB)
                return string.Format("{0} b", b);
            if (n >= KB && n < MB)
                return string.Format("{0},{1:D3} KB", kb, b);
            if (n >= MB && n < GB)
                return string.Format("{0},{1:D3} MB", mb, kb);
            if (n >= GB && n < TB)
                return string.Format("{0},{1:D3} GB", gb, mb);

            return string.Format("{0} {1:D3} TB", tb, gb);
        }
    }
}
