using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using f0t0vi3w3r.Page;
using System.Security.Cryptography;

namespace f0t0vi3w3r.Page.Loader
{
    class Hash : Dictionary<int, Hashtable>
    {
        int capacity;
        public Hash(int capacity) : base(capacity)
        {
            this.capacity = capacity;
        }

        
        protected long GetHash(Picture picture)
        {
            long result = 0L;
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = md5.ComputeHash(encoding.GetBytes(picture.Name));
            foreach (byte b in stream)
            {
                result += b;
            }
            return result;
        }
        
        /*
        protected long GetHash(object picture)
        {
            return picture.GetHashCode();
        }
        */
       
        public void Remove(object picture)
        {
            Picture pict = picture as Picture;
            if (pict == null)
                pict = new Picture((picture as ImageObject).Name);

            long hashindex = this.GetHash(pict);
            int index = (int)hashindex % capacity;
            if (this.ContainsKey(index))
                if (base[index].Contains(hashindex)){
                    (base[index][hashindex] as ImageObject).Dispose();
                    base.Remove(index);
                }
        }


        protected object this[long index]
        {
            get
            {
                if (index >= 0 && index < this.Count)
                    return this[(int)index];
                return null;
            }
            set
            {
                int mainindex = (int)index % capacity;
                if (this.ContainsKey(mainindex))
                {
                    base[mainindex] = new Hashtable(1);
                    base[mainindex].Add(index, value);
                }
                else
                {
                    base.Add(mainindex, new Hashtable(1));
                    base[mainindex].Add(index, value);
                }
            }
        }

        /// <summary>
        /// Get or set a picture image object.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        protected ImageObject this[Picture picture]
        {
            get
            {
                long hashindex = this.GetHash(picture);
                int index = (int)hashindex % capacity;
                if (this.ContainsKey(index))
                {
                    if (base[index].Contains(hashindex))
                        return base[index][hashindex] as ImageObject;
                    else
                    {
                        ImageObject io = new ImageObject(picture);
                        return (io.Valid) ? (this[picture] = io) : null;
                    }
                }
                else
                {
                    ImageObject io = new ImageObject(picture);
                    return (io.Valid) ? (this[picture] = io) : null;
                }
            }
            set
            {
                long hashindex = this.GetHash(picture);
                //int index = (int)hashindex % capacity;
                this[hashindex] = value;
            }
        }
    }
}
