using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using f0t0vi3w3r.Page.Cursors;

namespace f0t0vi3w3r.Page
{
    partial class PictureBox
    {
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        [DllImport("shell32.dll")]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        private bool contextMenuOpened = false;

        public event System.IO.RenamedEventHandler PictureRenamed;
        public event System.IO.FileSystemEventHandler PictureDeleted;

        private FileIOHandler fileIOHandler;

        //private ContextMenuStrip contextMenu;
        private ContextMenu contextMenu;
        /*private ToolStripMenuItem menuItemZoomIn,
                        menuItemZoomOut,
                        menuItemOpen,
                        menuItemClose,
                        menuItemRotateRight,
                        menuItemRotateLeft,
                        menuItemCopy,
                        menuItemRename,
                        menuItemEdit,
                        menuItemSetBG,
                        menuItemProperties,
                        menuItemDelete;*/
        private MenuItem menuItemZoomIn,
                        menuItemZoomOut,
                        menuItemOpen,
                        menuItemClose,
                        menuItemRotateRight,
                        menuItemRotateLeft,
                        menuItemCopy,
                        //menuItemRename,
                        menuItemExploreDirectory,
                        //menuItemEdit,
                        menuItemSetBG,
                        //menuItemProperties,
                        //menuItemDelete,
                        menuItemSplitter,
                        menuItemSplitter2,
                        menuItemSplitter3,
                        menuItemSplitter4,
                        menuItemSplitter5;

