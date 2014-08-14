using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace f0t0vi3w3r.Page
{
    partial class PictureBox
    {
        class FileIOHandler 
        {
            PictureBox Box;
            public FileIOHandler(PictureBox box)
            {
                this.Box = box;
            }

            /// <summary>
            /// Perform a delete of the currrently loaded file
            /// </summary>
            public void PerformDelete()
            {
                if (Box.PictureDeleted != null && Box.LoadedPicture != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete this file?" + Environment.NewLine +
                        "The file will be removed from the location",
                        "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(Box.LoadedPicture.Name);
                        string fullname = fi.FullName;
                        string name = fi.Name;
                        string directory = fi.DirectoryName;
                        
                        Box.PictureDeleted(Box, new System.IO.FileSystemEventArgs(System.IO.WatcherChangeTypes.Deleted,
                        directory, name));
                    }
                } 
            }


            /// <summary>
            /// Perform the rename of the currently loaded picture
            /// </summary>
            public void PerformRename()
            {
                /*
                if (Box.PictureRenamed != null && Box.loadedPicture != null)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(Box.LoadedPicture.Name);
                    Box.PictureRenamed(Box, new System.IO.RenamedEventArgs(System.IO.WatcherChangeTypes.Renamed,
                        fi.Directory.FullName, fi.Name, ""));
                }
                */
            }
        }
    }
}
