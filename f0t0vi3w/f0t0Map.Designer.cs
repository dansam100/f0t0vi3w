namespace f0t0vi3w3r.Page.Map
{
    partial class f0t0Map
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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            {
                this.Unload();
                Dispose();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.boxtableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPictureBox = new PictureBox();
            this.picPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new f0t0NavPanel();
            this.rightPanel = new f0t0NavPanel();
            this.downPanel = new f0t0NavPanel();
            this.upPanel = new f0t0NavPanel();
            this.boxtableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boxtableLayoutPanel
            //
            this.boxtableLayoutPanel.ColumnCount = 3;
            this.boxtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.boxtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.boxtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.boxtableLayoutPanel.Controls.Add(this.picPanel, 1, 1);
            this.boxtableLayoutPanel.Controls.Add(this.leftPanel, 0, 0);
            this.boxtableLayoutPanel.Controls.Add(this.rightPanel, 2, 0);
            this.boxtableLayoutPanel.Controls.Add(this.downPanel, 1, 2);
            this.boxtableLayoutPanel.Controls.Add(this.upPanel, 1, 0);
            this.boxtableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxtableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.boxtableLayoutPanel.Name = "boxtableLayoutPanel";
            this.boxtableLayoutPanel.RowCount = 3;
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.boxtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.boxtableLayoutPanel.Size = new System.Drawing.Size(512, 417);
            this.boxtableLayoutPanel.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.AllowDrop = true;
            this.mainPictureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPictureBox.LoadedPicture = null;
            this.mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainPictureBox.LoadPicture = null;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize | System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.SizeChanged += new System.EventHandler(mainPictureBox_SizeChanged);
            this.mainPictureBox.PictureDeleted += new System.IO.FileSystemEventHandler(mainPictureBox_PictureDeleted);
            this.mainPictureBox.PictureRenamed += new System.IO.RenamedEventHandler(mainPictureBox_PictureRenamed);
            //
            //picPanel
            //
            this.picPanel.AutoScroll = true;
            this.picPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picPanel.Controls.Add(this.mainPictureBox);
            this.picPanel.Location = new System.Drawing.Point(3, 3);
            this.picPanel.Name = "picPanel";
            this.picPanel.TabIndex = 0;
            this.picPanel.SizeChanged += new System.EventHandler(picPanel_SizeChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.Name = "leftPanel";
            this.boxtableLayoutPanel.SetRowSpan(this.leftPanel, 3);
            this.leftPanel.TabIndex = 1;
            this.leftPanel.Click += new System.EventHandler(navPanel_Click);
            this.leftPanel.AllowDrop = true;
            this.leftPanel.Action = NavigationAction.MoveBackward;
            // 
            // rightPanel
            // 
            this.rightPanel.Name = "rightPanel";
            this.boxtableLayoutPanel.SetRowSpan(this.rightPanel, 3);
            this.rightPanel.TabIndex = 2;
            this.rightPanel.Click += new System.EventHandler(navPanel_Click);
            this.rightPanel.AllowDrop = true;
            this.rightPanel.Action = NavigationAction.MoveForward;
            // 
            // downPanel
            // 
            this.downPanel.Name = "downPanel";
            this.downPanel.TabIndex = 3;
            this.downPanel.Click += new System.EventHandler(navPanel_Click);
            this.downPanel.AllowDrop = true;
            this.downPanel.Action = NavigationAction.MoveForward;
            // 
            // upPanel
            // 
            this.upPanel.Name = "upPanel";
            this.upPanel.TabIndex = 4;
            this.upPanel.Click += new System.EventHandler(navPanel_Click);
            this.upPanel.AllowDrop = true;
            this.upPanel.Action = NavigationAction.MoveBackward;
            // 
            // f0t0Map
            //
            this.ClientSize = new System.Drawing.Size(512, 417);
            this.Controls.Add(this.boxtableLayoutPanel);
            this.Name = "f0t0Map";
            this.Text = "f0t0Map";
            this.boxtableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel boxtableLayoutPanel;
        private global::f0t0vi3w3r.Page.PictureBox mainPictureBox;
        private System.Windows.Forms.Panel picPanel;
        private f0t0NavPanel leftPanel;
        private f0t0NavPanel rightPanel;
        private f0t0NavPanel downPanel;
        private f0t0NavPanel upPanel;
    }
}