        public void InitializeComponents()
        {
            this.fileIOHandler = new FileIOHandler(this);
            /*this.contextMenu = new ContextMenuStrip();*/
            this.contextMenu = new ContextMenu();
            /*
            this.menuItemOpen = new ToolStripMenuItem();
            this.menuItemClose = new ToolStripMenuItem();
            this.menuItemCopy = new ToolStripMenuItem();
            this.menuItemDelete = new ToolStripMenuItem();
            this.menuItemEdit = new ToolStripMenuItem();
            this.menuItemProperties = new ToolStripMenuItem();
            this.menuItemRename = new ToolStripMenuItem();
            this.menuItemRotateLeft = new ToolStripMenuItem();
            this.menuItemRotateRight = new ToolStripMenuItem();
            this.menuItemSetBG = new ToolStripMenuItem();
            this.menuItemZoomIn = new ToolStripMenuItem();
            this.menuItemZoomOut = new ToolStripMenuItem();
            */
            this.menuItemOpen = new MenuItem();
            this.menuItemClose = new MenuItem();
            this.menuItemCopy = new MenuItem();
            //this.menuItemDelete = new MenuItem();
            this.menuItemExploreDirectory = new MenuItem();
            //this.menuItemEdit = new MenuItem();
            //this.menuItemProperties = new MenuItem();
            //this.menuItemRename = new MenuItem();
            this.menuItemRotateLeft = new MenuItem();
            this.menuItemRotateRight = new MenuItem();
            this.menuItemSetBG = new MenuItem();
            this.menuItemZoomIn = new MenuItem();
            this.menuItemZoomOut = new MenuItem();
            this.menuItemSplitter = new MenuItem();
            this.menuItemSplitter2 = new MenuItem();
            this.menuItemSplitter3 = new MenuItem();
            this.menuItemSplitter4 = new MenuItem();
            this.menuItemSplitter5 = new MenuItem();
            //
            //contextMenu
            //
            this.contextMenu.MenuItems.AddRange(new MenuItem[]{
            //this.contextMenu.Items.AddRange(new ToolStripItem[]{
                menuItemZoomIn,
                menuItemZoomOut,
                menuItemSplitter,
                menuItemRotateRight,
                menuItemRotateLeft,
                menuItemSplitter2,
                menuItemSetBG,
                menuItemSplitter3,
                menuItemExploreDirectory,
                //menuItemEdit,
                menuItemCopy,
                //menuItemRename,
                //menuItemDelete,
                menuItemSplitter4,
                menuItemOpen,
                menuItemClose,
                //menuItemSplitter5,
                //menuItemProperties,
            });
            //
            //menuItemZoomIn
            //
            this.menuItemZoomIn.Name = "zoomIn";
            this.menuItemZoomIn.Text = "Zoom &In";
            this.menuItemZoomIn.Enabled = false;
            this.menuItemZoomIn.Click += new EventHandler(menuItemZoomIn_Click);
            //
            //menuItemZoomOut
            //
            this.menuItemZoomOut.Name = "zoomOut";
            this.menuItemZoomOut.Text = "Zoom &Out";
            this.menuItemZoomOut.Enabled = false;
            this.menuItemZoomOut.Click += new EventHandler(menuItemZoomOut_Click);
            //
            //menuItemRotateRight
            //
            this.menuItemRotateRight.Name = "rotateRight";
            this.menuItemRotateRight.Text = "Rotate Clockwise";
            this.menuItemRotateRight.Enabled = false;
            this.menuItemRotateRight.Click += new EventHandler(menuItemRotateRight_Click);
            //
            //menuItemRotateLeft
            //
            this.menuItemRotateLeft.Name = "rotateLeft";
            this.menuItemRotateLeft.Text = "Rotate Anti-clockwise";
            this.menuItemRotateLeft.Enabled = false;
            this.menuItemRotateLeft.Click += new EventHandler(menuItemRotateLeft_Click);
            /*
            //
            //menuItemEdit
            //
            this.menuItemEdit.Name = "edit";
            this.menuItemEdit.Text = "Edit...";
            this.menuItemEdit.Enabled = false;
            this.menuItemEdit.Click += new EventHandler(menuItemEdit_Click);
            */
            //
            //menuItemExploreDirectory
            //
            this.menuItemExploreDirectory.Name = "exploredirectory";
            this.menuItemExploreDirectory.Text = "Explore picture directory";
            //this.menuItemExploreDirectory.Image = exploreIcon;
            this.menuItemExploreDirectory.Click += new EventHandler(menuItemExploreDirectory_Click);
            //
            //menuItemCopy
            //
            this.menuItemCopy.Name = "copy";
            this.menuItemCopy.Text = "C&opy...";
            this.menuItemCopy.Enabled = false;
            this.menuItemCopy.Shortcut = Shortcut.CtrlC;
            this.menuItemCopy.Click += new EventHandler(menuItemCopy_Click);
            /*
            //
            //menuItemRename
            //
            this.menuItemRename.Name = "rename";
            this.menuItemRename.Text = "Rename...";
            this.menuItemRename.Enabled = false;
            this.menuItemRename.Click += new EventHandler(menuItemRename_Click);
            //
            //menuItemDelete
            //
            this.menuItemDelete.Name = "delete";
            this.menuItemDelete.Text = "&Delete...";
            this.menuItemDelete.Enabled = false;
            this.menuItemDelete.Click += new EventHandler(menuItemDelete_Click);
            */
            //
            //menuItemClose
            //
            this.menuItemClose.Name = "close";
            this.menuItemClose.Text = "&Close...";
            this.menuItemClose.Enabled = false;
            this.menuItemClose.Click += new EventHandler(menuItemClose_Click);
            //
            //menuItemOpen
            //
            this.menuItemOpen.Name = "open";
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Enabled = true;
            this.menuItemOpen.Click += new EventHandler(menuItemOpen_Click);
            //
            //menuItemSetBG
            //
            this.menuItemSetBG.Name = "setbackground";
            this.menuItemSetBG.Text = "&Set as background...";
            this.menuItemSetBG.Enabled = false;
            this.menuItemSetBG.Click += new EventHandler(menuItemSetBG_Click);
            /*
            //
            //menuItemProperties
            //
            this.menuItemProperties.Name = "properties";
            this.menuItemProperties.Text = "P&roperties...";
            this.menuItemProperties.Enabled = false;
            this.menuItemProperties.Click += new EventHandler(menuItemProperties_Click);
            */
            ///*
            //
            //menuItemSplitter
            //
            this.menuItemSplitter.Name = "menuItemSplitter";
            this.menuItemSplitter.Text = "-";
            //
            //menuItemSplitter2
            //
            this.menuItemSplitter2.Name = "menuItemSplitter2";
            this.menuItemSplitter2.Text = "-";
            //
            //menuItemSplitter3
            //
            this.menuItemSplitter3.Name = "menuItemSplitter3";
            this.menuItemSplitter3.Text = "-";
            //
            //menuItemSplitter4
            //
            this.menuItemSplitter4.Name = "menuItemSplitter4";
            this.menuItemSplitter4.Text = "-";
            //
            //menuItemSplitter5
            //
            this.menuItemSplitter5.Name = "menuItemSplitter5";
            this.menuItemSplitter5.Text = "-";
            //*/
        }

