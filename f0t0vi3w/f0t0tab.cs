using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using f0t0vi3w3r.Page.Map;
using System.Reflection;
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using System.Collections;

namespace f0t0vi3w3r.Page
{
    /// <summary>
    /// Tab control with an added oomph
    /// </summary>
    [ToolboxBitmap(typeof(TabControl))]
    public partial class f0t0tab : System.Windows.Forms.TabControl
    {
        private f0t0Box fotoBox;

        public static Orientation Orientation = Orientation.Vertical;
        private Dictionary<int, TabPage> closeTabList;
        private ContextMenuStrip menuStrip;
        private ToolStripMenuItem menuItemCloseTab,
            menuItemUndoCloseTab,
            menuItemSplitter,
            menuItemNewTab;

        private Image closeImage, undoCloseImage, newTabImage, exploreIcon;
        private int textLength = 20;
        
        public f0t0tab()
        {
            this.menuStrip = new ContextMenuStrip();
            this.menuItemCloseTab = new ToolStripMenuItem();
            this.menuItemUndoCloseTab = new ToolStripMenuItem();
            this.menuItemSplitter = new ToolStripMenuItem();
            this.menuItemNewTab = new ToolStripMenuItem();

            this.closeTabList = new Dictionary<int, TabPage>(5);
            this.undoCloseImage = Properties.Resources.recycle_symbol;
            this.newTabImage = Properties.Resources.newtab;
            this.closeImage = Properties.Resources.close_btn_u;
            this.exploreIcon = Properties.Resources.magnifying_glass;
            //
            //menuStrip
            //
            this.menuStrip.Items.AddRange(new ToolStripMenuItem[] {
                this.menuItemNewTab,
                this.menuItemCloseTab,
                this.menuItemUndoCloseTab,
                //menuItemSplitter
            });
            this.menuStrip.Opening += new System.ComponentModel.CancelEventHandler(menuStrip_Opening);
            //
            //menuItemNewTab
            //
            this.menuItemNewTab.Name = "newtab";
            this.menuItemNewTab.Text = "New Tab";
            this.menuItemNewTab.Image = newTabImage;
            this.menuItemNewTab.Click += new EventHandler(menuItemNewTab_Click);

            //
            //menuItemCloseTab
            //
            this.menuItemCloseTab.Name = "closetab";
            this.menuItemCloseTab.Text = "Cl&ose Tab";
            this.menuItemCloseTab.Image = closeImage;
            this.menuItemCloseTab.Click += new EventHandler(menuItemCloseTab_Click);
            //
            //menuItemUndoCloseTab
            //
            this.menuItemUndoCloseTab.Name = "undoclosetab";
            this.menuItemUndoCloseTab.Text = "Closed Tabs List";
            this.menuItemUndoCloseTab.Image = undoCloseImage;
            //
            //menuItemSplitter
            //
            this.menuItemSplitter.Name = "splitter";
            this.menuItemSplitter.Text = "-";
            //
            //f0t0tab
            //
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabStop = false;
            this.ItemSize = new System.Drawing.Size(180, 25);
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.HotTrack = true;
            this.KeyDown += new KeyEventHandler(f0t0tab_KeyDown);
            this.KeyUp += new KeyEventHandler(f0t0tab_KeyUp);
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.AllowDrop = true;
            this.ImageList = new ImageList();
            this.Name = "tabs";
            this.SelectedIndex = 0;
            this.ShowToolTips = true;
            this.Size = new System.Drawing.Size(753, 601);
            this.ContextMenuStrip = menuStrip;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            //Set draw properties
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Event: Occurs when the menu is opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.menuItemCloseTab.Enabled = CurrentPage.CanClose || CurrentPage.IsActive;
            if (this.closeTabList.Count > 0)
            {
                this.menuItemUndoCloseTab.Enabled = true;
                List<ToolStripMenuItem> items = new List<ToolStripMenuItem>(this.closeTabList.Count);
                foreach (KeyValuePair<int, TabPage> kvp in this.closeTabList)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = kvp.Value.Text;
                    item.Name = string.Concat(kvp.Value.Name, kvp.Value.Text);
                    item.Click += new EventHandler(ClosedTabsItem_Click);
                    items.Add(item);
                }
                this.menuItemUndoCloseTab.DropDownItems.Clear();
                this.menuItemUndoCloseTab.DropDownItems.AddRange(items.ToArray());
            }
            else this.menuItemUndoCloseTab.Enabled = false;
        }

