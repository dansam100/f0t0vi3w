using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using f0t0vi3w3r.Tools;
using System.Threading;
using f0t0vi3w3r.Page;
using f0t0vi3w3r.Page.Loader;
using System.IO;
using f0t0vi3w3r.Page.Map;

namespace f0t0vi3w3r.Page
{
    public delegate void DisplayPictureHandler(object sender, f0t0page.DisplayPictureEventArgs e);
    public delegate void CloneEventHandler(object sender, f0t0page.CloningEventArgs e);
    public delegate void MouseWheelEventHandler(object sender, MouseEventArgs e);
        
    public partial class f0t0page : TabPage
    {
        private f0t0Box fotobox;
        private IF0t0Loader loader;
        private const string defaultText = "n0 pic";
        public Rectangle CloseBounds;
        internal bool CloseHot;
        private delegate void ShowPicture();

        private DisplayPictureHandler DisplayPicture;
        private event CloneEventHandler CloneOpened, CloneClosed;

        private System.Boolean canClose = true;
        private static bool showTinyImage = false, canNavigateClone = false;
        private static int textLength = 20;
        private static int hashSize = 5;

        /// <summary>
        /// default ctor
        /// </summary>
        public f0t0page()
        {
            InitializeComponent();
            //this.loader = new PictureHashLoader(hashSize);
            this.loader = new f0t0Loader();

            //TODO: In future, make loader associate itself with fotomap instead of page.

            this.DisplayPicture = new DisplayPictureHandler(this.DisplayPics);
            this.CloneOpened += new CloneEventHandler(this.InitClone);
            this.CloneClosed += new CloneEventHandler(this.CloseClone);
            this.loader.LoadCompleted += new System.EventHandler(loader_LoadCompleted);
            this.MouseEnter += new EventHandler(f0t0page_MouseEnter);
            
        }

        public void SetFotoTab(f0t0tab parent)
        {
            base.Parent = parent;
        }

        /// <summary>
        /// ctor with parent setter.
        /// </summary>
        /// <param name="fotobox"></param>
        public f0t0page(f0t0Box fotobox) : this()
        {
            this.fotobox = fotobox;
        }

        #region Loading...
        /// <summary>
        /// Invoke the load of a picture
        /// </summary>
        /// <param name="picture"></param>
        public void Load(string picture)
        {
            ManualResetEvent loadEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(loader.Load, new object[] { picture, loadEvent });
            WaitHandle.WaitAll(new WaitHandle[] { loadEvent });
        }

        /// <summary>
        /// Image loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void loader_LoadCompleted(object sender, System.EventArgs e)
        {
            this.ShowImage(); //show the image.
        }

        /// <summary>
        /// Show the image on another thread.
        /// </summary>
        void ShowImage()
        {
            if (this.InvokeRequired)
            {
                ShowPicture d = new ShowPicture(ShowImage);
                this.Invoke(d, null);
            }
            else
            {
                this.DisplayPicture(this.mainPicBox, new DisplayPictureEventArgs(loader.CurrentImage));
                fotobox.UpdateAll();
            }
        }

        /// <summary>
        /// Scale an image to fit certain dimensions
        /// </summary>
        /// <param name="targetArea">target dimension</param>
        /// <param name="img">image to resize</param>
        /// <returns>the perfect size for the image to fit the target area</returns>
        public static Rectangle ScaleToFit(Rectangle targetArea, System.Drawing.Image img)
        {
            Rectangle r = new Rectangle(
                targetArea.Location, targetArea.Size);

            // Determine best fit: width or height
            if (r.Height * img.Width > r.Width * img.Height)
            {
                // Use width, determine height
                r.Height = r.Width * img.Height / img.Width;
                r.Y += (targetArea.Height - r.Height) / 2;
            }
            else
            {
                // Use height, determine width
                r.Width = r.Height * img.Width / img.Height;
                r.X += (targetArea.Width - r.Width) / 2;
            }

            return r;
        }