        void ContextMenu_Collapse(object sender, EventArgs e)
        {
            this.contextMenuOpened = false;
        }


        void ContextMenu_Popup(object sender, EventArgs e)
        {
            this.contextMenuOpened = true;
            this.menuItemExploreDirectory.Enabled = (this.loadedPicture != null);
        }

        /// <summary>
        /// Event: Occurs when the picturebox is clicked.
        /// Causes zoom in or zoom out when in appropriate mode
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!contextMenuOpened)
            {
                switch (viewmode)
                {
                    case ViewMode.Normal:
                        break;
                    case ViewMode.ZoomIn:
                        this.OnMouseWheel(new MouseEventArgs(MouseButtons.Middle, e.Clicks, e.X, e.Y, DELTA * e.Clicks));
                        break;
                    case ViewMode.ZoomOut:
                        this.OnMouseWheel(new MouseEventArgs(MouseButtons.Middle, e.Clicks, e.X, e.Y, DELTA * e.Clicks * -1));
                        break;
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(this.loadedPicture != null && e.Button == MouseButtons.Left)
                this.DoDragDrop(this.loadedPicture.Name, DragDropEffects.All);
        }
        /// <summary>
        /// Event: Occurs when rotate clockwise is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemRotateRight_Click(object sender, EventArgs e)
        {
            ImageObject image = this.loadedPicture;
            image.Image = this.Image;
            image.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.Show(new f0t0page.DisplayPictureEventArgs(image, pictureSize));
        }

        /// <summary>
        /// Event: Occurs when rotate anti-clockwise is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemRotateLeft_Click(object sender, EventArgs e)
        {
            ImageObject image = this.loadedPicture;
            image.Image = this.Image;
            image.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.Show(new f0t0page.DisplayPictureEventArgs(image, pictureSize));
        }

        /// <summary>
        /// Event: Occurs when "copy" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemCopy_Click(object sender, EventArgs e)
        {
            this.PerformImageCopy();
        }

        /// <summary>
        /// Copy the currently loaded image to clipboard
        /// </summary>
        private void PerformImageCopy()
        {
            if (this.loadedPicture != null)
            {
                Clipboard.SetImage(this.loadedPicture.Image);
            }
        }

        void menuItemExploreDirectory_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(loadedPicture.Name);
            new Runner.Runner("explorer", fi.DirectoryName).Start();
        }

        /// <summary>
        /// Event: Occurs when "rename" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemRename_Click(object sender, EventArgs e)
        {
            this.fileIOHandler.PerformRename();
        }

        /// <summary>
        /// Event: Occurs when "delete" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemDelete_Click(object sender, EventArgs e)
        {
            this.fileIOHandler.PerformDelete();
        }

