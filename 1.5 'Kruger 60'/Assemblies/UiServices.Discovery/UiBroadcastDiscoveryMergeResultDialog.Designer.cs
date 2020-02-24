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

namespace IpTviewr.UiServices.Discovery
{
    partial class UiBroadcastDiscoveryMergeResultDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiBroadcastDiscoveryMergeResultDialog));
            this.labelSuccess = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.labelEqual = new System.Windows.Forms.Label();
            this.labelBulletEqual = new System.Windows.Forms.Label();
            this.labelChanged = new System.Windows.Forms.Label();
            this.labelBulletChanged = new System.Windows.Forms.Label();
            this.labelRemoved = new System.Windows.Forms.Label();
            this.labelBulletRemoved = new System.Windows.Forms.Label();
            this.labelAdded = new System.Windows.Forms.Label();
            this.labelBulletAdded = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.pictureIconSuccess = new System.Windows.Forms.PictureBox();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSuccess
            // 
            resources.ApplyResources(this.labelSuccess, "labelSuccess");
            this.labelSuccess.Name = "labelSuccess";
            // 
            // groupBoxResults
            // 
            resources.ApplyResources(this.groupBoxResults, "groupBoxResults");
            this.groupBoxResults.Controls.Add(this.labelEqual);
            this.groupBoxResults.Controls.Add(this.labelBulletEqual);
            this.groupBoxResults.Controls.Add(this.labelChanged);
            this.groupBoxResults.Controls.Add(this.labelBulletChanged);
            this.groupBoxResults.Controls.Add(this.labelRemoved);
            this.groupBoxResults.Controls.Add(this.labelBulletRemoved);
            this.groupBoxResults.Controls.Add(this.labelAdded);
            this.groupBoxResults.Controls.Add(this.labelBulletAdded);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.TabStop = false;
            // 
            // labelEqual
            // 
            resources.ApplyResources(this.labelEqual, "labelEqual");
            this.labelEqual.Name = "labelEqual";
            // 
            // labelBulletEqual
            // 
            resources.ApplyResources(this.labelBulletEqual, "labelBulletEqual");
            this.labelBulletEqual.Name = "labelBulletEqual";
            // 
            // labelChanged
            // 
            resources.ApplyResources(this.labelChanged, "labelChanged");
            this.labelChanged.Name = "labelChanged";
            // 
            // labelBulletChanged
            // 
            resources.ApplyResources(this.labelBulletChanged, "labelBulletChanged");
            this.labelBulletChanged.Name = "labelBulletChanged";
            // 
            // labelRemoved
            // 
            resources.ApplyResources(this.labelRemoved, "labelRemoved");
            this.labelRemoved.Name = "labelRemoved";
            // 
            // labelBulletRemoved
            // 
            resources.ApplyResources(this.labelBulletRemoved, "labelBulletRemoved");
            this.labelBulletRemoved.Name = "labelBulletRemoved";
            // 
            // labelAdded
            // 
            resources.ApplyResources(this.labelAdded, "labelAdded");
            this.labelAdded.Name = "labelAdded";
            // 
            // labelBulletAdded
            // 
            resources.ApplyResources(this.labelBulletAdded, "labelBulletAdded");
            this.labelBulletAdded.Name = "labelBulletAdded";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_Ok_16x16;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonDetails
            // 
            resources.ApplyResources(this.buttonDetails, "buttonDetails");
            this.buttonDetails.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDetails.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_Details_16x16;
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.ButtonDetails_Click);
            // 
            // pictureIconSuccess
            // 
            this.pictureIconSuccess.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Status_Success_24x24;
            resources.ApplyResources(this.pictureIconSuccess, "pictureIconSuccess");
            this.pictureIconSuccess.Name = "pictureIconSuccess";
            this.pictureIconSuccess.TabStop = false;
            // 
            // UiBroadcastDiscoveryMergeResultDialog
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.labelSuccess);
            this.Controls.Add(this.pictureIconSuccess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UiBroadcastDiscoveryMergeResultDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.UiBroadcastDiscoveryMergeResultDialog_Load);
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconSuccess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureIconSuccess;
        private System.Windows.Forms.Label labelSuccess;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label labelEqual;
        private System.Windows.Forms.Label labelBulletEqual;
        private System.Windows.Forms.Label labelChanged;
        private System.Windows.Forms.Label labelBulletChanged;
        private System.Windows.Forms.Label labelRemoved;
        private System.Windows.Forms.Label labelBulletRemoved;
        private System.Windows.Forms.Label labelAdded;
        private System.Windows.Forms.Label labelBulletAdded;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDetails;
    } // class UiBroadcastDiscoveryMergeResultDialog
} // namespace
