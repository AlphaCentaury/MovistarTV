namespace IpTviewr.Internal.Tools.GuiTools
{
    partial class RawEpgDownloaderForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader columnHeaderSegmentIdentity;
            System.Windows.Forms.ColumnHeader columnHeaderFragmentCount;
            System.Windows.Forms.ColumnHeader columnHeaderSegmentSize;
            System.Windows.Forms.ColumnHeader columnHeaderTime;
            System.Windows.Forms.ColumnHeader columnHeader5;
            System.Windows.Forms.ColumnHeader columnHeader6;
            System.Windows.Forms.ColumnHeader columnHeader7;
            System.Windows.Forms.ColumnHeader columnHeader8;
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawEpgDownloaderForm));
            this.groupBoxAbridgedEPG = new System.Windows.Forms.GroupBox();
            this.labelDataReception = new System.Windows.Forms.Label();
            this.listViewDownloadedSegmentsAbridged = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.textBoxDomainName = new System.Windows.Forms.TextBox();
            this.labelDomainName = new System.Windows.Forms.Label();
            this.listViewDownloadedSegments = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.listViewPayloads = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.imageListPayloadStatus = new System.Windows.Forms.ImageList(this.components);
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSegmentIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderFragmentCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSegmentSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxAbridgedEPG.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBaseSaveFolder
            // 
            this.textBaseSaveFolder.Location = new System.Drawing.Point(110, 41);
            this.textBaseSaveFolder.Size = new System.Drawing.Size(350, 20);
            // 
            // labelBaseSaveFolder
            // 
            this.labelBaseSaveFolder.Location = new System.Drawing.Point(15, 44);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(267, 12);
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(126, 12);
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.Location = new System.Drawing.Point(12, 15);
            this.labelIpAddress.Size = new System.Drawing.Size(108, 13);
            this.labelIpAddress.Text = "Bootstrap IP address:";
            // 
            // labelPort
            // 
            this.labelPort.Location = new System.Drawing.Point(232, 15);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(572, 38);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(466, 38);
            // 
            // columnHeaderSegmentIdentity
            // 
            columnHeaderSegmentIdentity.Text = "Segment id";
            columnHeaderSegmentIdentity.Width = 100;
            // 
            // columnHeaderFragmentCount
            // 
            columnHeaderFragmentCount.Text = "Fragments";
            columnHeaderFragmentCount.Width = 100;
            // 
            // columnHeaderSegmentSize
            // 
            columnHeaderSegmentSize.Text = "Segment size";
            columnHeaderSegmentSize.Width = 100;
            // 
            // columnHeaderTime
            // 
            columnHeaderTime.Text = "Time";
            columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnHeaderTime.Width = 200;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Segment id";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Fragments";
            columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Segment size";
            columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Time";
            columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnHeader8.Width = 200;
            // 
            // groupBoxAbridgedEPG
            // 
            this.groupBoxAbridgedEPG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAbridgedEPG.Controls.Add(this.labelDataReception);
            this.groupBoxAbridgedEPG.Controls.Add(this.listViewDownloadedSegmentsAbridged);
            this.groupBoxAbridgedEPG.Location = new System.Drawing.Point(12, 74);
            this.groupBoxAbridgedEPG.Name = "groupBoxAbridgedEPG";
            this.groupBoxAbridgedEPG.Size = new System.Drawing.Size(660, 124);
            this.groupBoxAbridgedEPG.TabIndex = 8;
            this.groupBoxAbridgedEPG.TabStop = false;
            this.groupBoxAbridgedEPG.Text = "Present && following EPG";
            // 
            // labelDataReception
            // 
            this.labelDataReception.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDataReception.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelDataReception.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDataReception.Location = new System.Drawing.Point(529, 0);
            this.labelDataReception.Name = "labelDataReception";
            this.labelDataReception.Size = new System.Drawing.Size(125, 15);
            this.labelDataReception.TabIndex = 37;
            this.labelDataReception.Text = "l";
            this.labelDataReception.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listViewDownloadedSegmentsAbridged
            // 
            this.listViewDownloadedSegmentsAbridged.AllowColumnReorder = true;
            this.listViewDownloadedSegmentsAbridged.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDownloadedSegmentsAbridged.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderSegmentIdentity,
            columnHeaderFragmentCount,
            columnHeaderSegmentSize,
            columnHeaderTime});
            this.listViewDownloadedSegmentsAbridged.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDownloadedSegmentsAbridged.FullRowSelect = true;
            this.listViewDownloadedSegmentsAbridged.GridLines = true;
            this.listViewDownloadedSegmentsAbridged.HeaderCustomFont = null;
            this.listViewDownloadedSegmentsAbridged.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewDownloadedSegmentsAbridged.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDownloadedSegmentsAbridged.HideSelection = false;
            this.listViewDownloadedSegmentsAbridged.IsDoubleBuffered = true;
            this.listViewDownloadedSegmentsAbridged.Location = new System.Drawing.Point(6, 19);
            this.listViewDownloadedSegmentsAbridged.MultiSelect = false;
            this.listViewDownloadedSegmentsAbridged.Name = "listViewDownloadedSegmentsAbridged";
            this.listViewDownloadedSegmentsAbridged.SelfSorting = false;
            this.listViewDownloadedSegmentsAbridged.Size = new System.Drawing.Size(648, 99);
            this.listViewDownloadedSegmentsAbridged.TabIndex = 8;
            this.listViewDownloadedSegmentsAbridged.UseCompatibleStateImageBehavior = false;
            this.listViewDownloadedSegmentsAbridged.View = System.Windows.Forms.View.Details;
            // 
            // textBoxDomainName
            // 
            this.textBoxDomainName.Location = new System.Drawing.Point(466, 12);
            this.textBoxDomainName.Name = "textBoxDomainName";
            this.textBoxDomainName.Size = new System.Drawing.Size(206, 20);
            this.textBoxDomainName.TabIndex = 20;
            this.textBoxDomainName.Text = "DEM_19.imagenio.es";
            // 
            // labelDomainName
            // 
            this.labelDomainName.AutoSize = true;
            this.labelDomainName.Location = new System.Drawing.Point(336, 15);
            this.labelDomainName.Name = "labelDomainName";
            this.labelDomainName.Size = new System.Drawing.Size(124, 13);
            this.labelDomainName.TabIndex = 19;
            this.labelDomainName.Text = "Service provider domain:";
            // 
            // listViewDownloadedSegments
            // 
            this.listViewDownloadedSegments.AllowColumnReorder = true;
            this.listViewDownloadedSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDownloadedSegments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader5,
            columnHeader6,
            columnHeader7,
            columnHeader8});
            this.listViewDownloadedSegments.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDownloadedSegments.FullRowSelect = true;
            this.listViewDownloadedSegments.GridLines = true;
            this.listViewDownloadedSegments.HeaderCustomFont = null;
            this.listViewDownloadedSegments.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewDownloadedSegments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDownloadedSegments.HideSelection = false;
            this.listViewDownloadedSegments.IsDoubleBuffered = true;
            this.listViewDownloadedSegments.Location = new System.Drawing.Point(12, 409);
            this.listViewDownloadedSegments.MultiSelect = false;
            this.listViewDownloadedSegments.Name = "listViewDownloadedSegments";
            this.listViewDownloadedSegments.SelfSorting = false;
            this.listViewDownloadedSegments.Size = new System.Drawing.Size(660, 127);
            this.listViewDownloadedSegments.TabIndex = 22;
            this.listViewDownloadedSegments.UseCompatibleStateImageBehavior = false;
            this.listViewDownloadedSegments.View = System.Windows.Forms.View.Details;
            // 
            // listViewPayloads
            // 
            this.listViewPayloads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPayloads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            columnHeader1,
            columnHeader2,
            columnHeader3,
            this.columnHeader4});
            this.listViewPayloads.FullRowSelect = true;
            this.listViewPayloads.GridLines = true;
            this.listViewPayloads.HeaderCustomFont = null;
            this.listViewPayloads.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewPayloads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPayloads.HideSelection = false;
            this.listViewPayloads.IsDoubleBuffered = true;
            this.listViewPayloads.Location = new System.Drawing.Point(12, 204);
            this.listViewPayloads.Name = "listViewPayloads";
            this.listViewPayloads.SelfSorting = false;
            this.listViewPayloads.ShowItemToolTips = true;
            this.listViewPayloads.Size = new System.Drawing.Size(660, 199);
            this.listViewPayloads.SmallImageList = this.imageListPayloadStatus;
            this.listViewPayloads.TabIndex = 23;
            this.listViewPayloads.UseCompatibleStateImageBehavior = false;
            this.listViewPayloads.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Type of information";
            columnHeader1.Width = 210;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Fragments";
            columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Progress";
            columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader3.Width = 65;
            // 
            // imageListPayloadStatus
            // 
            this.imageListPayloadStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPayloadStatus.ImageStream")));
            this.imageListPayloadStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPayloadStatus.Images.SetKeyName(0, "Waiting");
            this.imageListPayloadStatus.Images.SetKeyName(1, "Restarted");
            this.imageListPayloadStatus.Images.SetKeyName(2, "Downloading");
            this.imageListPayloadStatus.Images.SetKeyName(3, "Completed");
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Multicast address";
            this.columnHeader0.Width = 125;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Time";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 150;
            // 
            // RawEpgDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.listViewPayloads);
            this.Controls.Add(this.listViewDownloadedSegments);
            this.Controls.Add(this.textBoxDomainName);
            this.Controls.Add(this.labelDomainName);
            this.Controls.Add(this.groupBoxAbridgedEPG);
            this.Name = "RawEpgDownloaderForm";
            this.Text = "Raw EPG Downloader";
            this.Controls.SetChildIndex(this.buttonStart, 0);
            this.Controls.SetChildIndex(this.buttonStop, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.textIpAddress, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.labelBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.textBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.groupBoxAbridgedEPG, 0);
            this.Controls.SetChildIndex(this.labelDomainName, 0);
            this.Controls.SetChildIndex(this.textBoxDomainName, 0);
            this.Controls.SetChildIndex(this.listViewDownloadedSegments, 0);
            this.Controls.SetChildIndex(this.listViewPayloads, 0);
            this.groupBoxAbridgedEPG.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxAbridgedEPG;
        private System.Windows.Forms.TextBox textBoxDomainName;
        private System.Windows.Forms.Label labelDomainName;
        private UiServices.Common.Controls.ListViewSortable listViewDownloadedSegmentsAbridged;
        private System.Windows.Forms.Label labelDataReception;
        private UiServices.Common.Controls.ListViewSortable listViewDownloadedSegments;
        private UiServices.Common.Controls.ListViewSortable listViewPayloads;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.ImageList imageListPayloadStatus;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}