        /// <summary>
        /// Event: Occurs when "set as background" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemSetBG_Click(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Event: Occurs when "edit" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemEdit_Click(object sender, EventArgs e)
        {
            //new Runner.Runner().StartPaint(loadedPicture.Name);
            string execPath = Application.ExecutablePath;
            System.IO.FileInfo fi = new System.IO.FileInfo(execPath);
            new Runner.Runner(System.IO.Path.Combine(fi.DirectoryName, "mspaint.bat"), loadedPicture.Name).Start();
        }

        /// <summary>
        /// Event: Occurs when "open" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemOpen_Click(object sender, EventArgs e)
        {
            this.InitBox();
        }

        /// <summary>
        /// Event: Occurs when the properties context menu is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemProperties_Click(object sender, EventArgs e)
        {
            if (this.loadedPicture != null) ShowFileProperties(this.loadedPicture.Name);
        }

        /// <summary>
        /// Event: Occurs when the close menu item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemClose_Click(object sender, EventArgs e)
        {
            if (this.loadedPicture != null)
                this.Unload();
        }

        /// <summary>
        /// Event: Occurs when the preview key is down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    if (this.viewmode != ViewMode.Normal)
                    {
                        this.viewmode = ViewMode.Normal;
                        this.menuItemZoomIn.Enabled = true;
                        this.menuItemZoomOut.Enabled = true;
                        this.Cursor = ZoomCursor.Normal;
                    }
                    break;
            }
            base.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// Event: Occurs when "zoom in" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemZoomIn_Click(object sender, EventArgs e)
        {
            this.viewmode = ViewMode.ZoomIn;
            this.Cursor = new ZoomCursor(CursorType.ZoomIn).Cursor;
            this.menuItemZoomIn.Enabled = false;
        }

        /// <summary>
        /// Event: Occurs when "zoom out" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemZoomOut_Click(object sender, EventArgs e)
        {
            this.viewmode = ViewMode.ZoomOut;
            this.Cursor = new ZoomCursor(CursorType.ZoomOut).Cursor;
            this.menuItemZoomOut.Enabled = false;
        }


        /// <summary>
        /// Show file properties
        /// </summary>
        /// <param name="Filename"></param>
        public static void ShowFileProperties(string filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "open";
            info.lpFile = filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }

        /// <summary>
        /// Disable all controls in the context menu.
        /// </summary>
        public void DisableAllContextItems()
        {
            //if (this.ContextMenuStrip != null)
            if (this.ContextMenu != null)
            {
                //foreach (ToolStripMenuItem c in this.ContextMenuStrip.Items)
                foreach (MenuItem c in this.ContextMenu.MenuItems)
                {
                    c.Enabled = false;
                }
                this.menuItemOpen.Enabled = true;
            }
        }

        /// <summary>
        /// Enable all controls in the context menu.
        /// </summary>
        public void EnableAllContextItems()
        {
            //if (this.ContextMenuStrip != null)
            if (this.ContextMenu != null)
            {
                //foreach (ToolStripMenuItem c in this.ContextMenuStrip.Items)
                foreach (MenuItem c in this.ContextMenu.MenuItems)
                {
                    c.Enabled = true;
                }
                this.menuItemOpen.Enabled = true;
            }
        }


        /// <summary>
        /// Unload all images on the current picturebox
        /// </summary>
        public void Unload()
        {
            this.Image = null;
            this.loadedPicture.Dispose();
            this.loadedPicture = null;
            this.DisableAllContextItems();

            this.Size = new Size(0, 0); //reset size
            if (this.frameCounterThread != null && frameCounterThread.IsAlive)
                frameCounterThread.Abort();
            if (this.PictureUnloaded != null)
                PictureUnloaded(this);
        }
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public uint fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public String lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public String lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public String lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public String lpDirectory;
        public int nShow;
        public int hInstApp;
        public int lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public String lpClass;
        public int hkeyClass;
        public uint dwHotKey;
        public int hIcon;
        public int hProcess;
    }
}
