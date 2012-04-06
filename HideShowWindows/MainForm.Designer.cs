namespace HideShowWindows
{
    partial class HideShow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HideShow));
            this.GetProgs = new System.Windows.Forms.Button();
            this.ProgList = new System.Windows.Forms.ListBox();
            this.Hide = new System.Windows.Forms.Button();
            this.Show = new System.Windows.Forms.Button();
            this.ProgListHidden = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // GetProgs
            // 
            this.GetProgs.Location = new System.Drawing.Point(12, 12);
            this.GetProgs.Name = "GetProgs";
            this.GetProgs.Size = new System.Drawing.Size(102, 50);
            this.GetProgs.TabIndex = 1;
            this.GetProgs.Text = "GetProgs";
            this.GetProgs.UseVisualStyleBackColor = true;
            this.GetProgs.Click += new System.EventHandler(this.GetProgs_Click);
            // 
            // ProgList
            // 
            this.ProgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgList.FormattingEnabled = true;
            this.ProgList.Location = new System.Drawing.Point(12, 98);
            this.ProgList.Name = "ProgList";
            this.ProgList.Size = new System.Drawing.Size(509, 186);
            this.ProgList.TabIndex = 2;
            this.ProgList.DoubleClick += new System.EventHandler(this.ProgList_DoubleClick);
            // 
            // Hide
            // 
            this.Hide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Hide.Location = new System.Drawing.Point(12, 69);
            this.Hide.Name = "Hide";
            this.Hide.Size = new System.Drawing.Size(102, 23);
            this.Hide.TabIndex = 3;
            this.Hide.Text = "Hide";
            this.Hide.UseVisualStyleBackColor = true;
            this.Hide.Click += new System.EventHandler(this.Hide_Click);
            // 
            // Show
            // 
            this.Show.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Show.Location = new System.Drawing.Point(12, 337);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(102, 23);
            this.Show.TabIndex = 4;
            this.Show.Text = "Show";
            this.Show.UseVisualStyleBackColor = true;
            this.Show.Click += new System.EventHandler(this.Show_Click);
            // 
            // ProgListHidden
            // 
            this.ProgListHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgListHidden.FormattingEnabled = true;
            this.ProgListHidden.Location = new System.Drawing.Point(12, 366);
            this.ProgListHidden.Name = "ProgListHidden";
            this.ProgListHidden.Size = new System.Drawing.Size(509, 238);
            this.ProgListHidden.TabIndex = 5;
            this.ProgListHidden.DoubleClick += new System.EventHandler(this.ProgListHidden_DoubleClick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HideShow_MouseDoubleClick);
            // 
            // HideShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 616);
            this.Controls.Add(this.ProgListHidden);
            this.Controls.Add(this.Show);
            this.Controls.Add(this.Hide);
            this.Controls.Add(this.ProgList);
            this.Controls.Add(this.GetProgs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HideShow";
            this.Text = "Hide - Show";
            this.Load += new System.EventHandler(this.HideShow_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HideShow_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.HideShow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetProgs;
        private System.Windows.Forms.ListBox ProgList;
        private System.Windows.Forms.Button Hide;
        private System.Collections.ArrayList ProgListHandle;
        private System.Windows.Forms.Button Show;
        private System.Windows.Forms.ListBox ProgListHidden;
        private System.Collections.ArrayList ProgListHiddenHandle;

        private string tempFilename;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

