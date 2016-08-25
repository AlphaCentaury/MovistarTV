// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace IpTviewr.Internal.Tools.GuiTools
{
    partial class SimpleDvbStpDownloadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleDvbStpDownloadForm));
            this.textSegmentId = new System.Windows.Forms.TextBox();
            this.labelSegmentID = new System.Windows.Forms.Label();
            this.textPayloadId = new System.Windows.Forms.TextBox();
            this.labelPayloadId = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textSegmentId
            // 
            this.textSegmentId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textSegmentId.Location = new System.Drawing.Point(491, 15);
            this.textSegmentId.Name = "textSegmentId";
            this.textSegmentId.Size = new System.Drawing.Size(75, 20);
            this.textSegmentId.TabIndex = 19;
            // 
            // labelSegmentID
            // 
            this.labelSegmentID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSegmentID.AutoSize = true;
            this.labelSegmentID.Location = new System.Drawing.Point(425, 18);
            this.labelSegmentID.Name = "labelSegmentID";
            this.labelSegmentID.Size = new System.Drawing.Size(60, 13);
            this.labelSegmentID.TabIndex = 18;
            this.labelSegmentID.Text = "SegmentID";
            // 
            // textPayloadId
            // 
            this.textPayloadId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textPayloadId.Location = new System.Drawing.Point(369, 15);
            this.textPayloadId.Name = "textPayloadId";
            this.textPayloadId.Size = new System.Drawing.Size(50, 20);
            this.textPayloadId.TabIndex = 17;
            this.textPayloadId.Text = "0x01";
            // 
            // labelPayloadId
            // 
            this.labelPayloadId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPayloadId.AutoSize = true;
            this.labelPayloadId.Location = new System.Drawing.Point(307, 18);
            this.labelPayloadId.Name = "labelPayloadId";
            this.labelPayloadId.Size = new System.Drawing.Size(56, 13);
            this.labelPayloadId.TabIndex = 16;
            this.labelPayloadId.Text = "PayloadID";
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(13, 43);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(659, 357);
            this.textBoxResult.TabIndex = 23;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDownload.Image = ((System.Drawing.Image)(resources.GetObject("buttonDownload.Image")));
            this.buttonDownload.Location = new System.Drawing.Point(572, 12);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(100, 25);
            this.buttonDownload.TabIndex = 20;
            this.buttonDownload.Text = "Get Payload";
            this.buttonDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(213, 15);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(50, 20);
            this.textPort.TabIndex = 15;
            this.textPort.Text = "3937";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(181, 18);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 14;
            this.labelPort.Text = "Port";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(75, 15);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(100, 20);
            this.textIpAddress.TabIndex = 13;
            this.textIpAddress.Text = "239.0.2.129";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(12, 18);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(57, 13);
            this.labelIpAddress.TabIndex = 12;
            this.labelIpAddress.Text = "IP address";
            // 
            // SimpleDvbStpDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.textSegmentId);
            this.Controls.Add(this.labelSegmentID);
            this.Controls.Add(this.textPayloadId);
            this.Controls.Add(this.labelPayloadId);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textIpAddress);
            this.Controls.Add(this.labelIpAddress);
            this.Name = "SimpleDvbStpDownloadForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Simple DVB-STP Payload downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textSegmentId;
        private System.Windows.Forms.Label labelSegmentID;
        private System.Windows.Forms.TextBox textPayloadId;
        private System.Windows.Forms.Label labelPayloadId;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textIpAddress;
        private System.Windows.Forms.Label labelIpAddress;

    }
}