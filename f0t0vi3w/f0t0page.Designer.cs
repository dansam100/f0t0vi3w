using System.Windows.Forms;
using System.Drawing;
using f0t0vi3w3r.Page.Map;
namespace f0t0vi3w3r.Page
{
    partial class f0t0page
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPicBox = new f0t0Map();
            this.clonePicBox = new f0t0Map();
            this.pictableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPicPanel = new System.Windows.Forms.Panel();
            this.clonePicPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pictableLayoutPanel.SuspendLayout();
            this.mainPicPanel.SuspendLayout();
            this.clonePicPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPicBox
            // 
            this.mainPicBox.AllowDrop = true;
            this.mainPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPicBox.BackColor = SystemColors.ControlDarkDark;
            this.mainPicBox.LoadPicture = new LoadPictureHandler(this.Load);
            this.mainPicBox.Location = new System.Drawing.Point(0, 0);
            this.mainPicBox.Name = "mainPicBox";
            this.mainPicBox.Size = new System.Drawing.Size(454, 362);
            this.mainPicBox.TabIndex = 0;
            this.mainPicBox.SetFotoPage(this);
            this.mainPicBox.AutoScroll = true;
            this.mainPicBox.NavigationInvoked += new NavigationPushedHandler(mainPicBox_NavigationInvoked);
            this.mainPicBox.PictureUnloaded += new UnloadPictureEventHandler(this.UnloadPicture);
            this.mainPicBox.PictureDeleted += new System.IO.FileSystemEventHandler(PictureBox_PictureDeleted);
            this.mainPicBox.PictureRenamed += new System.IO.RenamedEventHandler(PictureBox_PictureRenamed);
            this.mainPicBox.TabStop = false;
            // 
            // clonePicBox
            // 
            this.clonePicBox.AllowDrop = true;
            this.clonePicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clonePicBox.LoadPicture = new LoadPictureHandler(this.LoadClone);
            this.clonePicBox.Location = new System.Drawing.Point(0, 0);
            this.clonePicBox.Name = "clonePicBox";
            this.clonePicBox.CanNavigate = canNavigateClone;
            this.clonePicBox.Size = new System.Drawing.Size(87, 30);
            this.clonePicBox.TabIndex = 0;
            this.clonePicBox.SetFotoPage(this);
            this.clonePicBox.AutoScroll = true;
            this.clonePicBox.NavigationInvoked += new NavigationPushedHandler(mainPicBox_NavigationInvoked);
            this.clonePicBox.PictureRenamed += new System.IO.RenamedEventHandler(PictureBox_PictureRenamed);
            this.clonePicBox.PictureDeleted += new System.IO.FileSystemEventHandler(PictureBox_PictureDeleted);
            this.clonePicBox.TabStop = false;
            // 
            // pictableLayoutPanel
            // 
            this.pictableLayoutPanel.ColumnCount = 1;
            this.pictableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pictableLayoutPanel.Controls.Add(this.splitContainer);
            this.pictableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.pictableLayoutPanel.Name = "pictableLayoutPanel";
            this.pictableLayoutPanel.RowCount = 1;
            this.pictableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pictableLayoutPanel.Size = new System.Drawing.Size(465, 372);
            this.pictableLayoutPanel.TabIndex = 0;
            // 
            // mainPicPanel
            // 
            this.mainPicPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictableLayoutPanel.SetColumnSpan(this.mainPicPanel, 1);
            this.mainPicPanel.Controls.Add(this.mainPicBox);
            this.mainPicPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPicPanel.Location = new System.Drawing.Point(3, 3);
            this.mainPicPanel.Name = "mainPicPanel";
            this.pictableLayoutPanel.SetRowSpan(this.mainPicPanel, 1);
            this.mainPicPanel.Size = new System.Drawing.Size(458, 366);
            this.mainPicPanel.TabIndex = 0;
            // 
            // clonePicPanel
            // 
            this.clonePicPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clonePicPanel.Controls.Add(this.clonePicBox);
            this.clonePicPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clonePicPanel.Location = new System.Drawing.Point(3, 3);
            this.clonePicPanel.Name = "clonePicPanel";
            this.clonePicPanel.Size = new System.Drawing.Size(91, 34);
            this.clonePicPanel.TabIndex = 1;
            //
            //splitContainer
            //
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.BackColor = SystemColors.ControlDarkDark;
            this.splitContainer.ForeColor = SystemColors.InfoText;
            this.splitContainer.BorderStyle = BorderStyle.Fixed3D;
            this.splitContainer.Orientation = f0t0tab.Orientation;
            this.splitContainer.Panel1.Controls.Add(this.mainPicPanel);
            this.splitContainer.Panel2.Controls.Add(this.clonePicPanel);
            this.splitContainer.TabStop = false;
            this.splitContainer.SplitterDistance = this.pictableLayoutPanel.Width;
            this.splitContainer.SplitterIncrement = 20;
            this.splitContainer.SplitterWidth = 4;
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Panel1MinSize = 0;
            this.splitContainer.Panel2Collapsed = true;
            // 
            // f0t0page
            //
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(471, 378);
            this.Controls.Add(this.pictableLayoutPanel);
            this.Name = "f0t0page";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "n0 pic";
            this.pictableLayoutPanel.ResumeLayout(false);
            this.mainPicPanel.ResumeLayout(false);
            this.clonePicPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private f0t0Map mainPicBox;
        private f0t0Map clonePicBox;
        private System.Windows.Forms.TableLayoutPanel pictableLayoutPanel;
        private System.Windows.Forms.Panel mainPicPanel;
        private System.Windows.Forms.Panel clonePicPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}