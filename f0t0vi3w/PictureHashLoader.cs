using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using f0t0vi3w3r.Page;
using System.Security.Cryptography;
using System.Threading;
using System.Drawing;
using f0t0vi3w3r;
using System.IO;

namespace f0t0vi3w3r.Page.Loader
{
    class PictureHashLoader : Hash, IF0t0Loader
    {
        private int capacity;
        ManualResetEvent loadEvent;
        public event EventHandler LoadCompleted;
        //const int LIMIT = 2;

        private Picture currentImage;
        //private Dictionary<long, int> counter;
        private Picture.PictureEnumerator pictureEnumerator;

        public PictureHashLoader(int capacity) : base(capacity)
        {
            this.capacity = capacity;
            //this.counter = new Dictionary<long,int>(capacity);
        }

        #region Previous...
        /*
        private void Add(long index, ImageObject value)
        {
            if (!counter.ContainsKey(index))
            {
                int maxIndex = 0, max = 0;
                
                Dictionary<long, int> counts = new Dictionary<long, int>(counter);
                {
                    foreach (KeyValuePair<long, int> o in counts)
                    {
                        counts[o.Key] = counts[o.Key] + 1;
                        if (counts[o.Key] > max)
                        {
                            maxIndex = o.Value;
                            max = counts[o.Key];
                        }
                    }
                }
                
                if (counter.Count == capacity)
                {
                    base.Remove(maxIndex);
                    counter.Remove(maxIndex);
                }
                else
                {
                    base.Add(index, value);
                    counter.Add(index, 0);
                }
                
            }
            else
                base.Add(index, value);
        }
        */

        /*
        private ImageObject this[Picture picture]
        {
            get
            {
                long index = this.GetHash(picture);
                if (this.Contains(index))
                {
                    return base[index] as ImageObject;
                }
                else
                {
                    this[picture] = new ImageObject(picture);
                    if (this.Contains(index))
                        return base[index] as ImageObject;
                }
                return null;
            }
            set
            {
                long index = this.GetHash(picture);
                if (!this.Contains(index))
                {
                    this.Add(index, value);
                }
            }
        }
        */
        #endregion

        /// <summary>
        /// Load a given picture.
        /// </summary>
        /// <param name="picture">the picture : string</param>
        public void Load(object pictureArgs)
        {
            loadEvent = (pictureArgs as object[])[1] as ManualResetEvent;
            string picToDisplay = (pictureArgs as object[])[0] as string;
            if (picToDisplay != null || picToDisplay != string.Empty)
            {
                //get the first picture and move on.
                currentImage = new Picture(picToDisplay);

                //leave the rest of the loading to this guy.
                ThreadPool.QueueUserWorkItem(LoadPictures, picToDisplay);

                if (LoadCompleted != null)
                    LoadCompleted(this, EventArgs.Empty);

                WaitHandle.WaitAll(new WaitHandle[] { loadEvent });
            }
        }

        /// <summary>
        /// Load all pictures in the location that contains a given picture.
        /// </summary>
        /// <param name="pic">a picture location</param>
        private void LoadPictures(object pic)
        {
            this.Clear();
            Picture picture = new Picture((string)pic);
            pictureEnumerator = picture.GetEnumerator();
            this.pictureEnumerator.SetIndex(picture);
            SetUp();
            loadEvent.Set();
        }

        public new void Clear()
        {
            if (CurrentImage != null)
                CurrentImage.Dispose();
            this.pictureEnumerator = null;
            base.Clear();
            this.currentImage = null;
        }

        /// <summary>
        /// Setup the loader for viewing.
        /// </summary>
        private void SetUp()
        {
            int extent = capacity / 2;
            currentImage = pictureEnumerator.Current as Picture;
            lock (this)
            {
                pictureEnumerator.MoveNext(extent);
                for (int i = 0; i < capacity; i++)
                {
                    pictureEnumerator.MovePrevious();
                    ImageObject io = new ImageObject(pictureEnumerator.Current as Picture);
                    if (io.Valid)
                    {
                        this[pictureEnumerator.Current as Picture] = io;
                    }
                }

                #region commentedOut...
                /*
                for (int i = 0; i < extent; i++)
                {
                    pictureEnumerator.MovePrevious();
                    ImageObject io = new ImageObject(pictureEnumerator.Current as Picture);
                    if (io.Valid)
                    {
                        this[pictureEnumerator.Current as Picture] = io;
                    }
                }
                pictureEnumerator.SetIndex(currentImage);
                for (int i = 0; i < extent; i++)
                {
                    pictureEnumerator.MoveNext();
                    ImageObject io = new ImageObject(pictureEnumerator.Current as Picture);
                    if (io.Valid)
                    {
                        this[pictureEnumerator.Current as Picture] = io;
                    }
                }
                pictureEnumerator.SetIndex(currentImage);
                
                ImageObject cur_io = new ImageObject(pictureEnumerator.Current as Picture);
                if (cur_io.Valid)
                {
                    this[pictureEnumerator.Current as Picture] = cur_io;
                }
                */
                #endregion
            }
        }

        /// <summary>
        /// Get the next image in the folder.
        /// </summary>
        /// <returns></returns>
        public Image GetNextImage()
        {
            currentImage = pictureEnumerator.Current as Picture;
            ImageObject current = this[currentImage] as ImageObject;
            pictureEnumerator.MoveNext();
            //SetUp();
            if (current != null)
            {
                return current.Image;
            }
            if (this[currentImage] != null)
                return (this[currentImage] as ImageObject).Image;
            return Image.FromStream(new FileStream(currentImage.Name, FileMode.Open,
                FileAccess.Read, FileShare.Delete | FileShare.Read));
        }

        /// <summary>
        /// Get the previous image viewed.
        /// </summary>
        /// <returns></returns>
        public Image GetPreviousImage()
        {
            pictureEnumerator.MovePrevious(2);
            ImageObject current = this[currentImage = pictureEnumerator.Current as Picture] as ImageObject;
            pictureEnumerator.MoveNext();
            //SetUp();
            if (current != null)
                return current.Image;
            if (this[pictureEnumerator.Current as Picture] != null)
                return (this[pictureEnumerator.Current as Picture] as ImageObject).Image;
            return Image.FromStream(new FileStream(pictureEnumerator.Current.ToString(), FileMode.Open,
                FileAccess.Read, FileShare.Delete | FileShare.Read));
        }

        /// <summary>
        /// Pictures in the same folder as current picture including itself.
        /// </summary>
        public Picture.PictureEnumerator Fotos
        {
            get { return this.pictureEnumerator; }
        }

        /// <summary>
        /// Get the currently loaded Image.
        /// </summary>
        public ImageObject CurrentImage
        {
            get
            {
                if (currentImage != null)
                {
                    ImageObject current = this[currentImage] as ImageObject;
                    if (current != null)
                        return current;
                    return new ImageObject(pictureEnumerator.Current as Picture);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the capacity of the hashloader
        /// </summary>
        public int HashSize
        {
            get { return this.capacity; }
        }
    }
}
