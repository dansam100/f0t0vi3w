/*
 * Created by SharpDevelop.
 * User: samuel
 * Date: 16/02/2008
 * Time: 4:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;

namespace f0t0vi3w3r
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f0t0Box fotobox = new f0t0Box();
            fotobox.Show();
            while (fotobox.Created)
            {
                Application.DoEvents();
            }
        }
        
    }
}
