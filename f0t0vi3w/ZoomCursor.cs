using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace f0t0vi3w3r.Page.Cursors
{
    public enum CursorType
    {
        ZoomIn,
        ZoomOut,
    }

    class ZoomCursor
    {
        private Cursor cursor;
        private static Size iconSize;
        public ZoomCursor(CursorType type)
        {
            iconSize = new Size(80, 80);
            LoadCursor(type);
        }

        /// <summary>
        /// Load a cursor based on the type, whether zoom in or zoom out
        /// </summary>
        /// <param name="type"></param>
        public void LoadCursor(CursorType type)
        {
            switch(type)
            {
                case CursorType.ZoomIn:
                    using (Bitmap bmp = new Bitmap(Properties.Resources.zoomin, iconSize))
                    {
                        cursor = CreateCursor(bmp, 3, 3);
                    }
                    break;
                case CursorType.ZoomOut:
                    using (Bitmap bmp = new Bitmap(Properties.Resources.zoomout, iconSize))
                    {
                        cursor = CreateCursor(bmp, 40, 40);
                    }
                    break;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }

        /// <summary>
        /// Cursor
        /// </summary>
        public Cursor Cursor
        {
            get { return this.cursor; }
        }

        /// <summary>
        /// Default OS cursor
        /// </summary>
        public static Cursor Normal
        {
            get
            {
                return System.Windows.Forms.Cursors.Default;
            }
        }
    }

    /// <summary>
    /// Cursor info
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
}
