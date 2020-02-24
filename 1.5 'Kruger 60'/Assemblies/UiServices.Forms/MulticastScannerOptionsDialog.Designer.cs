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
    partial class MulticastScannerOptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MulticastScannerOptionsDialog));
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelScanTimeout = new System.Windows.Forms.Label();
            this.numericTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupScanWhat = new System.Windows.Forms.GroupBox();
            this.radioScanDead = new System.Windows.Forms.RadioButton();
            this.radioScanActive = new System.Windows.Forms.RadioButton();
            this.radioScanAll = new System.Windows.Forms.RadioButton();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonRequestCancel = new System.Windows.Forms.Button();
            this.pictureIcon = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
            this.groupScanWhat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            resources.ApplyResources(this.labelCaption, "labelCaption");
            this.labelCaption.Name = "labelCaption";
            // 
            // labelScanTimeout
            // 
            resources.ApplyResources(this.labelScanTimeout, "labelScanTimeout");
            this.labelScanTimeout.Name = "labelScanTimeout";
            // 
            // numericTimeout
            // 
            this.numericTimeout.DecimalPlaces = 3;
            this.numericTimeout.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericTimeout, "numericTimeout");
            this.numericTimeout.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTimeout.Name = "numericTimeout";
            this.numericTimeout.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // buttonStart
            // 
            resources.ApplyResources(this.buttonStart, "buttonStart");
            this.buttonStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonStart.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Run_16x16;
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupScanWhat
            // 
            this.groupScanWhat.Controls.Add(this.radioScanDead);
            this.groupScanWhat.Controls.Add(this.radioScanActive);
            this.groupScanWhat.Controls.Add(this.radioScanAll);
            resources.ApplyResources(this.groupScanWhat, "groupScanWhat");
            this.groupScanWhat.Name = "groupScanWhat";
            this.groupScanWhat.TabStop = false;
            // 
            // radioScanDead
            // 
            resources.ApplyResources(this.radioScanDead, "radioScanDead");
            this.radioScanDead.Name = "radioScanDead";
            this.radioScanDead.UseVisualStyleBackColor = false;
            // 
            // radioScanActive
            // 
            resources.ApplyResources(this.radioScanActive, "radioScanActive");
            this.radioScanActive.Name = "radioScanActive";
            this.radioScanActive.UseVisualStyleBackColor = false;
            // 
            // radioScanAll
            // 
            resources.ApplyResources(this.radioScanAll, "radioScanAll");
            this.radioScanAll.Checked = true;
            this.radioScanAll.Name = "radioScanAll";
            this.radioScanAll.TabStop = true;
            this.radioScanAll.UseVisualStyleBackColor = false;
            // 
            // labelInfo
            // 
            resources.ApplyResources(this.labelInfo, "labelInfo");
            this.labelInfo.Name = "labelInfo";
            // 
            // buttonRequestCancel
            // 
            resources.ApplyResources(this.buttonRequestCancel, "buttonRequestCancel");
            this.buttonRequestCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRequestCancel.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Cancel_16x16;
            this.buttonRequestCancel.Name = "buttonRequestCancel";
            this.buttonRequestCancel.UseVisualStyleBackColor = true;
            // 
            // pictureIcon
            // 
            this.pictureIcon.Image = global::IpTviewr.UiServices.Forms.Properties.Resources.ScanTv_128x128;
            resources.ApplyResources(this.pictureIcon, "pictureIcon");
            this.pictureIcon.Name = "pictureIcon";
            this.pictureIcon.TabStop = false;
            // 
            // MulticastScannerOptionsDialog
            // 
            this.AcceptButton = this.buttonStart;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonRequestCancel;
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.groupScanWhat);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRequestCancel);
            this.Controls.Add(this.numericTimeout);
            this.Controls.Add(this.labelScanTimeout);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.pictureIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MulticastScannerOptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
            this.groupScanWhat.ResumeLayout(false);
            this.groupScanWhat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureIcon;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Label labelScanTimeout;
        private System.Windows.Forms.NumericUpDown numericTimeout;
        private System.Windows.Forms.Button buttonRequestCancel;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupScanWhat;
        private System.Windows.Forms.RadioButton radioScanDead;
        private System.Windows.Forms.RadioButton radioScanActive;
        private System.Windows.Forms.RadioButton radioScanAll;
        private System.Windows.Forms.Label labelInfo;
    }
}
