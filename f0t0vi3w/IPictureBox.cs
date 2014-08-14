using System;
using System.Collections.Generic;
using System.Text;

namespace f0t0vi3w3r.Page
{
    public interface IPictureBox
    {
        
        //Properties
        ImageObject LoadedPicture { get;}
        LoadPictureHandler LoadPicture { get; set;}
        void Show(f0t0page.DisplayPictureEventArgs e);
        void InitBox();
        void Unload();
        event System.IO.RenamedEventHandler PictureRenamed;
        event System.IO.FileSystemEventHandler PictureDeleted;
        System.Drawing.Image Image { get; set;}
    }
}
