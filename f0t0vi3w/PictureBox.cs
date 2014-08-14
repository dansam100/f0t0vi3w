using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using f0t0vi3w3r.Tools;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace f0t0vi3w3r.Page
{
    public delegate void LoadPictureHandler(string picture);
    public delegate void ShowPictureHandler(PictureBox.ShowPictureEventArgs e);
    public delegate void UnloadPictureEventHandler(object sender);

    public enum MouseWheelState : int
    {
        Decrement = -1,
        Increment = 1
    }

    public enum ViewMode : int
    {
        Normal,
        ZoomIn = 1,
        ZoomOut = 2,
    }

    public partial class PictureBox : System.Windows.Forms.PictureBox, IPictureBox
    {
        public static Color DefaultColor = System.Drawing.SystemColors.ControlDarkDark;
        private LoadPictureHandler loadPicture;
        public event UnloadPictureEventHandler PictureUnloaded;
        private OpenFileDialog openDialog;
        private ImageObject loadedPicture;
        public ViewMode viewmode;
        private const int DELTA = 120, WIDTH_DECREMENT = 20, HEIGHT_DECREMENT = 20;
        private const int MAXIMUM_HEIGHT = 10000, MAXIMUM_WIDTH = 10000;

        public delegate void UnloadDelegate();
        private ShowPictureHandler ShowPictureHandler;

        private Int32 FrameIndex = 0, FrameCount = 1;
        private FrameDimension frameDimension = null;
        private Rectangle pictureSize;

        private Thread frameCounterThread;

        public PictureBox()
        {
            InitializeComponents();
            //
            //PictureBox
            //
            this.viewmode = ViewMode.Normal;
            this.BackColor = DefaultColor;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Location = new System.Drawing.Point(0, 0);
            //this.ContextMenuStrip = contextMenu;
            this.ContextMenu = contextMenu;
            this.Name = "PicBox";
            this.Size = new System.Drawing.Size(87, 30);
            this.TabIndex = 0;
            this.TabStop = false;
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(PicBox_DragDrop);
            this.ShowPictureHandler = new ShowPictureHandler(ShowPictureFrame);
            this.MouseWheel += new MouseEventHandler(PictureBox_MouseWheel);
            this.MouseEnter += new EventHandler(PictureBox_MouseEnter);
            this.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);
            this.ContextMenu.Collapse += new EventHandler(ContextMenu_Collapse);
        }

        /// <summary>
        /// Event: Occurs when the mouse enters this picturebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        /// <summary>
        /// Event: Occurs when the mousewheel is rotated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheelState ms = (e.Delta < 0) ? MouseWheelState.Decrement : MouseWheelState.Increment;

            try
            {
                if (this.loadedPicture != null)
                {
                    int amount = Math.Abs(e.Delta) / DELTA;
                    int increment = (int)ms;

                    int width = pictureSize.Width;
                    int height = pictureSize.Height;

                    //decrement the size and ensure it is of a good size before displaying it.
                    while (amount > 0)
                    {
                        if (e.Clicks > 0)
                        {
                            increment *= 2;
                        }
                        width += increment * WIDTH_DECREMENT;
                        height += increment * HEIGHT_DECREMENT;
                        switch (ms)
                        {
                            case MouseWheelState.Decrement:
                                if (width <= WIDTH_DECREMENT || height <= HEIGHT_DECREMENT)
                                    return;
                                break;
                            case MouseWheelState.Increment:
                                if (width >= MAXIMUM_WIDTH || height > MAXIMUM_HEIGHT)
                                    return;
                                break;
                        }
                        //this.loadedPicture.Image = this.Image;
                        this.Image = null;
                        pictureSize = new Rectangle(pictureSize.X, pictureSize.Y, width, height);
                        this.Show(new f0t0page.DisplayPictureEventArgs(this.loadedPicture, pictureSize));
                        amount -= 1;
                    }
                }
            }
            catch { }
        }

        
        /// <summary>
        /// Event: Occurs when an item is dragged into a the picturebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            this.BackColor = Color.DodgerBlue;
            drgevent.Effect = DragDropEffects.All;
        }


        /// <summary>
        /// Drag drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_DragDrop(object sender, DragEventArgs e)
        {
            string pic = string.Empty;
            try
            {
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.BackColor = PictureBox.DefaultColor;

                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        e.Effect = DragDropEffects.Link;
                        pic = (System.String)((System.Array)e.Data.GetData(
                            DataFormats.FileDrop)).GetValue(0);
                    }
                    else if (e.Data.GetDataPresent(typeof(string)))
                    {
                        e.Effect = DragDropEffects.Link;
                        pic = e.Data.GetData(typeof(string)) as string;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    
                    if (this.loadedPicture != null)
                    {
                        if (pic != null && pic.CompareTo(this.loadedPicture.Name) != 0)
                            this.LoadNew(pic);
                    }
                    else this.LoadNew(pic);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load image: " + pic, "ERROR!");
            }
        }


        /// <summary>
        /// Event: Occurs when an image is dragged out of this control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnDragLeave(EventArgs e)
        {
            this.BackColor = DefaultColor;
            this.DoDragDrop(loadedPicture.Name, DragDropEffects.All);
        }


        /// <summary>
        /// Loads a given picture
        /// </summary>
        /// <param name="picture">the path to the given picture</param>
        public void LoadNew(string picture)
        {
            if (loadedPicture != null && ImageAnimator.CanAnimate(loadedPicture.Image))
                ImageAnimator.StopAnimate(LoadedPicture.Image, null);

            if (LoadPicture != null)
                LoadPicture(picture);
        }

        /// <summary>
        /// Initializes an open of a new picture by showing the dialog box
        /// </summary>
        public void InitBox()
        {
            //
            //openDialog
            //
            this.openDialog = new OpenFileDialog();
            this.openDialog.Filter = ExtensionHandler.GetDialogFormat();
            if (this.openDialog.ShowDialog() == DialogResult.OK)
            {
                LoadNew(this.openDialog.FileName);
            }
        }

        /// <summary>
        /// Shows a given image with specifications
        /// </summary>
        /// <param name="e"></param>
        public void Show(f0t0page.DisplayPictureEventArgs e)
        {
            LoadedPicture = e.Image;
            pictureSize = e.Dimensions;
            if (LoadedPicture != null)
            {
                frameDimension = new FrameDimension(LoadedPicture.Image.FrameDimensionsList[0]);
                FrameCount = LoadedPicture.Image.GetFrameCount(frameDimension);
                FrameIndex = 0;
                if (ImageAnimator.CanAnimate(LoadedPicture.Image))
                {
                    #region framecounterMethod...
                    
                    if (frameCounterThread != null && frameCounterThread.IsAlive)
                    {
                        frameCounterThread.Abort();
                    }
                    ParameterizedThreadStart starter = new ParameterizedThreadStart(ShowFrames);
                    frameCounterThread = new Thread(starter);
                    frameCounterThread.IsBackground = true;
                    frameCounterThread.Start(e);
                    #endregion
                    #region frameupdatemethod...
                    /*
                    ParameterizedThreadStart starter = new ParameterizedThreadStart(ShowAnimation);
                    if (frameCounterThread != null && frameCounterThread.IsAlive)
                    {
                        frameCounterThread.Abort();
                    }
                    frameCounterThread = new Thread(starter);
                    frameCounterThread.IsBackground = true;
                    frameCounterThread.Start(e);
                    */
                    #endregion

                    //ShowAnimation(e);
                    //e = null;
                }
                else
                {
                    ShowPictureFrame(new ShowPictureEventArgs(LoadedPicture.Image,
                        //FrameIndex, frameDimension, e.Dimensions));     //just display crappy image.
                        FrameIndex, frameDimension, f0t0page.ScaleToFit(e.Dimensions, e.Image.Image))); //scale image properly.
                }
            }
            this.EnableAllContextItems();
        }

        /// <summary>
        /// Show animation within picture arguments
        /// </summary>
        /// <param name="pictureArgs"></param>
        private void ShowAnimation(object pictureArgs)
        {
            f0t0page.DisplayPictureEventArgs e = pictureArgs as f0t0page.DisplayPictureEventArgs;
            if (e != null)
            {
                Image i = e.Image.Image;
                this.Image = i;
                ImageAnimator.Animate(i, null);
            }
        }


        /*
        private void ShowAnimation(object pictureArgs)
        {
            f0t0page.DisplayPictureEventArgs e = pictureArgs as f0t0page.DisplayPictureEventArgs;
            if (e != null)
            {
                lock(e.Image)
                {
                    Image i = e.Image.Image;
                    while (true)
                    {
                        this.Image = new Bitmap(i, e.Dimensions.Size);
                        ImageAnimator.UpdateFrames(i);
                        this.Invalidate();
                    }
                }
            }
        }
        */

        /// <summary>
        /// Show a particular image frame.
        /// </summary>
        /// <param name="e">the image and frame details</param>
        private void ShowPictureFrame(ShowPictureEventArgs e)
        {
            if(e.Image != null && e.FrameDimension != null)
            {
                try
                {
                    Image clone = e.Image;
                    clone.SelectActiveFrame(e.FrameDimension, FrameIndex);
                    this.Image = new Bitmap(clone, e.Dimensions.Size);
                    this.OnSizeChanged(null);
                }
                catch (Exception ex)
                {
                    //this.Image = new Bitmap(e.Image, e.Dimensions.Size);
                }
            }
        }

        #region obsolete...
        /// <summary>
        /// Show all the frames of an image.
        /// </summary>
        /// <param name="pictureArgs">the image details</param>
        private void ShowFrames(object pictureArgs)
        {
            f0t0page.DisplayPictureEventArgs e = pictureArgs as f0t0page.DisplayPictureEventArgs;
            if (e != null)
            {
                try
                {
                    //Image i = e.Image.Image.Clone() as Image;
                    Image i = e.Image.Image;
                    PropertyItem delayTime = i.GetPropertyItem(0x5100); //get frame delay
                    int delay = (delayTime.Value[0] + delayTime.Value[1] * 256) * 10;
                    for (int j = FrameIndex; j <= FrameCount; j++)
                    {
                        j = (j.CompareTo(FrameCount) == 0) ? 0 : j;
                        FrameIndex = j;
                        if (ShowPictureHandler != null)
                            ShowPictureHandler(new ShowPictureEventArgs(i, FrameIndex, frameDimension, e.Dimensions));
                        Thread.Sleep(delay);
                    }
                }
                catch { /*ShowFrames(pictureArgs);*/ }
            }
        }
        #endregion


        #region Properties...
        /// <summary>
        /// A load picture handler
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
                 DefaultValue(false), Category("EventHandler"),
                 Description("Picture loading handler")]
        public LoadPictureHandler LoadPicture
        {
            get { return this.loadPicture; }
            set { this.loadPicture = value; }
        }

        /// <summary>
        /// The loaded picture details
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
                 DefaultValue(false), Category("Property"),
                 Description("Loaded Picture")]
        public ImageObject LoadedPicture
        {
            get { return this.loadedPicture; }
            set { this.loadedPicture = value; }
        }
        #endregion


        /// <summary>
        /// Event args for showing pictures by frame.
        /// </summary>
        public class ShowPictureEventArgs : EventArgs
        {
            private Image image;
            private int frameCount;
            private FrameDimension frameDimension;
            private Rectangle dimensions;
            public ShowPictureEventArgs(Image image, int frameIndex, FrameDimension dimension, Rectangle dimensions)
            {
                this.image = image;
                this.frameCount = frameIndex;
                this.frameDimension = dimension;
                this.dimensions = dimensions;
            }

            public int FrameIndex
            {
                get { return this.frameCount; }
            }

            public Image Image
            {
                get { return this.image; }
            }

            public FrameDimension FrameDimension
            {
                get { return this.frameDimension; }
            }

            public Rectangle Dimensions
            {
                get { return this.dimensions; }
            }

        }
    }
}
