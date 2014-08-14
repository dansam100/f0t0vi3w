using System;
using System.Collections.Generic;
using System.Text;
using f0t0vi3w3r.Page;
using System.Drawing;

namespace f0t0vi3w3r.Page.Loader
{
    public interface IF0t0Loader
    {
        void Load(object picture);
        Image GetNextImage();
        Image GetPreviousImage();
        Picture.PictureEnumerator Fotos { get;}
        ImageObject CurrentImage { get;}
        void Clear();
        void Remove(object picture);
        event EventHandler LoadCompleted;
        int HashSize { get;}
    }
}
