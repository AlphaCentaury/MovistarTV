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

namespace IpTviewr.UiServices.DvbStpClient
{
    partial class DvbStpEnhancedDownloadDialog
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DvbStpEnhancedDownloadDialog));
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader3;
            this.labelDownloadingPayloadName = new System.Windows.Forms.Label();
            this.labelDownloadSource = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgressPct = new System.Windows.Forms.Label();
            this.labelReceiving = new System.Windows.Forms.Label();
            this.labelDataReception = new System.Windows.Forms.Label();
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            this.labelEllapsedTime = new System.Windows.Forms.Label();
            this.timerEllapsed = new System.Windows.Forms.Timer(this.components);
            this.buttonRequestCancel = new System.Windows.Forms.Button();
            this.pictureDownloadIcon = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.listViewPayloads = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.imageListPayloadStatus = new System.Windows.Forms.ImageList(this.components);
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureDownloadIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            resources.ApplyResources(columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(columnHeader3, "columnHeader3");
            // 
            // labelDownloadingPayloadName
            // 
            this.labelDownloadingPayloadName.AutoEllipsis = true;
            resources.ApplyResources(this.labelDownloadingPayloadName, "labelDownloadingPayloadName");
            this.labelDownloadingPayloadName.Name = "labelDownloadingPayloadName";
            // 
            // labelDownloadSource
            // 
            this.labelDownloadSource.AutoEllipsis = true;
            resources.ApplyResources(this.labelDownloadSource, "labelDownloadSource");
            this.labelDownloadSource.Name = "labelDownloadSource";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.UseWaitCursor = true;
            // 
            // labelProgressPct
            // 
            resources.ApplyResources(this.labelProgressPct, "labelProgressPct");
            this.labelProgressPct.Name = "labelProgressPct";
            this.labelProgressPct.UseCompatibleTextRendering = true;
            this.labelProgressPct.UseWaitCursor = true;
            // 
            // labelReceiving
            // 
            resources.ApplyResources(this.labelReceiving, "labelReceiving");
            this.labelReceiving.Name = "labelReceiving";
            // 
            // labelDataReception
            // 
            resources.ApplyResources(this.labelDataReception, "labelDataReception");
            this.labelDataReception.Name = "labelDataReception";
            // 
            // timerClose
            // 
            this.timerClose.Interval = 500;
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // labelEllapsedTime
            // 
            resources.ApplyResources(this.labelEllapsedTime, "labelEllapsedTime");
            this.labelEllapsedTime.Name = "labelEllapsedTime";
            // 
            // timerEllapsed
            // 
            this.timerEllapsed.Interval = 1000;
            this.timerEllapsed.Tick += new System.EventHandler(this.timerEllapsed_Tick);
            // 
            // buttonRequestCancel
            // 
            this.buttonRequestCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRequestCancel.Image = global::IpTviewr.UiServices.DvbStpClient.Properties.Resources.Action_Cancel_Red_16x16;
            resources.ApplyResources(this.buttonRequestCancel, "buttonRequestCancel");
            this.buttonRequestCancel.Name = "buttonRequestCancel";
            this.buttonRequestCancel.UseVisualStyleBackColor = true;
            this.buttonRequestCancel.Click += new System.EventHandler(this.buttonRequestCancel_Click);
            // 
            // pictureDownloadIcon
            // 
            this.pictureDownloadIcon.Image = global::IpTviewr.UiServices.DvbStpClient.Properties.Resources.DvbStpDownload_128x128;
            resources.ApplyResources(this.pictureDownloadIcon, "pictureDownloadIcon");
            this.pictureDownloadIcon.Name = "pictureDownloadIcon";
            this.pictureDownloadIcon.TabStop = false;
            // 
            // listViewPayloads
            // 
            this.listViewPayloads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3});
            this.listViewPayloads.FullRowSelect = true;
            this.listViewPayloads.GridLines = true;
            this.listViewPayloads.HeaderCustomFont = null;
            this.listViewPayloads.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewPayloads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPayloads.HideSelection = false;
            this.listViewPayloads.IsDoubleBuffered = true;
            resources.ApplyResources(this.listViewPayloads, "listViewPayloads");
            this.listViewPayloads.Name = "listViewPayloads";
            this.listViewPayloads.SelfSorting = false;
            this.listViewPayloads.ShowItemToolTips = true;
            this.listViewPayloads.SmallImageList = this.imageListPayloadStatus;
            this.listViewPayloads.UseCompatibleStateImageBehavior = false;
            this.listViewPayloads.View = System.Windows.Forms.View.Details;
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
            // DvbStpEnhancedDownloadDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonRequestCancel;
            this.ControlBox = false;
            this.Controls.Add(this.listViewPayloads);
            this.Controls.Add(this.labelEllapsedTime);
            this.Controls.Add(this.pictureDownloadIcon);
            this.Controls.Add(this.buttonRequestCancel);
            this.Controls.Add(this.labelDataReception);
            this.Controls.Add(this.labelReceiving);
            this.Controls.Add(this.labelProgressPct);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelDownloadSource);
            this.Controls.Add(this.labelDownloadingPayloadName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvbStpEnhancedDownloadDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.Shown += new System.EventHandler(this.Dialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDownloadIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDownloadingPayloadName;
        private System.Windows.Forms.Label labelDownloadSource;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgressPct;
        private System.Windows.Forms.Label labelReceiving;
        private System.Windows.Forms.Label labelDataReception;
        private System.Windows.Forms.Button buttonRequestCancel;
        private System.Windows.Forms.Timer timerClose;
        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureDownloadIcon;
        private System.Windows.Forms.Label labelEllapsedTime;
        private System.Windows.Forms.Timer timerEllapsed;
        private IpTviewr.UiServices.Common.Controls.ListViewSortable listViewPayloads;
        private System.Windows.Forms.ImageList imageListPayloadStatus;
    } // class DvbStpEnhancedDownloadDialog
} // namespace
