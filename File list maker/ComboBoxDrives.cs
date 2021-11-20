using System.IO;
using System.Windows.Forms;

namespace File_list_maker
{
    public class ComboBoxDrives : ComboBox
    {
        public class DriveItem
        {
            public DriveInfo Drive { get; private set; }

            public DriveItem(DriveInfo driveInfo)
            {
                Drive = driveInfo;
            }

            public override string ToString()
            {
                return Drive.IsReady ? $"{Drive.Name} {Drive.VolumeLabel}" : $"{Drive.Name} <Not ready>";
            }
        }

        public void UpdateDriveList()
        {
            Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Items.Add(new DriveItem(drive));
            }
            SelectedIndex = Items.Count - 1;
        }
    }
}
