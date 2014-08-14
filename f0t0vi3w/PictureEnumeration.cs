/*
 * Created by SharpDevelop.
 * User: samuel
 * Date: 3/22/2007
 * Time: 7:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using f0t0vi3w3r;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using f0t0vi3w3r.Tools;
using Microsoft.Win32.SafeHandles;

namespace f0t0vi3w3r.Page
{
	/// <summary>
	/// This class handles all picture maneuvers
	/// </summary>
	public class Picture
	{
		public System.String Name = null;
		
		public Picture(System.String pictureName)
		{
		    Name = pictureName;
		}

        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }

        public PictureEnumerator GetEnumerator()
        {
            return new PictureEnumerator(this);
        }

        public override string ToString()
        {
            return Name;
        }

		/// <summary>
		/// Enumerator class.
		/// </summary>
		public class PictureEnumerator : System.Collections.IEnumerator
		{
            private Picture picture;
		    private DirectoryInfo _Dir = null;
		    private List<Picture> _Pictures = null;
		    private System.Int32 _FileIndex = -1;

		    public PictureEnumerator( Picture pics )
		    {
		        picture = pics;
                FileInfo fi = new FileInfo(pics.Name);
                _Dir = fi.Directory;
                GetFiles();
		    }

            private void GetFiles()
            {
                string[] extensions = ExtensionHandler.GetSearchOptionFormat();
                if ((_Pictures == null || _Pictures.Count == 0) && _Dir != null)
                {
                    _Pictures = new List<Picture>();
                    foreach (string ext in extensions)
                    {
                        FileInfo[] files = _Dir.GetFiles(ext);
                        foreach (FileInfo file in files)
                        {
                            if(file.Exists)
                                _Pictures.Add(new Picture(file.FullName));
                        }
                    }
                }
                
            }

		    public bool MoveNext()
		    {
		        _FileIndex += 1;
		        
		        if( _Pictures != null || _Pictures.Count != 0 )
		        {

                    if (_FileIndex >= _Pictures.Count)
                    {
                        _FileIndex = 0;
                    }
                    if (_FileIndex >= _Pictures.Count)
	                {
	                    return false;
	                }
		        }
		        
		        return true;
		    }

            public bool MoveNext(int times)
            {
                while (times > 0)
                {
                    MoveNext();
                    times--;
                }
                return (times == 0);
            }
		    
		    public bool MovePrevious()
		    {
		        _FileIndex -= 1;

                if (_Pictures != null || _Pictures.Count != 0)
    		    {
    		        if( _FileIndex < 0 )
                    {
                        _FileIndex = _Pictures.Count - 1;
                    }
                    if( _FileIndex < 0 )
                    {
                        return false;
                    }
               }     
    		        return true;
		    }

            public bool MovePrevious(int times)
            {
                while (times > 0)
                {
                    MovePrevious();
                    times--;
                }
                return (times == 0);
            }
		    
		    public System.Object Current
		    {
		        get
		        {
                    if (_FileIndex < 0 || _FileIndex >= _Pictures.Count)
	                {
	                    return null;
	                }
	                return _Pictures[_FileIndex];
		        }
		    }

            public System.Object[] this[int extent]
            {
                get{
                    int index = _FileIndex;
                    List<Object> items = new List<Object>();
                    
                    for (int i = extent; i > 0; i++)
                    {
                        MovePrevious();
                        if(!items.Contains(Current))
                            items.Add(Current);
                    }
                    _FileIndex = index;
                    for (int i = 0; i < extent; i++)
                    {
                        MoveNext();
                        if (!items.Contains(Current))
                            items.Add(Current);
                    }
                    return items.ToArray();
                }
            }

            public void SetIndex(Picture picture)
            {
                if (picture == null) return;
                for (int i = 0; i < _Pictures.Count; i++)
                {
                    if (_Pictures[i].Name.ToLower().CompareTo(picture.Name.ToLower()) == 0)
                    {
                        _FileIndex = i;
                        break;
                    }
                }
            }

            public void SetIndex(Picture picture, int ahead)
            {
                int i = ahead;
                SetIndex(picture);
                while (i > 0 && MovePrevious()) { i--; };
            }

            /// <summary>
            /// Get a picture at a certain index
            /// </summary>
            /// <param name="picture">the picture to set the index on</param>
            /// <param name="ahead">the number of indices</param>
            /// <returns>picture</returns>
            public Picture GetIndex(Picture picture, int ahead)
            {
                int i = _FileIndex;
                SetIndex(picture, ahead);
                Picture pict = (Picture)Current;
                _FileIndex = i;
                return pict;
            }

            /// <summary>
            /// Size of the list.
            /// </summary>
            public int Size
            {
                get { return _Pictures.Count; }
            }

            /// <summary>
            /// Reset
            /// </summary>
            public void Reset()
		    {
		        _FileIndex = -1;
		        _Dir = null;
		        _Pictures = null; 
		    }
		}
	}

    /// <summary>
    /// Image
    /// </summary>
    public class ImageObject : IDisposable
    {
        private System.Drawing.Image image;
        private System.String name;

        public const int CACHE_LIMIT = 20000;
        FileStream fs;
        public ImageObject(Picture picture)
        {
            FileInfo fi = new FileInfo(picture.Name);
            this.name = picture.Name;
            //FileStream stream = new FileStream(fi.Name, FileMode.Open);
            //SafeFileHandle fileHandle = new SafeFileHandle(stream.SafeFileHandle.DangerousGetHandle(), false);
            //if (ValidSize(fi))
            fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete | FileShare.ReadWrite);
            this.image = System.Drawing.Image.FromStream(fs, true, true);
        }

        public string Name
        {
            get { return name; }
        }


        public System.Drawing.Image Image
        {
            get { return image; }
            internal set { image = value; }
        }


        public override int GetHashCode()
        {
            return name.ToLower().GetHashCode();
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }


        private bool ValidSize(FileInfo fileinfo)
        {
            if (fileinfo.Length / (1024 * 1024) <= CACHE_LIMIT)
                return true;
            return false;
        }

        /// <summary>
        /// Reloads the image it is holding
        /// </summary>
        public void Reload()
        {
            FileInfo fi = new FileInfo(this.Name);
            //if (ValidSize(fi))
                this.image = System.Drawing.Image.FromFile(fi.FullName, true);
        }

        public void Dispose()
        {
            this.image.Dispose();
            this.fs.Close();
        }

        public bool Valid
        {
            get
            {
                return (this.image != null);
            }
        }
    }
}
