using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using f0t0vi3w3r.Page;

namespace f0t0vi3w3r.Page.Map
{
    public delegate void NavigationPushedHandler(object sender, NavigationActionArgs e);

    public enum NavigationAction
    {
        MoveForward,
        MoveBackward,
    }

    /// <summary>
    /// A picture frame object
    /// Contains picturebox and navigation side panels
    /// </summary>
    public partial class f0t0Map : Panel, IPictureBox
    {
        private Color DefaultColor;
        private f0t0page fotopage;
        public event NavigationPushedHandler NavigationInvoked;
        public event System.IO.RenamedEventHandler PictureRenamed;
        public event System.IO.FileSystemEventHandler PictureDeleted;

        public f0t0Map()
        {
            InitializeComponent();
            DefaultColor = this.mainPictureBox.BackColor;
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
        }

        /// <summary>
        /// Unload the image when closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            this.mainPictureBox.Unload();
        }

        /// <summary>
        /// set the page of this control.
        /// necessary for navigation purposes or Delegate handlers can be used.
        /// Handlers take precedence.
        /// </summary>
        /// <param name="page"></param>
        public void SetFotoPage(f0t0page page)
        {
            this.fotopage = page;
        }

        /// <summary>
        /// Event: Occurs when a navigation panel is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void navPanel_Click(object sender, EventArgs e)
        {
            f0t0NavPanel panel = sender as f0t0NavPanel;

            if (panel != null)
            {
                NavigationActionArgs n = new NavigationActionArgs(panel.Action, e);
                if (NavigationInvoked != null)
                    NavigationInvoked(this, n);
                else
                {
                    if (this.fotopage != null)
                    {
                        if (panel.Action == NavigationAction.MoveForward)
                            this.fotopage.ShowNextPicture();
                        else
                            this.fotopage.ShowPreviousPicture();
                    }
                }
            }
        }

        /// <summary>
        /// Invoke navigation on an object with a given action (navigation action)
        /// </summary>
        /// <param name="a"></param>
        public void InvokeNavigation(NavigationAction a)
        {
            foreach (Control c in this.Controls)
            {
                f0t0NavPanel f = c as f0t0NavPanel;
                if (f != null && f.Action == a)
                {
                    if(!f.IsInvoked)
                        f.PerformInvoke();
                    break;
                }
            }
        }

        /// <summary>
        /// Uninvoke navigation on an object with a given action (navigation action)
        /// </summary>
        /// <param name="a"></param>
        public void UninvokeNavigation(NavigationAction a)
        {
            foreach (Control c in this.Controls)
            {
                f0t0NavPanel f = c as f0t0NavPanel;
                if (f != null && f.Action == a && f.IsInvoked)
                {
                    f.EndInvoke();
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picPanel_MouseEnter(object sender, System.EventArgs e)
        {
            this.picPanel.Focus();
        }

        /// <summary>
        /// Event: Occurs when the picture panel size changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picPanel_SizeChanged(object sender, System.EventArgs e)
        {
            Panel parent = sender as Panel;
            if(parent != null)
            {
                parent.SuspendLayout();
                foreach (Control c in parent.Controls)
                {
                    c.Dock = DockStyle.Fill;
                }
                parent.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Event: Occurs when the picturebox size changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mainPictureBox_SizeChanged(object sender, System.EventArgs e)
        {
            PictureBox picBox = sender as PictureBox;
            if (picBox != null)
            {
                if (picBox.Image != null)
                {
                    if (!IsGreaterThan(picBox.Image.Size, picPanel.ClientSize))
                        picBox.Dock = DockStyle.Fill;
                    else
                    {
                        picBox.Dock = DockStyle.None;
                        picBox.Size = picBox.Image.Size;
                    }
                }
                else picBox.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// Returns true if the width or height of "left" is greater than 'right'
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool IsGreaterThan(Size left, Size right)
        {
            return (left.Height > right.Height || left.Width > right.Width) ? true : false;
        }


        /// <summary>
        /// Show the image
        /// </summary>
        /// <param name="e"></param>
        public void Show(f0t0page.DisplayPictureEventArgs e)
        {
            this.mainPictureBox.Show(e);
        }

        /// <summary>
        /// Unload a picturebox
        /// </summary>
        public void Unload()
        {
            if (this.mainPictureBox.InvokeRequired)
            {
                f0t0vi3w3r.Page.PictureBox.UnloadDelegate unload = 
                    new f0t0vi3w3r.Page.PictureBox.UnloadDelegate(this.mainPictureBox.Unload);
                this.mainPictureBox.Invoke(unload);
            }
            else
                this.mainPictureBox.Unload();
        }

        void mainPictureBox_PictureRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            if (this.CanNavigate)
            {
                if (this.PictureRenamed != null)
                    this.PictureRenamed(this, e);
            }
            else
            {
                //this.LoadPicture(e.FullPath);
                if (this.PictureRenamed != null)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(e.FullPath);
                    this.PictureRenamed(this, new System.IO.RenamedEventArgs(System.IO.WatcherChangeTypes.Changed, fi.DirectoryName,
                        fi.FullName, e.OldFullPath));
                }
            }
        }


        void mainPictureBox_PictureDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            if (this.PictureDeleted != null)
                this.PictureDeleted(this, e);
        }


        #region Events
        /// <summary>
        /// An unload picture handler.s
        /// </summary>
        public event UnloadPictureEventHandler PictureUnloaded
        {
            add { lock(this.mainPictureBox) this.mainPictureBox.PictureUnloaded += value; }
            remove { lock (this.mainPictureBox) this.mainPictureBox.PictureUnloaded -= value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Initialize loadbox
        /// </summary>
        public void InitBox()
        {
            this.mainPictureBox.InitBox();
        }


        //Properties
        public ImageObject LoadedPicture
        {
            get { return this.mainPictureBox.LoadedPicture; }
        }

        public LoadPictureHandler LoadPicture
        {
            get { return this.mainPictureBox.LoadPicture; }
            set { this.mainPictureBox.LoadPicture = value; }
        }

        public Image Image
        {
            get { return this.mainPictureBox.Image; }
            set { this.mainPictureBox.Image = value; }
        }


        /// <summary>
        /// Indicates whether the panels on this map can cause navigation.
        /// </summary>
        public bool CanNavigate
        {
            get
            {
                bool answer = (upPanel.CanNavigate || downPanel.CanNavigate ||
                    leftPanel.CanNavigate || rightPanel.CanNavigate);
                return answer;
                
            }
            set
            {
                upPanel.CanNavigate = downPanel.CanNavigate =
                    leftPanel.CanNavigate = rightPanel.CanNavigate = value;
            }
        }
        #endregion
    }
}