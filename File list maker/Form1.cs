using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static File_list_maker.Utils;
using static File_list_maker.ComboBoxDrives;

namespace File_list_maker
{
    public partial class Form1 : Form
    {
        private DateTime dateCreated;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config = new MainConfiguration(Application.StartupPath + "\\config_flm.json");
            config.Saving += (s, json) =>
            {
                json["lastUsedPath"] = config.lastUsedPath;
            };

            config.Loading += (s, json) =>
            {
                JToken jt = json.Value<JToken>("lastUsedPath");
                if (jt != null)
                {
                    config.lastUsedPath = jt.Value<string>();
                }
            };

            config.Load();

            ConfigureTreeView();

            comboBoxDrives1.UpdateDriveList();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            config.Save();
        }

        private void ConfigureTreeView()
        {
            treeListView1.ChildrenGetter = obj => { return ((ListItem)obj).Children; };
            treeListView1.ParentGetter = obj => { return ((ListItem)obj).Parent; };
            treeListView1.CanExpandGetter = obj => { return ((ListItem)obj).Children.Count > 0; };
            treeListView1.Roots = new List<ListItem>();
            colSize.AspectToStringConverter = val =>
            {
                long size = (long)val;
                return size >= 0 ? FormatSize(size) : "N / A";
            };
            colType.AspectToStringConverter = val =>
            {
                ListItemType itemType = (ListItemType)val;
                string res;
                switch (itemType)
                {
                    case ListItemType.Disk:
                        res = "Диск";
                        break;
                    case ListItemType.Directory:
                        res = "Папка";
                        break;
                    case ListItemType.File:
                        res = "Файл";
                        break;
                    default:
                        res = "N / A";
                        break;

                }
                return res;
            };
            colAttributes.AspectToStringConverter = val =>
            {
                if (val == null)
                {
                    return "N / A";
                }
                string attrs = (string)val;
                return attrs;
            };
            colCreationDate.AspectToStringConverter = val =>
            {
                DateTime dt = (DateTime)val;
                return dt > DateTime.MinValue ? dt.ToString("dd.MM.yyyy HH:mm:ss") : "N / A";
            };
            colModificationDate.AspectToStringConverter = val =>
            {
                DateTime dt = (DateTime)val;
                return dt > DateTime.MinValue ? dt.ToString("dd.MM.yyyy HH:mm:ss") : "N / A";
            };
            colLastAccessDate.AspectToStringConverter = val =>
            {
                DateTime dt = (DateTime)val;
                return dt > DateTime.MinValue ? dt.ToString("dd.MM.yyyy HH:mm:ss") : "N / A";
            };
        }

