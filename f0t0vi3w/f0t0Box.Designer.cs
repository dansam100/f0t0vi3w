/*
 * Created by SharpDevelop.
 * User: samuel
 * Date: 16/02/2008
 * Time: 4:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using f0t0vi3w3r.Page;
using System.Windows.Forms;
namespace f0t0vi3w3r
{
    public partial class f0t0Box
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.topMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter1 = new System.Windows.Forms.MenuItem();
            this.menuNewTabView = new System.Windows.Forms.MenuItem();
            this.menuItemCloseTab = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter2 = new System.Windows.Forms.MenuItem();
            this.menuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter5 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemView = new System.Windows.Forms.MenuItem();
            this.menuItemClone = new System.Windows.Forms.MenuItem();
            this.fotopage = new f0t0vi3w3r.Page.f0t0page();
            this.menuRemoveClone = new System.Windows.Forms.MenuItem();
            this.menuChangeSplitOrientation = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter3 = new System.Windows.Forms.MenuItem();
            this.menuShowBorder = new System.Windows.Forms.MenuItem();
            this.menuHideMenuBar = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter4 = new System.Windows.Forms.MenuItem();
            this.menuItemMaximize = new System.Windows.Forms.MenuItem();
            this.menuItemRestore = new System.Windows.Forms.MenuItem();
            this.menuItemMinimize = new System.Windows.Forms.MenuItem();
            this.menuItemFullScreen = new System.Windows.Forms.MenuItem();
            this.menuSlideShow = new System.Windows.Forms.MenuItem();
            this.menuItemRepeat = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAboutDetail = new System.Windows.Forms.MenuItem();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.navLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tabs = new f0t0vi3w3r.Page.f0t0tab();
            this.tableLayoutPanel.SuspendLayout();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemView,
            this.menuItemOptions,
            this.menuItemAbout});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpen,
            this.menuItemSplitter1,
            this.menuNewTabView,
            this.menuItemCloseTab,
            this.menuItemSplitter2,
            this.menuItemPrint,
            this.menuItemSplitter5,
            this.menuItemExit});
            this.menuItemFile.Text = "File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 0;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSplitter1
            // 
            this.menuItemSplitter1.Index = 1;
            this.menuItemSplitter1.Text = "-";
            // 
            // menuNewTabView
            // 
            this.menuNewTabView.Index = 2;
            this.menuNewTabView.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.menuNewTabView.Text = "New &Tab";
            this.menuNewTabView.Click += new System.EventHandler(this.menuNewTabView_Click);
            // 
            // menuItemCloseTab
            // 
            this.menuItemCloseTab.Enabled = false;
            this.menuItemCloseTab.Index = 3;
            this.menuItemCloseTab.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemCloseTab.Text = "Cl&ose Tab";
            this.menuItemCloseTab.Click += new System.EventHandler(this.menuItemCloseTab_Click);
            // 
            // menuItemSplitter2
            // 
            this.menuItemSplitter2.Index = 4;
            this.menuItemSplitter2.Text = "-";
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Index = 5;
            this.menuItemPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItemPrint.Text = "&Print";
            this.menuItemPrint.Click += new System.EventHandler(this.Print_Click);
            // 
            // menuItemSplitter5
            // 
            this.menuItemSplitter5.Index = 6;
            this.menuItemSplitter5.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 7;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.Index = 1;
            this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemClone,
            this.menuRemoveClone,
            this.menuChangeSplitOrientation,
            this.menuItemSplitter3,
            this.menuShowBorder,
            this.menuHideMenuBar,
            this.menuItemSplitter4,
            this.menuItemMaximize,
            this.menuItemRestore,
            this.menuItemMinimize,
            this.menuItemFullScreen,
            this.menuSlideShow,
            this.menuItemRepeat});
            this.menuItemView.Text = "&View";
            // 
            // menuItemClone
            // 
            this.menuItemClone.Enabled = this.fotopage.CanClone;
            this.menuItemClone.Index = 0;
            this.menuItemClone.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.menuItemClone.Text = "&Clone View Against";
            this.menuItemClone.Click += new System.EventHandler(this.menuItemClone_Click);
            // 
            // fotopage
            // 
            this.fotopage.AllowDrop = true;
            this.fotopage.CanClose = false;
            this.fotopage.Location = new System.Drawing.Point(4, 29);
            this.fotopage.Name = "fotopage";
            this.fotopage.Padding = new System.Windows.Forms.Padding(3);
            this.fotopage.Size = new System.Drawing.Size(745, 551);
            this.fotopage.SplitOrientation = System.Windows.Forms.Orientation.Vertical;
            this.fotopage.TabIndex = 2;
            this.fotopage.Text = "n0 pic";
            this.fotopage.UseVisualStyleBackColor = true;
            // 
            // menuRemoveClone
            // 
            this.menuRemoveClone.Enabled = false;
            this.menuRemoveClone.Index = 1;
            this.menuRemoveClone.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.menuRemoveClone.Text = "&Remove Clone...";
            this.menuRemoveClone.Click += new System.EventHandler(this.menuRemoveClone_Click);
            // 
            // menuChangeSplitOrientation
            // 
            this.menuChangeSplitOrientation.Enabled = this.fotopage.IsCloned;
            this.menuChangeSplitOrientation.Index = 2;
            this.menuChangeSplitOrientation.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuChangeSplitOrientation.Text = "Ch&ange Split Orientation";
            this.menuChangeSplitOrientation.Click += new System.EventHandler(this.menuChangeSplitOrientation_Click);
            // 
            // menuItemSplitter3
            // 
            this.menuItemSplitter3.Index = 3;
            this.menuItemSplitter3.Text = "-";
            // 
            // menuShowBorder
            // 
            this.menuShowBorder.Index = 4;
            this.menuShowBorder.Text = "Show B&order";
            this.menuShowBorder.Click += new System.EventHandler(this.menuShowBorder_Click);
            // 
            // menuHideMenuBar
            // 
            this.menuHideMenuBar.Index = 5;
            this.menuHideMenuBar.Text = "Show Menu &Bar";
            this.menuHideMenuBar.Click += new System.EventHandler(this.menuHideMenuBar_Click);
            // 
            // menuItemSplitter4
            // 
            this.menuItemSplitter4.Index = 6;
            this.menuItemSplitter4.Text = "-";
            // 
            // menuItemMaximize
            // 
            this.menuItemMaximize.Enabled = false;
            this.menuItemMaximize.Index = 7;
            this.menuItemMaximize.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuItemMaximize.Text = "Ma&ximize";
            this.menuItemMaximize.Click += new System.EventHandler(this.menuItemMaximize_Click);
            // 
            // menuItemRestore
            // 
            this.menuItemRestore.DefaultItem = true;
            this.menuItemRestore.Enabled = false;
            this.menuItemRestore.Index = 8;
            this.menuItemRestore.Text = "R&estore";
            this.menuItemRestore.Click += new System.EventHandler(this.menuItemRestore_Click);
            // 
            // menuItemMinimize
            // 
            this.menuItemMinimize.Enabled = false;
            this.menuItemMinimize.Index = 9;
            this.menuItemMinimize.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItemMinimize.Text = "Mi&nimize";
            this.menuItemMinimize.Click += new System.EventHandler(this.menuItemMinimize_Click);
            // 
            // menuItemFullScreen
            // 
            this.menuItemFullScreen.Index = 10;
            this.menuItemFullScreen.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.menuItemFullScreen.Text = "&Full Screen";
            // 
            // menuSlideShow
            // 
            this.menuSlideShow.Enabled = false;
            this.menuSlideShow.Index = 11;
            this.menuSlideShow.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.menuSlideShow.Text = "&Slide Show";
            // 
            // menuItemRepeat
            // 
            this.menuItemRepeat.Index = 12;
            this.menuItemRepeat.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.menuItemRepeat.Text = "&Repeat";
            this.menuItemRepeat.Click += new System.EventHandler(this.menuItemRepeat_Click);
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Index = 2;
            this.menuItemOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSettings});
            this.menuItemOptions.Text = "&Options";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 0;
            this.menuItemSettings.Text = "&Settings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 3;
            this.menuItemAbout.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemHelp,
            this.menuItemAboutDetail});
            this.menuItemAbout.Text = "&About";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 0;
            this.menuItemHelp.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItemHelp.Text = "&Help";
            this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
            // 
            // menuItemAboutDetail
            // 
            this.menuItemAboutDetail.Index = 1;
            this.menuItemAboutDetail.Text = "&About f0t0Vi3w3r...";
            this.menuItemAboutDetail.Click += new System.EventHandler(this.menuItemAboutDetail_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(0, 0);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(100, 20);
            this.descriptionBox.TabIndex = 0;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.navLayoutPanel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tabs, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(759, 615);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.f0t0Box_MouseDown);
            this.tableLayoutPanel.MouseEnter += new System.EventHandler(this.tableLayoutPanel_MouseEnter);
            // 
            // navLayoutPanel
            // 
            this.navLayoutPanel.ColumnCount = 3;
            this.navLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.navLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.navLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.navLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navLayoutPanel.Location = new System.Drawing.Point(3, 593);
            this.navLayoutPanel.Name = "navLayoutPanel";
            this.navLayoutPanel.RowCount = 1;
            this.navLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.navLayoutPanel.Size = new System.Drawing.Size(753, 19);
            this.navLayoutPanel.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.AllowDrop = true;
            this.tabs.AllowRepeat = true;
            this.tabs.FotoBox = this;
            this.tabs.AutoRepeatDelay = 1000;
            this.tabs.Controls.Add(this.fotopage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.HotTrack = true;
            this.tabs.ItemSize = new System.Drawing.Size(140, 25);
            this.tabs.Location = new System.Drawing.Point(3, 3);
            this.tabs.LockAndScroll = false;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.ShowTinyImages = false;
            this.tabs.ShowToolTips = true;
            this.tabs.Size = new System.Drawing.Size(753, 584);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.SplitOrientation = System.Windows.Forms.Orientation.Vertical;
            this.tabs.TabIndex = 5;
            this.tabs.TabStop = false;
            this.tabs.TabWidth = 20;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // f0t0Box
            //
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 615);
            this.Controls.Add(this.tableLayoutPanel);
            this.Menu = this.topMenu;
            this.Name = "f0t0Box";
            this.Text = "f0t0vi3w3r";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.f0t0Box_MouseDown);
            this.Load += new System.EventHandler(this.f0t0Box_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.MainMenu topMenu;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemOpen;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemView;
        private System.Windows.Forms.MenuItem menuNewTabView;
        private System.Windows.Forms.MenuItem menuItemClone;
        private System.Windows.Forms.MenuItem menuRemoveClone;
        private System.Windows.Forms.MenuItem menuChangeSplitOrientation;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private f0t0tab tabs;
        private f0t0page fotopage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel navLayoutPanel;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemAboutDetail;
        private System.Windows.Forms.MenuItem menuItemSplitter1;
        private System.Windows.Forms.MenuItem menuItemCloseTab;
        private System.Windows.Forms.MenuItem menuItemSplitter2;
        private System.Windows.Forms.MenuItem menuItemOptions;
        private System.Windows.Forms.MenuItem menuItemSettings;
        private System.Windows.Forms.MenuItem menuItemSplitter3;
        private System.Windows.Forms.MenuItem menuItemMaximize;
        private System.Windows.Forms.MenuItem menuItemMinimize;
        private System.Windows.Forms.MenuItem menuItemFullScreen;
        private System.Windows.Forms.MenuItem menuSlideShow;
        private System.Windows.Forms.MenuItem menuItemRepeat;
        private System.Windows.Forms.MenuItem menuItemRestore;
        private System.Windows.Forms.MenuItem menuHideMenuBar;
        private System.Windows.Forms.MenuItem menuItemSplitter4;
        private System.Windows.Forms.MenuItem menuShowBorder;
        private System.Windows.Forms.MenuItem menuItemPrint;
        private System.Windows.Forms.MenuItem menuItemSplitter5;
    }
}
