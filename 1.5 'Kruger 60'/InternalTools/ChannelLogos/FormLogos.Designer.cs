namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    partial class FormLogos
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
            System.Windows.Forms.ColumnHeader ChannelLocal;
            System.Windows.Forms.ColumnHeader ChannelWeb;
            this.imgListLocalLogos = new System.Windows.Forms.ImageList(this.components);
            this.imgListWebLogos = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewLocalLogos = new System.Windows.Forms.ListView();
            this.listViewWebLogos = new System.Windows.Forms.ListView();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLocal = new System.Windows.Forms.ToolStripProgressBar();
            this.progressWeb = new System.Windows.Forms.ToolStripProgressBar();
            ChannelLocal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ChannelWeb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelLocal
            // 
            ChannelLocal.Text = "Channel";
            ChannelLocal.Width = 190;
            // 
            // ChannelWeb
            // 
            ChannelWeb.Text = "Channel";
            ChannelWeb.Width = 190;
            // 
            // imgListLocalLogos
            // 
            this.imgListLocalLogos.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListLocalLogos.ImageSize = new System.Drawing.Size(128, 128);
            this.imgListLocalLogos.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imgListWebLogos
            // 
            this.imgListWebLogos.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListWebLogos.ImageSize = new System.Drawing.Size(128, 128);
            this.imgListWebLogos.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewLocalLogos);
            this.splitContainer1.Panel1MinSize = 75;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewWebLogos);
            this.splitContainer1.Panel2MinSize = 75;
            this.splitContainer1.Size = new System.Drawing.Size(460, 328);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewLocalLogos
            // 
            this.listViewLocalLogos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ChannelLocal});
            this.listViewLocalLogos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLocalLogos.FullRowSelect = true;
            this.listViewLocalLogos.HideSelection = false;
            this.listViewLocalLogos.LargeImageList = this.imgListLocalLogos;
            this.listViewLocalLogos.Location = new System.Drawing.Point(0, 0);
            this.listViewLocalLogos.Name = "listViewLocalLogos";
            this.listViewLocalLogos.Size = new System.Drawing.Size(228, 328);
            this.listViewLocalLogos.SmallImageList = this.imgListLocalLogos;
            this.listViewLocalLogos.TabIndex = 0;
            this.listViewLocalLogos.UseCompatibleStateImageBehavior = false;
            this.listViewLocalLogos.View = System.Windows.Forms.View.Tile;
            this.listViewLocalLogos.SelectedIndexChanged += new System.EventHandler(this.listViewLocalLogos_SelectedIndexChanged);
            // 
            // listViewWebLogos
            // 
            this.listViewWebLogos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ChannelWeb});
            this.listViewWebLogos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWebLogos.FullRowSelect = true;
            this.listViewWebLogos.HideSelection = false;
            this.listViewWebLogos.LargeImageList = this.imgListWebLogos;
            this.listViewWebLogos.Location = new System.Drawing.Point(0, 0);
            this.listViewWebLogos.Name = "listViewWebLogos";
            this.listViewWebLogos.Size = new System.Drawing.Size(228, 328);
            this.listViewWebLogos.SmallImageList = this.imgListWebLogos;
            this.listViewWebLogos.TabIndex = 0;
            this.listViewWebLogos.UseCompatibleStateImageBehavior = false;
            this.listViewWebLogos.View = System.Windows.Forms.View.Tile;
            this.listViewWebLogos.SelectedIndexChanged += new System.EventHandler(this.listViewWebLogos_SelectedIndexChanged);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 12);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(125, 25);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load logos";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.progressLocal,
            this.progressWeb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 374);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoToolTip = true;
            this.labelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.labelStatus.Size = new System.Drawing.Size(315, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressLocal
            // 
            this.progressLocal.Name = "progressLocal";
            this.progressLocal.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressLocal.Size = new System.Drawing.Size(75, 16);
            this.progressLocal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // progressWeb
            // 
            this.progressWeb.Name = "progressWeb";
            this.progressWeb.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressWeb.Size = new System.Drawing.Size(75, 16);
            // 
            // FormLogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 396);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormLogos";
            this.Text = "Channel logos";
            this.Load += new System.EventHandler(this.FormLogos_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgListLocalLogos;
        private System.Windows.Forms.ImageList imgListWebLogos;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewLocalLogos;
        private System.Windows.Forms.ListView listViewWebLogos;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar progressLocal;
        private System.Windows.Forms.ToolStripProgressBar progressWeb;
    }
}

