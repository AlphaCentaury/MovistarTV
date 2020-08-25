namespace IpTviewr.Internal.Tools.GuiTools
{
    partial class BaseExplorerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusLabelReceiving = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDataReception = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDatagramCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelByteCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBaseSaveFolder = new System.Windows.Forms.TextBox();
            this.labelBaseSaveFolder = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.TabIndex = 5;
            // 
            // buttonStart
            // 
            this.buttonStart.TabIndex = 4;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelReceiving,
            this.statusLabelDataReception,
            this.statusLabelDatagramCount,
            this.statusLabelByteCount});
            this.statusStripMain.Location = new System.Drawing.Point(0, 390);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(684, 22);
            this.statusStripMain.TabIndex = 7;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // statusLabelReceiving
            // 
            this.statusLabelReceiving.AutoSize = false;
            this.statusLabelReceiving.Name = "statusLabelReceiving";
            this.statusLabelReceiving.Size = new System.Drawing.Size(175, 17);
            this.statusLabelReceiving.Text = "Data reception is in progress";
            this.statusLabelReceiving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabelDataReception
            // 
            this.statusLabelDataReception.AutoSize = false;
            this.statusLabelDataReception.Font = new System.Drawing.Font("Wingdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.statusLabelDataReception.Name = "statusLabelDataReception";
            this.statusLabelDataReception.Size = new System.Drawing.Size(125, 17);
            this.statusLabelDataReception.Text = "lll";
            // 
            // statusLabelDatagramCount
            // 
            this.statusLabelDatagramCount.AutoSize = false;
            this.statusLabelDatagramCount.Name = "statusLabelDatagramCount";
            this.statusLabelDatagramCount.Size = new System.Drawing.Size(184, 17);
            this.statusLabelDatagramCount.Spring = true;
            this.statusLabelDatagramCount.Text = "Datagram count";
            this.statusLabelDatagramCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusLabelByteCount
            // 
            this.statusLabelByteCount.AutoSize = false;
            this.statusLabelByteCount.Name = "statusLabelByteCount";
            this.statusLabelByteCount.Size = new System.Drawing.Size(184, 17);
            this.statusLabelByteCount.Spring = true;
            this.statusLabelByteCount.Text = "Total received bytes";
            this.statusLabelByteCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBaseSaveFolder
            // 
            this.textBaseSaveFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaseSaveFolder.Location = new System.Drawing.Point(375, 43);
            this.textBaseSaveFolder.Name = "textBaseSaveFolder";
            this.textBaseSaveFolder.Size = new System.Drawing.Size(297, 20);
            this.textBaseSaveFolder.TabIndex = 7;
            // 
            // labelBaseSaveFolder
            // 
            this.labelBaseSaveFolder.AutoSize = true;
            this.labelBaseSaveFolder.Location = new System.Drawing.Point(280, 46);
            this.labelBaseSaveFolder.Name = "labelBaseSaveFolder";
            this.labelBaseSaveFolder.Size = new System.Drawing.Size(89, 13);
            this.labelBaseSaveFolder.TabIndex = 6;
            this.labelBaseSaveFolder.Text = "Base save folder:";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(219, 15);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(50, 20);
            this.textPort.TabIndex = 3;
            this.textPort.Text = "3937";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(184, 18);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "Port:";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(78, 15);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(100, 20);
            this.textIpAddress.TabIndex = 1;
            this.textIpAddress.Text = "239.0.2.129";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(12, 18);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(60, 13);
            this.labelIpAddress.TabIndex = 0;
            this.labelIpAddress.Text = "IP address:";
            // 
            // BaseExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.textBaseSaveFolder);
            this.Controls.Add(this.labelBaseSaveFolder);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textIpAddress);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.statusStripMain);
            this.Name = "BaseExplorerForm";
            this.Text = "BaseExplorerForm";
            this.Controls.SetChildIndex(this.statusStripMain, 0);
            this.Controls.SetChildIndex(this.buttonStart, 0);
            this.Controls.SetChildIndex(this.buttonStop, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.textIpAddress, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.labelBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.textBaseSaveFolder, 0);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.StatusStrip statusStripMain;
        protected System.Windows.Forms.ToolStripStatusLabel statusLabelReceiving;
        protected System.Windows.Forms.ToolStripStatusLabel statusLabelDataReception;
        protected System.Windows.Forms.ToolStripStatusLabel statusLabelDatagramCount;
        protected System.Windows.Forms.ToolStripStatusLabel statusLabelByteCount;
        protected System.Windows.Forms.TextBox textBaseSaveFolder;
        protected System.Windows.Forms.Label labelBaseSaveFolder;
        protected System.Windows.Forms.TextBox textPort;
        protected System.Windows.Forms.TextBox textIpAddress;
        protected System.Windows.Forms.Label labelIpAddress;
        protected System.Windows.Forms.Label labelPort;
    }
}