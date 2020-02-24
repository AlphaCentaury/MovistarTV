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

namespace IpTviewr.UiServices.EPG
{
    partial class EpgNowThenDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgNowThenDialog));
            this.labelChannelName = new System.Windows.Forms.Label();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.EpgProgramBefore = new IpTviewr.UiServices.EPG.EpgProgramMiniBar();
            this.EpgProgramNow = new IpTviewr.UiServices.EPG.EpgProgramMiniBar();
            this.EpgProgramThen = new IpTviewr.UiServices.EPG.EpgProgramMiniBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.labelChannelName.AutoEllipsis = true;
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.UseMnemonic = false;
            // 
            // pictureChannelLogo
            // 
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // EpgProgramBefore
            // 
            resources.ApplyResources(this.EpgProgramBefore, "EpgProgramBefore");
            this.EpgProgramBefore.Name = "EpgProgramBefore";
            // 
            // EpgProgramNow
            // 
            resources.ApplyResources(this.EpgProgramNow, "EpgProgramNow");
            this.EpgProgramNow.Name = "EpgProgramNow";
            // 
            // EpgProgramThen
            // 
            resources.ApplyResources(this.EpgProgramThen, "EpgProgramThen");
            this.EpgProgramThen.Name = "EpgProgramThen";
            // 
            // FormEpgNowThen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EpgProgramThen);
            this.Controls.Add(this.EpgProgramNow);
            this.Controls.Add(this.EpgProgramBefore);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEpgNowThen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.FormBasicEpgData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.Label labelChannelName;
        private EpgProgramMiniBar EpgProgramBefore;
        private EpgProgramMiniBar EpgProgramNow;
        private EpgProgramMiniBar EpgProgramThen;
    } // class EpgNowThenDialog
} // namespace