        private void btnOpenList_Click(object sender, EventArgs e)
        {
            DisableControls();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите файл";
            ofd.InitialDirectory = config.lastUsedPath;
            ofd.Filter = "Списки (JSON)|*.json";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                config.lastUsedPath = Path.GetDirectoryName(ofd.FileName);
                string ext = Path.GetExtension(ofd.FileName);
                if (!string.IsNullOrEmpty(ext))
                {
                    ext = ext.ToLower();
                    if (ext == ".json")
                    {
                        OpenJson(ofd.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Неподдерживаемый тип файла!", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            ofd.Dispose();

            EnableControls();
        }

        private void btnSaveList_Click(object sender, EventArgs e)
        {
            DisableControls();

            List<ListItem> roots = treeListView1.Roots.Cast<ListItem>().ToList();
            if (roots.Count == 0)
            {
                MessageBox.Show("Список пуст!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControls();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить список";
            sfd.Filter = "Списки (JSON)|*.json";
            sfd.DefaultExt = ".json";
            sfd.AddExtension = true;
            if (comboBoxDrives1.SelectedIndex >= 0)
            {
                DriveItem di = (DriveItem)comboBoxDrives1.Items[comboBoxDrives1.SelectedIndex];
                sfd.FileName = $"Disk {di.Drive.Name[0]}";
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveJson(sfd.FileName);
            }
            sfd.Dispose();

            EnableControls();
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            treeListView1.ClearObjects();
            dateCreated = DateTime.MinValue;
            lblDateCreated.Text = "Создан: N / A";
        }

        private void btnMakeDriveList_Click(object sender, EventArgs e)
        {
            DisableControls();

            if (comboBoxDrives1.SelectedIndex < 0)
            {
                MessageBox.Show("Ошибка!", "Не выбран диск!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControls();
                return;
            }

            DriveItem di = (DriveItem)comboBoxDrives1.Items[comboBoxDrives1.SelectedIndex];
            if (di.Drive.IsReady)
            {
                MakeDriveList(di.Drive.Name[0]);
            }
            else
            {
                MessageBox.Show($"Диск {di.Drive.Name} не готов!", "Ошибка овода Б!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EnableControls();
        }

        private void OpenJson(string fn)
        {
            JObject json = JObject.Parse(File.ReadAllText(fn));
            dateCreated = json.Value<DateTime>("buildDate");
            lblDateCreated.Text = $"Создан: {dateCreated}";
            JArray jsonArr = json.Value<JArray>("list");
            List<ListItem> roots = treeListView1.Roots.Cast<ListItem>().ToList();

            foreach (JObject j in jsonArr)
            {
                string nodeName = j.Value<string>("name");
                ListItem root = new ListItem(nodeName, ListItemType.Disk, null);
                JToken jt = j.Value<JToken>("label");
                string label = jt != null ? jt.Value<string>() : string.Empty;
                long size = j.Value<long>("size");
                long free = j.Value<long>("free");
                jt = j.Value<JToken>("buildDate");
                root.DateCreated = jt != null ? jt.Value<DateTime>() : DateTime.MinValue;
                root.DisplayName = $"{nodeName} {label} [{FormatSize(free)} / {FormatSize(size)}]";
                root.Size = size;
                root.DiskInfo.FreeSpace = free;
                JArray jArr = j.Value<JArray>("subItems");
                foreach (JObject j2 in jArr)
                {
                    ParseNode(j2, root);
                }
                roots.Add(root);
            }
            treeListView1.UpdateObjects(roots);

            void ParseNode(JObject jNode, ListItem treeNode)
            {
                string itemName = jNode.Value<string>("name");
                JToken jt = jNode.Value<JToken>("dateCreated");
                DateTime _dateCreated = jt != null ? jt.Value<DateTime>() : DateTime.MinValue;
                jt = jNode.Value<JToken>("dateModified");
                DateTime dateModified = jt != null ? jt.Value<DateTime>() : DateTime.MinValue;
                jt = jNode.Value<JToken>("dateLastAccess");
                DateTime dateLastAccess = jt != null ? jt.Value<DateTime>() : DateTime.MinValue;
                jt = jNode.Value<JToken>("size");
                long size = jt != null ? jt.Value<long>() : -1L;
                jt = jNode.Value<JToken>("attributes");
                string attr = jt != null ? jt.Value<string>() : null;
                jt = jNode.Value<JToken>("subItems");
                ListItemType itemType = jt != null ? ListItemType.Directory : ListItemType.File;
                ListItem node = new ListItem(itemName, itemType, treeNode);

                if (jt != null)
                {
                    JArray jArray = jt.Value<JArray>();
                    if (jArray.Count > 0)
                    {
                        foreach (JObject j in jArray)
                        {
                            ParseNode(j, node);
                        }
                    }
                }

                node.Size = size;
                node.DateCreated = _dateCreated;
                node.DateModified = dateModified;
                node.DateLastAccess = dateLastAccess;
                node.AttributesString = attr;
                node.DisplayName = itemName;
                treeNode.Children.Add(node);
            }
        }

        private void SaveJson(string fn)
        {
            List<ListItem> roots = treeListView1.Roots.Cast<ListItem>().ToList();
            JArray jRoots = new JArray();
            foreach (ListItem root in roots)
            {
                JObject j = new JObject();
                j["type"] = "disk";
                j["name"] = root.ItemName;
                if (!string.IsNullOrEmpty(root.DiskInfo.Label))
                {
                    j["label"] = root.DiskInfo.Label;
                }
                if (root.Size >= 0)
                {
                    j["size"] = root.Size;
                }
                if (root.DiskInfo.FreeSpace >= 0L)
                {
                    j["free"] = root.DiskInfo.FreeSpace;
                }
                if (root.DateCreated > DateTime.MinValue)
                {
                    j["buildDate"] = root.DateCreated;
                }
                if (root.Children.Count > 0)
                {
                    JArray jArray = new JArray();
                    foreach (ListItem item in root.Children)
                    {
                        SaveNode(item, jArray);
                    }
                    j["subItems"] = jArray;
                }
                jRoots.Add(j);
            }

            JObject json = new JObject();
            json["buildDate"] = dateCreated;
            json["list"] = jRoots;
            File.WriteAllText(fn, json.ToString());

            void SaveNode(ListItem node, JArray jsonArr)
            {
                JObject j = new JObject();
                j["type"] = node.ItemType.ToString();
                j["name"] = node.ItemName;
                if (node.AttributesString != null)
                {
                    j["attributes"] = node.AttributesString;
                }
                if (node.DateCreated > DateTime.MinValue)
                {
                    j["dateCreated"] = node.DateCreated;
                }
                if (node.DateModified > DateTime.MinValue)
                {
                    j["dateModified"] = node.DateModified;
                }
                if (node.DateLastAccess > DateTime.MinValue)
                {
                    j["dateLastAccess"] = node.DateLastAccess;
                }

                switch (node.ItemType)
                {
                    case ListItemType.Directory:
                        {
                            JArray jArray = new JArray();
                            foreach (ListItem subNode in node.Children)
                            {
                                SaveNode(subNode, jArray);
                            }
                            j["subItems"] = jArray;
                            break;
                        }

                    case ListItemType.File:
                        if (node.Size >= 0)
                        {
                            j["size"] = node.Size;
                        }
                        break;
                }
                jsonArr.Add(j);
            }
        }

        private void MakeDriveList(char driveLetter)
        {
            DisableControls();

            List<ListItem> roots = treeListView1.Roots.Cast<ListItem>().ToList();
            DriveInfo driveInfo = new DriveInfo(driveLetter.ToString());

            ListItem rootListItem = new ListItem(driveInfo.Name, ListItemType.Disk, null);
            rootListItem.Size = driveInfo.TotalSize;
            rootListItem.DiskInfo.FreeSpace = driveInfo.AvailableFreeSpace;
            rootListItem.DiskInfo.Label = driveInfo.VolumeLabel;
            string displayName = $"{driveInfo.Name} {rootListItem.DiskInfo.Label} " +
                $"[{FormatSize(driveInfo.AvailableFreeSpace)} / {FormatSize(driveInfo.TotalSize)}]";
            rootListItem.DisplayName = displayName;

            ParseDir(driveInfo.Name, rootListItem);

            dateCreated = DateTime.Now;
            rootListItem.DateCreated = dateCreated;
            roots.Add(rootListItem);
            treeListView1.UpdateObjects(roots);

            lblDateCreated.Text = $"Создан: {dateCreated}";

            EnableControls();

            void ParseDir(string dir, ListItem rootItem)
            {
                string[] rootDirs = Directory.GetDirectories(dir);
                string[] rootFiles = Directory.GetFiles(dir);

                foreach (string subDir in rootDirs)
                {
                    string subDirName = Path.GetFileName(subDir);
                    ListItem item = new ListItem(subDirName, ListItemType.Directory, rootItem);
                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(subDir);
                        item.DateCreated = directoryInfo.CreationTime;
                        item.DateModified = directoryInfo.LastWriteTime;
                        item.DateLastAccess = directoryInfo.LastAccessTime;
                        item.AttributesString = AttributesToString(directoryInfo.Attributes);
                        ParseDir(subDir, item);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    rootItem.Children.Add(item);
                }

                foreach (string file in rootFiles)
                {
                    lblDateCreated.Text = $"Сканирование: {file}";
                    Application.DoEvents();

                    string fileName = Path.GetFileName(file);
                    ListItem item = new ListItem(fileName, ListItemType.File, rootItem);
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        item.Size = fileInfo.Length;
                        item.DateCreated = fileInfo.CreationTime;
                        item.DateModified = fileInfo.LastWriteTime;
                        item.DateLastAccess = fileInfo.LastAccessTime;
                        item.AttributesString = AttributesToString(fileInfo.Attributes);
                        item.DisplayName = fileName;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    rootItem.Children.Add(item);
                }
            }
        }

        private void DisableControls()
        {
            btnMakeDriveList.Enabled = false;
            comboBoxDrives1.Enabled = false;
            btnOpenList.Enabled = false;
            btnSaveList.Enabled = false;
            btnClearList.Enabled = false;
        }

        private void EnableControls()
        {
            btnMakeDriveList.Enabled = true;
            comboBoxDrives1.Enabled = true;
            btnOpenList.Enabled = true;
            btnSaveList.Enabled = true;
            btnClearList.Enabled = true;
        }
    }
}
