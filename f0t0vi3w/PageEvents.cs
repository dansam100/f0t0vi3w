using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using f0t0vi3w3r.Tools;
using System.Drawing.Drawing2D;

namespace f0t0vi3w3r.Page
{
    partial class f0t0page
    {
        /*
        protected void DrawItem(object sender, DrawItemEventArgs e)
        {
            Parent.PerformDraw(this);
        }

        /// <summary>
        /// override to draw the close button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Parent.PerformDraw(this);
        }
        */

        /// <summary>
        /// parent tab control
        /// </summary>
        public new f0t0tab Parent
        {
            get { return base.Parent as f0t0tab; }
        }
        
        
        /// <summary>
        /// Display Picture Event args
        /// ImageObject, Dimensions.
        /// </summary>
        public class DisplayPictureEventArgs : EventArgs
        {
            private ImageObject image;
            private Rectangle dimensions;
            public DisplayPictureEventArgs(ImageObject image)
            {
                this.image = image;
                this.dimensions = new Rectangle(0, 0, image.Image.Width, image.Image.Height);
            }

            public DisplayPictureEventArgs(ImageObject image, Rectangle dimensions)
            {
                this.image = image;
                this.dimensions = dimensions;
            }

            public ImageObject Image
            {
                get { return image; }
            }

            public Rectangle Dimensions
            {
                get { return dimensions; }
            }
        }


        public class CloningEventArgs : EventArgs
        {
            private string mainPictureName, clonedPictureName;

            public CloningEventArgs(string mainPictureName, string clonedPictureName)
            {
                this.mainPictureName = mainPictureName;
                this.clonedPictureName = clonedPictureName;
            }

            public string MainPictureName
            {
                get { return this.mainPictureName; }
            }

            public string ClonedPictureName
            {
                get { return this.clonedPictureName; }
            }
        }
    }
}