        /// <summary>
        /// Display a pciture on a given picturebox with the given specifications
        /// </summary>
        public void DisplayPics(object sender, f0t0page.DisplayPictureEventArgs e)
        {
            Image pix = e.Image.Image;
            IPictureBox pictureBox = sender as IPictureBox;
            pictureBox.Image = null;
            try
            {
                //Rectangle rectangle = ScaleToFit( e.Dimensions, pix);                
                pictureBox.Show(e);
                this.UpdateText(e.Image.Name);
                if (showTinyImage)
                {
                    f0t0tab parent = ((f0t0tab)this.Parent);
                    parent.RemoveImage(this.ImageKey);
                    parent.AddImage(e.Image);
                    this.ImageKey = e.Image.Name;
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Unload a picture
        /// </summary>
        /// <param name="sender"></param>
        public void UnloadPicture(object sender)
        {
            if (showTinyImage)
            {
                f0t0tab parent = ((f0t0tab)this.Parent);
                parent.RemoveImage(this.ImageKey);
                this.ImageKey = null;
                this.Text = defaultText;
            }
            if (this.IsCloned)
                this.RemoveClonedView();
            if (this.mainPicBox.LoadedPicture != null)
                this.mainPicBox.Unload();
            if (loader != null)
                loader.Clear();  
        }

        /// <summary>
        /// Update the tabtext and image
        /// </summary>
        /// <param name="picturename">name of the picture</param>
        private void UpdateText(string picturename)
        {
            ImageObject clonePic = this.clonePicBox.LoadedPicture;
            FileInfo fi = new FileInfo(picturename);
            string tabText = fi.Name;
            string tooltip = fi.FullName;
            if(IsCloned && clonePic != null)
            {
                FileInfo fi2 = new FileInfo(clonePic.Name);
                tabText += " | " + fi2.Name;
                tooltip += "\n vs. " + fi2.FullName;
            }
            if (tabText.Length > textLength)
            {
                tabText = tabText.Substring(0, TextLength - 3);
                tabText += "...";
            }
            this.Text = tabText;
            this.ToolTipText = tooltip;
        }
        #endregion

        #region PictureNavigation...
        /// <summary>
        /// Show the next picture.
        /// </summary>
        public void ShowNextPicture()
        {
            try
            {
                loader.GetNextImage();
                this.DisplayPicture(this.mainPicBox, new DisplayPictureEventArgs(loader.CurrentImage));
                fotobox.UpdateAll();
            }
            catch { }
        }

        /// <summary>
        /// Show previous picture.
        /// </summary>
        public void ShowPreviousPicture()
        {
            try
            {
                loader.GetPreviousImage();
                this.DisplayPicture(this.mainPicBox, new DisplayPictureEventArgs(loader.CurrentImage));
                fotobox.UpdateAll();
            }
            catch { }
        }

        /// <summary>
        /// Invoke navigation on an object with a given action (navigation action)
        /// </summary>
        /// <param name="a"></param>
        public void InvokeNavigation(NavigationAction a)
        {
            this.mainPicBox.InvokeNavigation(a);
        }

        /// <summary>
        /// Uninvoke navigation on an object with a given action (navigation action)
        /// </summary>
        /// <param name="a"></param>
        public void UnInvokeNavigation(NavigationAction a)
        {
            this.mainPicBox.UninvokeNavigation(a);
        }
        #endregion

        #region cloning
        public void LoadClone(string picture)
        {
            try
            {
                ImageObject image = new ImageObject(new Picture(picture));
                if(DisplayPicture != null)
                    DisplayPicture(this.clonePicBox, new DisplayPictureEventArgs(image));
                if (this.CloneOpened != null)
                    CloneOpened(this, new CloningEventArgs(loader.CurrentImage.Name, picture));
            }
            catch { }
        }


        private void InitClone(object sender, CloningEventArgs e)
        {
            f0t0page page = sender as f0t0page;
            if (page != null)
            {
                //Thread.Sleep(200); //wait a few millsecs for stuff.
                this.splitContainer.SplitterDistance = this.splitContainer.Parent.Width;
                page.IsCloned = true;
                page.fotobox.UpdateAll();
                page.UpdateText(e.MainPictureName);
                page.Retract();
            }
        }


        private void CloseClone(object sender, CloningEventArgs e)
        {
            f0t0page page = sender as f0t0page;
            if (page != null)
            {
                page.ReAssemble();
                page.IsCloned = false;
                page.UpdateText(e.MainPictureName);
                page.fotobox.UpdateAll();
            }
        }

        /// <summary>
        /// Cause a cloned view to be created on the current tab page.
        /// </summary>
        public void CreateClonedView()
        {
            this.clonePicBox.InitBox();
        }

        /// <summary>
        /// Cause the cloned view on the current tab page to be closed.
        /// </summary>
        public void RemoveClonedView()
        {
            if(this.CloneClosed != null)
                this.CloneClosed(this, new CloningEventArgs(this.mainPicBox.LoadedPicture.Name,
                    this.clonePicBox.LoadedPicture.Name));
            this.clonePicBox.Unload();
        }

        /// <summary>
        /// Move the splitter towards the midpoint of the screen
        /// </summary>
        private void Retract()
        {
            int location = splitContainer.SplitterDistance;
            int increment = splitContainer.SplitterIncrement;
            this.clonePicBox.SuspendLayout();
            this.mainPicBox.SuspendLayout();
            int midwidth = (splitContainer.Orientation == Orientation.Vertical) ? splitContainer.Width / 2 : splitContainer.Height / 2;
            while (Math.Abs((location - midwidth)) > increment)
            {
                this.splitContainer.SplitterDistance -= ((increment)*(location - midwidth)) / Math.Abs((midwidth - location));
                location = this.splitContainer.SplitterDistance;
            }
            this.clonePicBox.ResumeLayout(true);
            this.mainPicBox.ResumeLayout(true);
        }

        /// <summary>
        /// Move the splitter to the end of the screen.
        /// </summary>
        private void ReAssemble()
        {
            this.clonePicBox.SuspendLayout();
            this.mainPicBox.SuspendLayout();
            int location = splitContainer.SplitterDistance;
            int increment = splitContainer.SplitterIncrement;
            int width = (splitContainer.Orientation == Orientation.Vertical) ? splitContainer.Width : splitContainer.Height;
            while (Math.Abs((width - location)) > increment)
            {
                this.splitContainer.SplitterDistance += increment;
                location = this.splitContainer.SplitterDistance;
            }
            this.clonePicBox.ResumeLayout(true);
            this.mainPicBox.ResumeLayout(true);
        }

        public void ChangeOrientation()
        {
            splitContainer.Orientation = (Orientation.Vertical == splitContainer.Orientation) 
                ? Orientation.Horizontal : Orientation.Vertical;
        }

        #endregion

        /// <summary>
        /// Set the pages main control
        /// </summary>
        /// <param name="box"></param>
        public void SetFotoBox(f0t0Box box)
        {
            this.fotobox = box;
        }

        void f0t0page_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        #region Picture Activities...
        void mainPicBox_NavigationInvoked(object sender, NavigationActionArgs e)
        {
            if (e != null)
            {
                switch (e.Action)
                {
                    case NavigationAction.MoveBackward:
                        this.ShowPreviousPicture();
                        break;
                    case NavigationAction.MoveForward:
                        this.ShowNextPicture();
                        break;
                }
            }
        }

        void PictureBox_PictureRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            //throw new System.Exception("The method or operation is not implemented.");
        }

        void PictureBox_PictureDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            f0t0Map pictureBox = sender as f0t0Map;
            if (pictureBox != null)
            {
                string next = e.FullPath;
                try
                {
                    File.Delete(e.FullPath);
                    if (pictureBox.CanNavigate)
                    {
                        loader.Remove(new Picture(e.FullPath));
                        pictureBox.Image = this.loader.GetNextImage();
                        next = loader.CurrentImage.Name;
                    }
                    pictureBox.Unload();
                    if (next.ToLower().CompareTo(e.FullPath.ToLower()) != 0)
                    {
                        this.Load(next);
                    }
                }
                catch (System.IO.IOException)
                {
                    if (pictureBox.CanNavigate)
                        this.Load(next);
                    else
                        this.LoadClone(next);
                    DialogResult result = MessageBox.Show("Could not delete: " + e.FullPath + Environment.NewLine +
                            "Check to make sure the path is not write-protected.", "Delete Failed...",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Could not load next file", "Load fail");
                }
            }
        }
        #endregion

        #region Properties...
        //Properties
        public f0t0Box FotoBox
        {
            get { return this.fotobox; }
            internal set { this.fotobox = value;}
        }

        /// <summary>
        /// Gets or sets a property that indicates if the current tab can be closed
        /// </summary>
        public Boolean CanClose
        {
            get { return canClose; }
            set { canClose = value; }
        }

        /// <summary>
        /// Gets a property that indicates if the current tab is cloned
        /// </summary>
        public Boolean IsCloned
        {
            get { return (!this.splitContainer.Panel2Collapsed && CanClone); }
            private set { this.splitContainer.Panel2Collapsed = !value; }
        }

        /// <summary>
        /// Gets the property that shows if the current tab can clone an image.
        /// </summary>
        public Boolean CanClone
        {
            get { return (this.loader.CurrentImage != null);}
        }

        /// <summary>
        /// Gets the property that shows if the current tab is active.
        /// Activity is determined by if an image is being shown.
        /// </summary>
        public Boolean IsActive
        {
            get { return (this.loader.CurrentImage != null); }
        }

        /// <summary>
        /// Gets or sets the length of the text shown on the tabpage.
        /// </summary>
        public static int TextLength
        {
            get { return textLength; }
            set { textLength = value; }
        }

        /// <summary>
        /// Gets or sets whether the form should show a small
        /// image of the current image
        /// on its tab.
        /// </summary>
        public static bool ShowTinyImage
        {
            get { return showTinyImage; }
            set { showTinyImage = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates the splitter orientation.
        /// </summary>
        public Orientation SplitOrientation
        {
            get { return this.splitContainer.Orientation; }
            set { this.splitContainer.Orientation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int HashSize
        {
            get { return hashSize; }
            set { hashSize = value; }
        }

        public static bool CloneCanNavigate
        {
            get { return canNavigateClone; }
            set { canNavigateClone = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CloneNavigation
        {
            set
            {
                this.clonePicBox.CanNavigate = value;
                canNavigateClone = value;
            }
            get
            {
                return this.clonePicBox.CanNavigate;
            }
        }

        #endregion
    }
}