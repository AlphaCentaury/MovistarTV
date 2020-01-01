// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

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
            this.radioFormatXml = new System.Windows.Forms.RadioButton();
            this.radioFormatBinary = new System.Windows.Forms.RadioButton();
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
            this.textBoxResult.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(13, 64);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(659, 336);
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
            // radioFormatXml
            // 
            this.radioFormatXml.AutoSize = true;
            this.radioFormatXml.Checked = true;
            this.radioFormatXml.Location = new System.Drawing.Point(310, 41);
            this.radioFormatXml.Name = "radioFormatXml";
            this.radioFormatXml.Size = new System.Drawing.Size(149, 17);
            this.radioFormatXml.TabIndex = 24;
            this.radioFormatXml.TabStop = true;
            this.radioFormatXml.Text = "Format as UTF-8 XML text";
            this.radioFormatXml.UseVisualStyleBackColor = true;
            // 
            // radioFormatBinary
            // 
            this.radioFormatBinary.AutoSize = true;
            this.radioFormatBinary.Location = new System.Drawing.Point(491, 41);
            this.radioFormatBinary.Name = "radioFormatBinary";
            this.radioFormatBinary.Size = new System.Drawing.Size(126, 17);
            this.radioFormatBinary.TabIndex = 25;
            this.radioFormatBinary.Text = "Format as binary data";
            this.radioFormatBinary.UseVisualStyleBackColor = true;
            // 
            // SimpleDvbStpDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.radioFormatBinary);
            this.Controls.Add(this.radioFormatXml);
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
            this.Text = "Simple DVB-STP Payload downloader - GuiTools";
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
        private System.Windows.Forms.RadioButton radioFormatXml;
        private System.Windows.Forms.RadioButton radioFormatBinary;
    }
}
