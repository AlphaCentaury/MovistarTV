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
    partial class EpgMiniGuideButtons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgMiniGuideButtons));
            this.SuspendLayout();
            // 
            // buttonRecordChannel
            // 
            resources.ApplyResources(this.buttonRecordChannel, "buttonRecordChannel");
            this.buttonRecordChannel.FlatAppearance.BorderSize = 0;
            this.buttonRecordChannel.UseVisualStyleBackColor = true;
            // 
            // buttonDisplayChannel
            // 
            resources.ApplyResources(this.buttonDisplayChannel, "buttonDisplayChannel");
            this.buttonDisplayChannel.FlatAppearance.BorderSize = 0;
            this.buttonDisplayChannel.UseVisualStyleBackColor = true;
            // 
            // buttonEpgGrid
            // 
            this.buttonEpgGrid.FlatAppearance.BorderSize = 0;
            // 
            // buttonDetails
            // 
            resources.ApplyResources(this.buttonDetails, "buttonDetails");
            this.buttonDetails.FlatAppearance.BorderSize = 0;
            // 
            // buttonFullView
            // 
            this.buttonFullView.FlatAppearance.BorderSize = 0;
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            // 
            // labelProgramTitle
            // 
            resources.ApplyResources(this.labelProgramTitle, "labelProgramTitle");
            // 
            // buttonForward
            // 
            this.buttonForward.FlatAppearance.BorderSize = 0;
            this.buttonForward.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.buttonForward, "buttonForward");
            // 
            // EpgMiniGuideButtons
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "EpgMiniGuideButtons";
            this.ResumeLayout(false);

        }

        #endregion
    } // class EpgMiniGuideButtons
} // namespace
