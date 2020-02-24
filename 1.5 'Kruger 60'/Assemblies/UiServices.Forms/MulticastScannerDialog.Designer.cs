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

namespace IpTviewr.UiServices.Forms
{
    partial class MulticastScannerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MulticastScannerDialog));
            System.Windows.Forms.ColumnHeader columnHeader2;
            this.labelScanning = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgressPercentage = new System.Windows.Forms.Label();
            this.labelServiceUrl = new System.Windows.Forms.Label();
            this.labelServiceName = new System.Windows.Forms.Label();
            this.buttonRequestCancel = new System.Windows.Forms.Button();
            this.labelCaption = new System.Windows.Forms.Label();
            this.listViewStats = new System.Windows.Forms.ListView();
            this.labelEllapsedTime = new System.Windows.Forms.Label();
            this.timerEllapsed = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxServiceLogo = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.pictureBoxIcon = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.buttonClose = new System.Windows.Forms.Button();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServiceLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
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
            // labelScanning
            // 
            resources.ApplyResources(this.labelScanning, "labelScanning");
            this.labelScanning.Name = "labelScanning";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // labelProgressPercentage
            // 
            resources.ApplyResources(this.labelProgressPercentage, "labelProgressPercentage");
            this.labelProgressPercentage.Name = "labelProgressPercentage";
            // 
            // labelServiceUrl
            // 
            resources.ApplyResources(this.labelServiceUrl, "labelServiceUrl");
            this.labelServiceUrl.Name = "labelServiceUrl";
            // 
            // labelServiceName
            // 
            this.labelServiceName.AutoEllipsis = true;
            resources.ApplyResources(this.labelServiceName, "labelServiceName");
            this.labelServiceName.Name = "labelServiceName";
            this.labelServiceName.UseMnemonic = false;
            // 
            // buttonRequestCancel
            // 
            this.buttonRequestCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRequestCancel.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Cancel_16x16;
            resources.ApplyResources(this.buttonRequestCancel, "buttonRequestCancel");
            this.buttonRequestCancel.Name = "buttonRequestCancel";
            this.buttonRequestCancel.UseVisualStyleBackColor = true;
            this.buttonRequestCancel.Click += new System.EventHandler(this.buttonRequestCancel_Click);
            // 
            // labelCaption
            // 
            resources.ApplyResources(this.labelCaption, "labelCaption");
            this.labelCaption.Name = "labelCaption";
            // 
            // listViewStats
            // 
            resources.ApplyResources(this.listViewStats, "listViewStats");
            this.listViewStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.listViewStats.FullRowSelect = true;
            this.listViewStats.GridLines = true;
            this.listViewStats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewStats.MultiSelect = false;
            this.listViewStats.Name = "listViewStats";
            this.listViewStats.Scrollable = false;
            this.listViewStats.ShowGroups = false;
            this.listViewStats.UseCompatibleStateImageBehavior = false;
            this.listViewStats.View = System.Windows.Forms.View.Details;
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
            // pictureBoxServiceLogo
            // 
            this.pictureBoxServiceLogo.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pictureBoxServiceLogo, "pictureBoxServiceLogo");
            this.pictureBoxServiceLogo.Name = "pictureBoxServiceLogo";
            this.pictureBoxServiceLogo.TabStop = false;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::IpTviewr.UiServices.Forms.Properties.Resources.ScanTv_128x128;
            resources.ApplyResources(this.pictureBoxIcon, "pictureBoxIcon");
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.TabStop = false;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Ok_16x16;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // MulticastScannerDialog
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonRequestCancel;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelEllapsedTime);
            this.Controls.Add(this.listViewStats);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.buttonRequestCancel);
            this.Controls.Add(this.labelServiceUrl);
            this.Controls.Add(this.labelServiceName);
            this.Controls.Add(this.pictureBoxServiceLogo);
            this.Controls.Add(this.labelProgressPercentage);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelScanning);
            this.Controls.Add(this.pictureBoxIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MulticastScannerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogMulticastServiceScanner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DialogMulticastServiceScanner_FormClosed);
            this.Load += new System.EventHandler(this.DialogMulticastServiceScanner_Load);
            this.Shown += new System.EventHandler(this.DialogMulticastServiceScanner_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServiceLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureBoxIcon;
        private System.Windows.Forms.Label labelScanning;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgressPercentage;
        private System.Windows.Forms.Label labelServiceUrl;
        private System.Windows.Forms.Label labelServiceName;
        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureBoxServiceLogo;
        private System.Windows.Forms.Button buttonRequestCancel;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.ListView listViewStats;
        private System.Windows.Forms.Label labelEllapsedTime;
        private System.Windows.Forms.Timer timerEllapsed;
        private System.Windows.Forms.Button buttonClose;
    }
}
