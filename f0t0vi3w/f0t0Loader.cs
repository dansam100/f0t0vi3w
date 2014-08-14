using System;
using System.Collections.Generic;
using System.Text;
using f0t0vi3w3r.Page;
using System.Threading;
using System.Drawing;

namespace f0t0vi3w3r.Page.Loader
{
    public class f0t0Loader : IF0t0Loader
    {
        ManualResetEvent loadEvent;
        const int LIMITS = 3;
        public event EventHandler LoadCompleted;
        
        private ImageObject currentImage;

        private Picture picture = null;
        private Picture.PictureEnumerator pictureEnumerator;
        private ImageHolder BootedPictures;

        public f0t0Loader()
        {
            this.Clear();
        }

        public void Load(object picture)
        {
            loadEvent = new ManualResetEvent(false);
            string picToDisplay = ((object[])picture)[0] as string;
            if (picToDisplay != null || picToDisplay != string.Empty)
            {
                ThreadPool.QueueUserWorkItem(LoadPictures, picToDisplay);
                WaitHandle.WaitAll(new WaitHandle[] { loadEvent });
                SetUp();
                if (LoadCompleted != null)
                    LoadCompleted(this, EventArgs.Empty);
            }
        }


        public void LoadPictures(object picture)
        {
            this.Clear();
            if (picture != null)
            {
                this.picture = new Picture((string)picture);
                pictureEnumerator = this.picture.GetEnumerator();
                loadEvent.Set();
            }
        }

        /// <summary>
        /// Set up pictures for viewing
        /// </summary>
        private void SetUp()
        {
            pictureEnumerator.SetIndex(picture, LIMITS);
            
            /*foreach (Object pix in pictureEnumerator[LIMITS])
            {
                BootedPictures.Add(new ImageHolder.ImageObject((Picture)pix));
            }*/

            Picture pict = picture;
            do
            {
                BootedPictures.Add(new ImageObject(pict));
                pict = (Picture)pictureEnumerator.Current;
            }
            while (pictureEnumerator.MoveNext() && !(BootedPictures.Contains(pict) || BootedPictures.IsFull()));
            BootedPictures.SetIndex(picture);
        }

        /// <summary>
        /// Get the next image in the list
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image GetNextImage()
        {
            currentImage = BootedPictures.Current;
            if (BootedPictures.Count > 1)
            {
                while (pictureEnumerator.MoveNext() && currentImage != null && currentImage.Equals(pictureEnumerator.Current)) ;
                BootedPictures.AddNext((Picture)pictureEnumerator.Current);
            }
            return currentImage.Image;
            //return null;
        }

        /// <summary>
        /// Get the previous image viewed.
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image GetPreviousImage()
        {
            currentImage = BootedPictures.Current;
            if (BootedPictures.Count > 1)
            {
                while (pictureEnumerator.MovePrevious() && currentImage != null && currentImage.Equals(pictureEnumerator.Current)) ;
                BootedPictures.AddNext((Picture)pictureEnumerator.Current);
            }
            return currentImage.Image;
            /*
            if (pictureEnumerator.MovePrevious())
            {
                currentImage = BootedPictures.Current;
                Picture p = new Picture(BootedPictures.Current.Name);
                BootedPictures.AddNext(pictureEnumerator.GetIndex(p, LIMITS));
                return currentImage.Image;
            }
            return null;*/
        }

        //Props
        public Picture.PictureEnumerator Fotos
        {
            get { return this.pictureEnumerator; }
        }

        /// <summary>
        /// Get the currently loaded image.
        /// </summary>
        public ImageObject CurrentImage
        {
            get { return currentImage; }
        }

        /// <summary>
        /// Clear the loader entirely.
        /// </summary>
        public void Clear()
        {
            BootedPictures = new ImageHolder(2 * LIMITS + 1);
            picture = null;
            pictureEnumerator = null;
            currentImage = null;

        }

        public void Remove(Object picture)
        {
            if (picture is ImageObject)
                BootedPictures.Remove(picture as ImageObject);
            else if(picture is Picture)
            {
                ImageObject io = new ImageObject(picture as Picture);
                BootedPictures.Remove(io);
            }
        }

        public int HashSize
        {
            get { return LIMITS * 2 + 1; }
        }
    }

    /// <summary>
    /// holds images.
    /// </summary>
    public class ImageHolder : List<ImageObject>
    {
        private int IndexF = 0, IndexB = 0, Index, capacity;
        private int maxEntries = 50;

        public ImageHolder(int capacity)
        {
            IndexF = capacity - 1;
            this.capacity = capacity - 1;
        }

        public bool Contains(Picture imagename)
        {
            foreach (ImageObject io in this)
            {
                if (io.Equals(imagename))
                    return true;
            }
            return false;
        }

        public bool IsFull()
        {
            return (capacity <= Count);
        }

        public void SetIndex(Picture picture)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Equals(picture))
                {
                    Index = i;
                    break;
                }
            }
        }

        public void AddNext(Picture imageObject)
        {
            Index++;
            Index = (Index >= Count) ? IndexB : Index;
            if (!this.Contains(imageObject))
            {
                if (!IsFull())
                {
                    this.Add(new ImageObject(imageObject));
                }
                else
                {
                    this.IndexB++; this.IndexF++;
                    if (IndexB >= capacity)
                        IndexB = 0;
                    if (IndexF >= capacity)
                        IndexF = 0;
                    else
                        this.Insert(IndexF, new ImageObject(imageObject));
                }
                CheckPurge();
            }
        }

        public void AddPrevious(Picture imageObject)
        {
            Index--;
            Index = (Index < 0) ? IndexF : Index;
            if (!this.Contains(imageObject))
            {
                {
                    this.IndexB--; this.IndexF--;
                    if (IndexB < 0)
                    {
                        IndexB = capacity - 1;
                    }
                    if (IndexF < 0)
                        IndexF = capacity - 1;
                    this.Insert(IndexB, new ImageObject(imageObject));
                }
                CheckPurge();
            }
        }

        public void CheckPurge()
        {
            if (this.Count >= maxEntries)
            {
                ImageObject[] temp = new ImageObject[capacity];
                Picture p = new Picture(this.Current.Name);
                this.CopyTo(IndexB, temp, 0, capacity);
                this.Clear();
                this.AddRange(temp);
                this.SetIndex(p);
            }
        }

        public ImageObject Current
        {
            get
            {
                if (this.Count > 0)
                    return this[Index];
                return null;
            }
        }

        public int MaxEntries
        {
            get { return this.maxEntries; }
            set { this.maxEntries = value; }
        }
    }
}
