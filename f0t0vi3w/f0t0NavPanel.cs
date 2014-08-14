using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace f0t0vi3w3r.Page.Map
{
    public partial class f0t0NavPanel : Panel
    {
        private NavigationAction action;
        private Timer repeatTimer;
        private bool buttonDown = false, canNavigate = true;
        private static bool allowRepeat = true, LockOnMouseOver = false;
        private static int v_delay = 1000, v_initDelay = 1500;
        private bool hasfocus = false;
        public f0t0NavPanel()
        {
            //
            //repeatTimer
            //
            this.repeatTimer = new Timer();
            this.repeatTimer.Tick += new EventHandler(repeatTimer_Tick);
            this.repeatTimer.Enabled = false;
            //
            //this
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MouseEnter += new EventHandler(f0t0NavPanel_MouseEnter);
            this.MouseLeave += new EventHandler(f0t0NavPanel_MouseLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(f0t0NavPanel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(f0t0NavPanel_MouseUp);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(28, 398);
            this.Name = "Panel";
        }

        public void PerformInvoke()
        {
            f0t0NavPanel_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        public void EndInvoke()
        {
            f0t0NavPanel_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        protected void f0t0NavPanel_MouseLeave(object sender, EventArgs e)
        {
            f0t0NavPanel panel = sender as f0t0NavPanel;
            if (panel != null && panel.CanNavigate)
            {
                hasfocus = false;
                panel.BackColor = panel.Parent.BackColor;
                //this.BorderStyle = BorderStyle.None;
                this.Refresh();
            }
            this.Focus();
        }

        protected void f0t0NavPanel_MouseEnter(object sender, EventArgs e)
        {
            f0t0NavPanel panel = sender as f0t0NavPanel;
            if (panel != null && panel.CanNavigate)
            {
                hasfocus = true;
                //panel.BackColor = SystemColors.ButtonShadow;
                panel.BackColor = Color.DodgerBlue;
                if (LockOnMouseOver)
                    this.BorderStyle = BorderStyle.FixedSingle;
                else
                    this.BorderStyle = BorderStyle.None;

                this.Refresh();
            }
            this.Focus();
        }

        /// <summary>
        /// Event: Occurs when the control is painted
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int height = Height; int width = Width; int locationX = e.ClipRectangle.X; int locationY = e.ClipRectangle.Y;
            Point bottomLeft, bottomRight;
            int x, y;
            bool invert = (height < width);
            int midwayY = height / 2, midwayX = width / 2;
            if (action == NavigationAction.MoveForward)
            {
                bottomLeft = new Point(locationX, locationY);
                bottomRight = invert ? new Point(locationX + width, locationY) : new Point(locationX, locationY + height);
                x = invert ? (locationX + width) / 2 : locationX + width;
                y = invert ? locationY + height : (locationY + height) / 2;
            }
            else
            {
                bottomLeft = invert ? new Point(locationX, locationY + height) : new Point(locationX + width, locationY);
                bottomRight = new Point(locationX + width, locationY + height);
                x = invert ? (locationX + width) / 2 : locationX;
                y = invert ? locationY : (locationY + height) / 2;
            }

            Point midPointForward = new Point(x, y);
            Pen pen;
            if (this.hasfocus)
                pen = new Pen(new SolidBrush(Color.LightBlue), 3);
            else
                pen = new Pen(new SolidBrush(this.BackColor), 10);
            e.Graphics.DrawLine(pen, bottomRight, midPointForward);
            e.Graphics.DrawLine(pen, midPointForward, bottomLeft);
            pen.Dispose();
            e.Graphics.Dispose();
        }


        /// <summary>
        /// Event: occurs when the mouse button goes up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void f0t0NavPanel_MouseUp(object sender, MouseEventArgs e)
        {
            f0t0NavPanel panel = sender as f0t0NavPanel;

            if (panel != null && panel.CanNavigate)
            {
                panel.BorderStyle = BorderStyle.None;
                repeatTimer.Enabled = false;
                buttonDown = false;
            }
        }

        /// <summary>
        /// Event: Occurs when the mouse is down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void f0t0NavPanel_MouseDown(object sender, MouseEventArgs e)
        {
            f0t0NavPanel panel = sender as f0t0NavPanel;

            if (panel != null && panel.CanNavigate)
            {
                panel.BorderStyle = BorderStyle.Fixed3D;
                repeatTimer.Interval = v_initDelay;
                repeatTimer.Enabled = true;
                buttonDown = true;
            }
        }

        /// <summary>
        /// Event: Occurs when the timer is reached.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repeatTimer_Tick(object sender, EventArgs e)
        {
            this.repeatTimer.Interval = v_delay;
            if (buttonDown && allowRepeat)
            {
                this.OnClick(e);
            }
        }

        /// <summary>
        /// Action of a form.
        /// </summary>
        public NavigationAction Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        /// <summary>
        /// Gets or sets the value of the form that allows autorepetition of click input.
        /// </summary>
        public static bool AllowRepeat
        {
            get { return allowRepeat; }
            set { allowRepeat = value; }
        }

        public static int AutoRepeatDelay
        {
            get { return v_delay; }
            set { v_delay = value; }
        }

        public static bool LockAndScroll
        {
            get { return LockOnMouseOver; }
            set { LockOnMouseOver = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that the picture box allows navigation.
        /// </summary>
        public bool CanNavigate
        {
            get { return this.canNavigate; }
            set { this.canNavigate = value; }
        }

        /// <summary>
        /// Gets if the current navigation panel's navigation has been invoked.
        /// </summary>
        public bool IsInvoked
        {
            get { return this.buttonDown; }
        }
    }

    /// <summary>
    /// Event arguments for navigation panels
    /// </summary>
    public class NavigationActionArgs : EventArgs
    {
        private NavigationAction action;
        private EventArgs e;
        public NavigationActionArgs(NavigationAction action, EventArgs e)
        {
            this.action = action;
            this.e = e;
        }

        public NavigationAction Action
        {
            get { return this.action; }
        }

        public EventArgs MouseEventArgs
        {
            get { return this.e; }
        }
    }
}