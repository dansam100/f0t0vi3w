/*
 * Created by SharpDevelop.
 * User: samuel
 * Date: 16/02/2008
 * Time: 4:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using f0t0vi3w3r.Properties;
using f0t0vi3w3r.Tools;
using f0t0vi3w3r.Page;
using System.Runtime.InteropServices;
using System.Configuration;
using f0t0vi3w3r.Extras;
using f0t0vi3w3r.ProgramSettings;
using f0t0vi3w3r.Page.Map;

namespace f0t0vi3w3r
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class f0t0Box : Form
    {
        private bool canClone;
        private string formName = "f0t0vi3w3r";
        private delegate void UpdateFormDelegate();
        private bool repeatSlideShow = false;
        internal static Dictionary<string, string> SettingValues;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCLBUTTONDBLCLK = 0xA3;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public f0t0Box()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //this.MouseWheel += new MouseEventHandler(f0t0Box_MouseWheel);
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            this.fotopage.SetFotoBox(this);
        }

        static f0t0Box()
        {
            SettingValues = new Dictionary<string, string>();
        }

        
        void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateAll();
        }


        void menuRemoveClone_Click(object sender, System.EventArgs e)
        {
            this.CurrentPage.RemoveClonedView();
        }


        void menuItemClone_Click(object sender, System.EventArgs e)
        {
            this.CurrentPage.CreateClonedView();
        }

        void menuItemRepeat_Click(object sender, System.EventArgs e)
        {
            this.repeatSlideShow = !this.repeatSlideShow;
            this.menuItemRepeat.Checked = this.repeatSlideShow;
            //do something to the settings.
        }

        void menuChangeSplitOrientation_Click(object sender, System.EventArgs e)
        {
            CurrentPage.ChangeOrientation();
        }

        /// <summary>
        /// Event: Occurs when Close Tab is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemCloseTab_Click(object sender, System.EventArgs e)
        {
            this.tabs.Close(CurrentPage);
        }

        /// <summary>
        /// Event: Occurs when the mouse button is down on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void f0t0Box_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks < 2)
                {
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                else
                {
                    SendMessage(Handle, WM_NCLBUTTONDBLCLK, HT_CAPTION, 0);
                }
            }
        }

        /*
        void f0t0Box_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                case Keys.Down:
                    this.CurrentPage.UnInvokeNavigation(NavigationAction.MoveForward);
                    break;
                case Keys.Left:
                case Keys.Up:
                    this.CurrentPage.UnInvokeNavigation(NavigationAction.MoveBackward);
                    break;
            }
        }

        void f0t0Box_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                case Keys.Down:
                    this.CurrentPage.InvokeNavigation(NavigationAction.MoveForward);
                    break;
                case Keys.Left:
                case Keys.Up:
                    this.CurrentPage.InvokeNavigation(NavigationAction.MoveBackward);
                    break;
            }
        }
        */

        /// <summary>
        /// Event: Occurs when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void f0t0Box_Load(object sender, System.EventArgs e)
        {
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
            this.UpdateAll();
            this.LoadSettings();
            this.Focus();
        }

        /// <summary>
        /// Event: Occurs when the settings are changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.LoadSettings(e.PropertyName);
            SettingValues[e.PropertyName] = Settings.Default[e.PropertyName].ToString();
        }

        /// <summary>
        /// Load all settings
        /// </summary>
        private void LoadSettings()
        {
            foreach (SettingsProperty s in Settings.Default.Properties)
            {
                LoadSettings(s.Name);
                if(!SettingValues.ContainsKey(s.Name))
                    SettingValues.Add(s.Name, s.DefaultValue.ToString());
                else SettingValues[s.Name] = s.DefaultValue.ToString();
            }
        }

        /// <summary>
        /// Loads a setting
        /// </summary>
        /// <param name="settingName">the setting to load</param>
        private void LoadSettings(string settingName)
        {
            switch (settingName)
            {
                case SettingsPage.SHOWTINYIMAGE:
                    this.tabs.ShowTinyImages = Settings.Default.ShowTinyImage;
                    break;
                case SettingsPage.REPEATSLIDESHOW:
                    this.repeatSlideShow = Settings.Default.RepeatSlideShow;
                    this.menuItemRepeat.Checked = this.repeatSlideShow;
                    break;
                case SettingsPage.STARTMAXIMIZED:
                    this.WindowState = (Settings.Default.StartMaximized) ? FormWindowState.Maximized : FormWindowState.Normal;
                    break;
                case SettingsPage.AUTONAVIGATION:
                    this.tabs.AllowRepeat = Settings.Default.AutoNavigation;
                    break;
                case SettingsPage.AUTOREPEATDELAY:
                    this.tabs.AutoRepeatDelay = (int)Settings.Default.AutoRepeatDelay;
                    break;
                case SettingsPage.TABCHARACTERS:
                    f0t0page.TextLength = (int)Settings.Default.TabCharacters;
                    break;
                case SettingsPage.HASHSIZE:
                    f0t0page.HashSize = (int)Settings.Default.HashSize;
                    break;
                case SettingsPage.CLONEBOXNAVIGATION:
                    this.tabs.CloneCanNavigate = Settings.Default.CloneBoxNavigation;
                    break;
                case SettingsPage.DEFAULTORIENTATION:
                    f0t0tab.Orientation = Settings.Default.DefaultOrientation;
                    break;
                case SettingsPage.SHOWBORDER:
                    if (Settings.Default.ShowBorder)
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.menuShowBorder.Checked = true;
                    }
                    else
                    {
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.menuShowBorder.Checked = false;
                    }
                    break;
                case SettingsPage.SHOWMENUBAR:
                    if (Settings.Default.ShowMenuBar)
                    {
                        if (ContextMenu != null)
                        {
                            MenuItem[] menuItems = new MenuItem[ContextMenu.MenuItems.Count];
                            ContextMenu.MenuItems.CopyTo(menuItems, 0);
                            topMenu.MenuItems.AddRange(menuItems);
                        }
                        this.Menu = topMenu;
                        this.ContextMenu = null;
                        this.menuHideMenuBar.Checked = true;
                    }
                    else
                    {
                        this.Menu = null;
                        MenuItem[] menuItems = new MenuItem[topMenu.MenuItems.Count];
                        topMenu.MenuItems.CopyTo(menuItems, 0);
                        this.ContextMenu = new ContextMenu(menuItems);
                        this.menuHideMenuBar.Checked = false;
                    }
                    break;
                case SettingsPage.SLIDESHOWDELAY:
                    break;
            }
        }

        /// <summary>
        /// Update form details and menus
        /// </summary>
        public void UpdateAll()
        {
            if (!this.InvokeRequired)
            {
                this.menuItemClone.Enabled = CurrentPage.CanClone;
                this.menuChangeSplitOrientation.Enabled = CurrentPage.IsCloned;
                this.menuRemoveClone.Enabled = CurrentPage.IsCloned;
                this.menuItemCloseTab.Enabled = CurrentPage.CanClose;
                this.menuItemMinimize.Enabled = (this.WindowState != FormWindowState.Minimized);
                this.menuItemMaximize.Enabled = (this.WindowState != FormWindowState.Maximized);
                this.menuItemRestore.Enabled = (this.WindowState != FormWindowState.Normal);
                if (CurrentPage.IsActive)
                {
                    this.Text = string.Format("{0} - {1}", CurrentPage.Text.Trim(), formName);
                    this.CanClone = true;
                }
                else
                {
                    this.Text = formName;
                    this.CanClone = false;
                }
            }
            else this.Invoke(new UpdateFormDelegate(this.UpdateAll), null);
        }

        /// <summary>
        /// Event: Occurs when Hide Menu Bar is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuHideMenuBar_Click(object sender, System.EventArgs e)
        {
            if (!this.menuHideMenuBar.Checked)
            {
                MenuItem[] menuItems = new MenuItem[ContextMenu.MenuItems.Count];
                ContextMenu.MenuItems.CopyTo(menuItems, 0);
                topMenu.MenuItems.AddRange(menuItems);
                this.Menu = topMenu;
                this.ContextMenu = null;
                this.menuHideMenuBar.Checked = true;
            }
            else
            {
                this.Menu = null;
                MenuItem[] menuItems = new MenuItem[topMenu.MenuItems.Count];
                topMenu.MenuItems.CopyTo(menuItems, 0);
                this.ContextMenu = new ContextMenu(menuItems);
                this.menuHideMenuBar.Checked = false;
            }

            /*
            if (!this.menuHideMenuBar.Checked)
            {
                Properties.Settings.Default.ShowMenuBar = true;
            }
            else
            {
                Properties.Settings.Default.ShowMenuBar = false;
            }
            Properties.Settings.Default.Save();
            */
        }

        /// <summary>
        /// Event: Occurs when settings is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemSettings_Click(object sender, System.EventArgs e)
        {
            SettingsPage settingsBox = new SettingsPage();
            settingsBox.ShowDialog(this);
        }

        /// <summary>
        /// Event: Occurs when print is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Print_Click(object sender, System.EventArgs e)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }


        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event: Occurs when a new tab is requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuNewTabView_Click(object sender, System.EventArgs e)
        {
            this.tabs.CreateNewTab();
        }

        /// <summary>
        /// Event: Occurs when open is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemOpen_Click(object sender, System.EventArgs e)
        {
            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.Filter = ExtensionHandler.GetDialogFormat();
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentPage.Load(this.openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Event: Occurs when minimized is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemMinimize_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.UpdateAll();
            }
        }

        /// <summary>
        /// Event: Occurs when the maximize is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemMaximize_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.UpdateAll();
            }
        }

        /// <summary>
        /// Event: Occurs when restore is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menuItemRestore_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
                this.UpdateAll();
            }
        }

        void menuShowBorder_Click(object sender, System.EventArgs e)
        {
            this.FormBorderStyle = (this.FormBorderStyle == FormBorderStyle.None) ?
                FormBorderStyle.Sizable : this.FormBorderStyle = FormBorderStyle.None;
            this.menuShowBorder.Checked = (this.FormBorderStyle == FormBorderStyle.None) ?
                false : true;
            
            /*if (this.menuShowBorder.Checked)
                Properties.Settings.Default.ShowBorder = false;
            else Properties.Settings.Default.ShowBorder = true;
            Properties.Settings.Default.Save();*/
        }

        void menuItemAboutDetail_Click(object sender, System.EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        void tableLayoutPanel_MouseEnter(object sender, System.EventArgs e)
        {
            (sender as TableLayoutPanel).Parent.Focus();
        }

        void menuItemHelp_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(Settings.Default.HelloN00bString.Replace("\\n", "\n"), "Hello n00b!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #region overrides...

        protected override void OnMenuStart(EventArgs e)
        {
            base.OnMenuStart(e);
            this.UpdateAll();
        }

        /// <summary>
        /// Event: Occurs when the main form is closing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.tabs.TabCount > 1)
                {
                    if (MessageBox.Show(this, "Are you sure you want to close all " + this.tabs.TabCount + " tabs?", "Close?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            base.OnFormClosing(e);
        }

        #endregion

        /// <summary>
        /// Gets the currently selected Tab page.
        /// </summary>
        public f0t0page CurrentPage
        {
            get { return this.tabs.CurrentPage; }
        }

        /// <summary>
        /// Gets or sets whether the current page can accept cloning.
        /// </summary>
        public bool CanClone
        {
            get { return canClone; }
            set { canClone = value; }
        }
    }
}
