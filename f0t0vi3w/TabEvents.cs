using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace f0t0vi3w3r.Page
{
    partial class f0t0tab
    {        
        /*
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = this.Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
            this.ItemSize = new Size(0, this.Font.Height + 2);
        }
        */

        void menuItemNewTab_Click(object sender, EventArgs e)
        {
            this.CreateNewTab();
        }

        /// <summary>
        /// Event: Overrides the painting of the background
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                LinearGradientBrush br = new LinearGradientBrush(
                            this.Bounds,
                            SystemColors.ControlLightLight,
                            SystemColors.ControlLight,
                            LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(br, this.Bounds);
                br.Dispose();
            }
            else
            {
                if (this.Parent != null)
                {
                    Rectangle clientRect = e.ClipRectangle;
                    GraphicsState state = e.Graphics.Save();
                    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    try
                    {
                        e.Graphics.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                        this.InvokePaintBackground(this.Parent, e);
                        this.InvokePaint(this.Parent, e);
                    }
                    finally
                    {
                        e.Graphics.Restore(state);
                    }
                }
            }
        }


        /// <summary>
        /// Event: Overrides the onpaint event and paints the form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnPaintBackground(e);
            for (int i = 0; i < this.TabCount; i++)
            {
                this.PaintBorder(e);
                Rectangle tabArea = this.GetTabRect(i);
                Pen borderPen = new Pen(SystemColors.ControlDark);

                System.Drawing.Brush buttonBrush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        tabArea,
                        SystemColors.ControlDark,
                        SystemColors.ControlLight,
                        LinearGradientMode.Vertical);

                GraphicsPath path = new GraphicsPath();
                path.Reset();

                if (i == this.SelectedIndex)
                {
                    buttonBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        tabArea,
                        SystemColors.ControlLight,
                        SystemColors.ControlLightLight,
                        LinearGradientMode.Vertical);
                    borderPen = new Pen(new SolidBrush(Color.Gray));
                    this.PaintTab(i, path, true);
                    e.Graphics.FillPath(buttonBrush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }
                else
                {
                    this.PaintTab(i, path, false);
                    e.Graphics.FillPath(buttonBrush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }
                borderPen.Dispose();
                buttonBrush.Dispose();

                this.PaintText(e, i);
                this.PaintTabImage(e.Graphics, i);
            }
        }

        /// <summary>
        /// Draw the lines for painting for a given tab
        /// </summary>
        /// <param name="index">index of the tab</param>
        /// <param name="path">the graphics path</param>
        /// <param name="isSelected">indicates whether the tab is selected or not</param>
        private void PaintTab(int index, GraphicsPath path, bool isSelected)
        {
            Rectangle area = this.GetTabRect(index);
            if (index == 0)
            {
                path.AddLine(area.Left + 1, area.Bottom + 1, area.Left + 1, area.Top + area.Height / 2);
                path.AddLine(area.Left + 1, area.Top + area.Height / 2 - 1, area.Left + area.Height / 2, area.Top);
                path.AddLine(area.Left + area.Height / 2, area.Top, area.Right - 3, area.Top);
                path.AddLine(area.Right - 1, area.Top, area.Right - 1, area.Bottom + 1);
            }
            else
            {
                if (isSelected)
                {
                    //path.AddLine(area.Left + 1, area.Top + 5, area.Left + 4, area.Top + 2);
                    path.AddLine(area.Left, area.Top, area.Right, area.Top);
                    path.AddLine(area.Left + 8, area.Top, area.Right - 3, area.Top);
                    path.AddLine(area.Right - 1, area.Top + 2, area.Right - 1, area.Bottom + 1);
                    path.AddLine(area.Right - 1, area.Bottom + 4, area.Left + 1, area.Bottom + 4);
                }
                else
                {
                    path.AddLine(area.Left, area.Top + 6, area.Left + 4, area.Top + 2);
                    path.AddLine(area.Left + 8, area.Top, area.Right - 3, area.Top);
                    path.AddLine(area.Right - 1, area.Top + 2, area.Right - 1, area.Bottom + 1);
                    path.AddLine(area.Right - 1, area.Bottom + 1, area.Left, area.Bottom + 1);
                }
            }
        }


        /// <summary>
        /// Paint the text
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        private void PaintText(PaintEventArgs e, int index)
        {
            //Get the area where the tab text should be printed onto
            Rectangle tabArea = this.GetTabRect(index);
            
            Rectangle tabimageArea = new Rectangle(tabArea.Left + tabArea.Height / 2, tabArea.Top + 4,
                18, tabArea.Height);

            Rectangle closeImageArea = new Rectangle(tabArea.Right - 16, tabArea.Top + 2, 15, tabArea.Height);

            Rectangle textArea = new Rectangle(tabimageArea.Right, tabimageArea.Top,
                closeImageArea.Left - tabimageArea.Right, 3*tabimageArea.Height/4);

            string str = this.TabPages[index].Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Trimming = StringTrimming.EllipsisWord;

            //Draw the tab header text
            Brush brush = SystemBrushes.ControlLightLight;
            {
                Font font = this.Font.Clone() as Font;
                if (index == this.SelectedIndex)
                {
                    font = new Font(font, FontStyle.Bold);
                    brush = SystemBrushes.ControlText;
                }
                e.Graphics.DrawString(str, font, brush, textArea, stringFormat);
            }
        }

        /// <summary>
        /// Paint the border
        /// </summary>
        /// <param name="e"></param>
        private void PaintBorder(PaintEventArgs e)
        {
            Rectangle borderRectangle = this.CurrentPage.Bounds;
            borderRectangle.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Draw the images on the current tab
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="index"></param>
        private void PaintTabImage(System.Drawing.Graphics graphics, int index)
        {
            Image tabImage = null;
            if (this.TabPages[index].ImageIndex > -1 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            }
            else if (this.TabPages[index].ImageKey.Trim().Length > 0 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }
            Rectangle rect = this.GetTabRect(index);
            if (tabImage != null)
            {
                Rectangle tabimageArea = new Rectangle(rect.Left + rect.Height / 2, rect.Top + 4, 15, 15);
                graphics.DrawRectangle(new Pen(Color.Blue), tabimageArea);
                this.PaintImage(tabimageArea, graphics, tabImage);
            }
            Rectangle closeImageArea = new Rectangle(rect.Right - 16, rect.Top + 2, 13, 13);
            f0t0page page = this.TabPages[index] as f0t0page;
            if (page != null && page.CanClose)
            {
                using (Bitmap bmp = (index == this.SelectedIndex) ? 
                    new Bitmap(Properties.Resources.closebtn) : new Bitmap(Properties.Resources.disabled_close_btn))
                {
                    this.PaintImage(closeImageArea, graphics, bmp);
                    page.CloseBounds = closeImageArea;
                }
            }
        }

        /// <summary>
        /// Paints a given image into a given rectangle
        /// </summary>
        /// <param name="area"></param>
        /// <param name="graphics"></param>
        /// <param name="image"></param>
        private void PaintImage(Rectangle area, Graphics graphics, Image image)
        {
            if (image != null)
                graphics.DrawImage(image, area);
        }


        /*
        /// <summary>
        /// override to draw the close button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Bounds != RectangleF.Empty)
            {
                for (int i = 0; i < this.TabCount; i++)
                {
                    this.PerformDraw(this.TabPages[i] as f0t0page);
                }
            }
        }
        
        /// <summary>
        /// Perform the draw of a specific item
        /// </summary>
        /// <param name="f0t0page"></param>
        internal void PerformDraw(f0t0page f0t0page)
        {
            RectangleF tabTextArea = RectangleF.Empty;
            Graphics gr = this.CreateGraphics();
            gr.SmoothingMode = SmoothingMode.HighSpeed;

            int index = this.FindTabIndex(f0t0page);
            if (index >= 0)
            {
                tabTextArea = (RectangleF)this.GetTabRect(index);

                //draw smoothness over it
                LinearGradientBrush br = new LinearGradientBrush(tabTextArea, SystemColors.ControlLightLight,
                    SystemColors.Control, LinearGradientMode.Vertical);
                gr.FillRectangle(br, tabTextArea);
                br.Dispose();

                //if this is the selected index, draw a colored icon over it
                if (index == this.SelectedIndex)
                {
                    //if not active draw ,inactive close button
                    if (f0t0page.CanClose)
                    {
                        using (Bitmap bmp = new Bitmap(GetIconFromResource()))
                        {
                            gr.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                        }
                    }
                }
                //otherwise draw a disabled close button if the page is closable.
                else
                {
                    Pen pen = new Pen(new SolidBrush(Color.Gray));
                    if (f0t0page.CanClose)
                    {
                        using (Bitmap bmp = new Bitmap(Properties.Resources.close_btn_u))
                        {
                            gr.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                        }                        
                    }
                    gr.DrawRectangle(pen, this.GetTabRect(index));
                    pen.Dispose();
                }

                //write the tabtext properly.
                string str = f0t0page.Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                using (SolidBrush brush = new SolidBrush(f0t0page.ForeColor))
                {
                    //Draw the tab header text
                    gr.DrawString(str, this.Font, brush,
                    tabTextArea, stringFormat);
                }
            }
        }
        */
    }
}
