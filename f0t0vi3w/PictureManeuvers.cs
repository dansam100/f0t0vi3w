/*
 * Created by SharpDevelop.
 * User: samuel
 * Date: 3/23/2007
 * Time: 1:31 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using f0t0vi3w3r.Tools;

namespace f0t0vi3w3r
{
	/// <summary>
	/// Handles user clicks
	/// </summary>
    public partial class f0t0Box
	{	    
        /// <summary>
	    /// get next picture
	    /// </summary>
	    /// <param name="sender">next button</param>
	    /// <param name="b"></param>
        protected void nextButton_Click(object sender, EventArgs b)
		{
            CurrentPage.ShowNextPicture();		    
		}


        /// <summary>
        /// get previous picture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="c"></param>
        protected void prevButton_Click(object sender, EventArgs c)
		{
            CurrentPage.ShowPreviousPicture();
		}
    }
}