        /// <summary>
        /// Event: Occurs when an item in the closed tabs list is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClosedTabsItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.UnClose((sender as ToolStripMenuItem).Name);
            }
            catch { }
        }

        /// <summary>
        /// Event: Occurs when the "close tab" menu is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemCloseTab_Click(object sender, EventArgs e)
        {
            this.Close(this.SelectedTab);
        }

        /// <summary>
        /// Creates a new tab
        /// </summary>
        internal void CreateNewTab()
        {
            f0t0page newtab = new f0t0page();
            newtab.Name = string.Format("{0}{1}", newtab.Name, this.TabCount);
            newtab.SetFotoBox(fotoBox);
            this.TabPages.Add(newtab);
            this.SelectTab(newtab.Name);
            newtab.Focus();
        }

        /// <summary>
        /// Event: occurs when mouse button is down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedTab = this.GetTabPageByLocation(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                f0t0page page = this.SelectedTab as f0t0page;
                if (page != null && page.CanClose)
                {
                    Rectangle closeBtnBounds = page.CloseBounds;
                    if (closeBtnBounds.Contains(e.Location))
                    {
                        page.CloseHot = true;
                        //this.Close(page);
                    }
                }
            }
            base.OnMouseDown(e);
        }

        
        /// <summary>
        /// Event: Occurs when the mouse button goes up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            f0t0page page = this.GetTabPageByLocation(e.Location) as f0t0page;
            if (e.Button == MouseButtons.Left)
            {
                if (page != null && page.CanClose && page.CloseHot)
                {
                    Rectangle closeBtnBounds = page.CloseBounds;
                    if (closeBtnBounds.Contains(e.Location))
                        this.Close(page);
                }
            }
        }


        /*
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            f0t0page page = this.GetTabPageByLocation(e.Location) as f0t0page;
            if (page != null && page.CanClose)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Rectangle closeBtnBounds = page.CloseBounds;
                    if (closeBtnBounds.Contains(e.Location) && page.CloseHot)
                        this.Close(page);
                    else page.CloseHot = false;
                }
            }
        }
        */

        /// <summary>
        /// Process command key
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool process = false;
            if (keyData == Keys.Right || keyData == Keys.Left ||
                keyData == Keys.Up || keyData == Keys.Down)
                process = true;
            return process ? true : base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// Event: Occurs when a key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void f0t0tab_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                case Keys.Down:
                    this.CurrentPage.UnInvokeNavigation(NavigationAction.MoveForward);
                    break;
                case Keys.Left:
                case Keys.Up:
                    this.CurrentPage.UnInvokeNavigation(NavigationAction.MoveBackward);
                    break;
            }
        }

        /// <summary>
        /// Event: Occurs when a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void f0t0tab_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                case Keys.Down:
                    this.CurrentPage.InvokeNavigation(NavigationAction.MoveForward);
                    break;
                case Keys.Left:
                case Keys.Up:
                    this.CurrentPage.InvokeNavigation(NavigationAction.MoveBackward);
                    break;
            }
        }

        
        /// <summary>
        /// Event: Occurs when the mouse is dragged over the tabcontrol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="drgevent"></param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            Point p = new Point(drgevent.X, drgevent.Y);
            p = this.PointToClient(p);

            f0t0page hovered_page = this.GetTabPageByLocation(p) as f0t0page;
            if (hovered_page != null)
            {
                this.SelectedTab = hovered_page;
                if (drgevent.Data.GetDataPresent(typeof(f0t0page)))
                {
                    drgevent.Effect = DragDropEffects.Move;
                    f0t0page movingTab = drgevent.Data.GetData(typeof(f0t0page)) as f0t0page;
                    int currentIndex = FindTabIndex(hovered_page);
                    int movingTabIndex = FindTabIndex(movingTab);
                    if (currentIndex.CompareTo(movingTabIndex) != 0)
                    {
                        this.SuspendLayout();
                        if (currentIndex >= 0 || movingTabIndex >= 0)
                        {
                            this.TabPages[currentIndex] = movingTab;
                            this.TabPages[movingTabIndex] = hovered_page;
                            bool canclose = movingTab.CanClose;
                            movingTab.CanClose = hovered_page.CanClose;
                            hovered_page.CanClose = canclose;
                        }
                        this.ResumeLayout(true);
                        this.SelectedTab = movingTab;
                    }
                }
                else
                {
                    drgevent.Effect = DragDropEffects.Link;
                    hovered_page.Focus();
                }
            }
        }


        /// <summary>
        /// Event: occurs when the mouse moves over the surface of a control
        /// </summary>
        /// <param name="e">mouse event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.SuspendLayout();
            base.OnMouseMove(e);
            Point pt = new Point(e.X, e.Y);
            f0t0page page = GetTabPageByLocation(pt) as f0t0page;
            if (e.Button == MouseButtons.Left)
            {
                if (page != null)
                {
                    this.DoDragDrop(page, DragDropEffects.All);
                }
            }
            this.ResumeLayout(true);
        }


        /// <summary>
        /// Given a tabpage, find its index in the tabcontrol.
        /// </summary>
        /// <param name="page">page index</param>
        /// <returns></returns>
        public int FindTabIndex(TabPage page)
        {
            for(int i = 0; i < this.TabPages.Count; i++)
            {
                if (TabPages[i] == page)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Get a TabPage given a location on the screen.
        /// </summary>
        /// <param name="p">point given</param>
        /// <returns>tabpage at current point</returns>
        private TabPage GetTabPageByLocation(Point p)
        {
            foreach (TabPage tp in this.TabPages)
            {
                if (GetTabRect(FindTabIndex(tp)).Contains(p))
                    return tp;
            }
            return null;
        }

        /// <summary>
        /// Add an image to the tabs imagelist for retrieval
        /// </summary>
        /// <param name="image">the image to add</param>
        public void AddImage(ImageObject image)
        {
            if(image != null)
                this.ImageList.Images.Add(image.Name, image.Image);
        }

        /// <summary>
        /// Remove an image from the imagelist of the tab
        /// </summary>
        /// <param name="key">the image name/key</param>
        public void RemoveImage(string key)
        {
            if(key != null)
                this.ImageList.Images.RemoveByKey(key);
        }


        /// <summary>
        /// Close a given tabpage
        /// </summary>
        /// <param name="tabpage">the tabpage to close</param>
        public void Close(TabPage tabpage)
        {
            if (this.Contains(tabpage))
            {
                f0t0page f = (tabpage as f0t0page);
                if (f.CanClose)
                {
                    int tabIndex = this.FindTabIndex(f);
                    while (this.closeTabList.ContainsKey(tabIndex))
                        tabIndex++;
                    this.closeTabList.Add(tabIndex, tabpage);

                    //TODO: Design a DS to hold tabpages so that an upper limit can be set.

                    this.TabPages.Remove(tabpage);
                }
                else this.CurrentPage.UnloadPicture(this);
            }
        }

        /// <summary>
        /// Uncloses a tabpage
        /// </summary>
        /// <param name="closedIndex"></param>
        public void UnClose(string pageID)
        {
            int closedIndex = -1;
            foreach (KeyValuePair<int, TabPage> kvp in this.closeTabList)
            {
                if (string.Concat(kvp.Value.Name, kvp.Value.Text).CompareTo(pageID) == 0)
                {
                    closedIndex = kvp.Key;
                    break;
                }
            }
            if (this.closeTabList.ContainsKey(closedIndex))
            {
                this.TabPages.Insert(closedIndex, this.closeTabList[closedIndex] as TabPage);
                this.SelectedTab = this.closeTabList[closedIndex];
                this.closeTabList.Remove(closedIndex);
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets whether all tabpages are showing tiny images.
        /// </summary>
        public bool ShowTinyImages
        {
            get { return f0t0page.ShowTinyImage; }
            set { f0t0page.ShowTinyImage = value; }
        }

        /// <summary>
        /// Gets or sets whether the photobox should lock and allow navigation by scrolling
        /// </summary>
        public bool LockAndScroll
        {
            get { return f0t0NavPanel.LockAndScroll; }
            set { f0t0NavPanel.LockAndScroll = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if each page should allow auto repetition.
        /// </summary>
        public bool AllowRepeat
        {
            get { return f0t0NavPanel.AllowRepeat; }
            set { f0t0NavPanel.AllowRepeat = value; }
        }

        /// <summary>
        /// Gets or sets the auto repeat delay
        /// </summary>
        public int AutoRepeatDelay
        {
            get { return f0t0NavPanel.AutoRepeatDelay; }
            set { f0t0NavPanel.AutoRepeatDelay = value; }
        }

        /// <summary>
        /// Gets or sets the Split Orientation of each page.
        /// </summary>
        public Orientation SplitOrientation
        {
            get { return Orientation; }
            set
            {
                foreach (f0t0page p in this.TabPages)
                {
                    p.SplitOrientation = value;
                }
                Orientation = value;
            }
        }

        /// <summary>
        /// Tab width
        /// </summary>
        public int TabWidth
        {
            get { return this.textLength; }
            set
            {
                this.textLength = value;
                this.ItemSize = new Size((int)(value * 6 + 20), 25);
                f0t0page.TextLength = value;
            }
        }

        /// <summary>
        /// Sets wheter the clone picturebox can navigate.
        /// </summary>
        public bool CloneCanNavigate
        {
            set
            {
                foreach (f0t0page p in this.TabPages)
                {
                    p.CloneNavigation = value;
                }
            }
        }

        /// <summary>
        /// Gets the currently selected tabpage.
        /// </summary>
        public f0t0page CurrentPage
        {
            get { return (f0t0page)this.SelectedTab; }
        }

        public f0t0Box FotoBox
        {
            get { return this.fotoBox; }
            set { this.fotoBox = value; }
        }
        #endregion

    }
}
