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
    partial class EpgExtendedInfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgExtendedInfoDialog));
            this.labelChannelName = new System.Windows.Forms.Label();
            this.richTextProgramData = new System.Windows.Forms.RichTextBox();
            this.contextMenuRtf = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextRtfMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRtfMenuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonRecordProgram = new System.Windows.Forms.Button();
            this.buttonShowProgram = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.pictureProgramPreview = new System.Windows.Forms.PictureBox();
            this.labelAdditionalDetails = new System.Windows.Forms.Label();
            this.contextMenuRtf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.labelChannelName.Name = "labelChannelName";
            // 
            // richTextProgramData
            // 
            resources.ApplyResources(this.richTextProgramData, "richTextProgramData");
            this.richTextProgramData.AutoWordSelection = true;
            this.richTextProgramData.BackColor = System.Drawing.SystemColors.Window;
            this.richTextProgramData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextProgramData.ContextMenuStrip = this.contextMenuRtf;
            this.richTextProgramData.HideSelection = false;
            this.richTextProgramData.Name = "richTextProgramData";
            this.richTextProgramData.ReadOnly = true;
            // 
            // contextMenuRtf
            // 
            this.contextMenuRtf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextRtfMenuCopy,
            this.contextRtfMenuSelectAll});
            this.contextMenuRtf.Name = "contextMenuRtf";
            resources.ApplyResources(this.contextMenuRtf, "contextMenuRtf");
            this.contextMenuRtf.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuRtf_Opening);
            // 
            // contextRtfMenuCopy
            // 
            this.contextRtfMenuCopy.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Copy_Clip_16x16;
            this.contextRtfMenuCopy.Name = "contextRtfMenuCopy";
            resources.ApplyResources(this.contextRtfMenuCopy, "contextRtfMenuCopy");
            this.contextRtfMenuCopy.Click += new System.EventHandler(this.contextRtfMenuCopy_Click);
            // 
            // contextRtfMenuSelectAll
            // 
            this.contextRtfMenuSelectAll.Name = "contextRtfMenuSelectAll";
            resources.ApplyResources(this.contextRtfMenuSelectAll, "contextRtfMenuSelectAll");
            this.contextRtfMenuSelectAll.Click += new System.EventHandler(this.contextRtfMenuSelectAll_Click);
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Forward_16x16;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            resources.ApplyResources(this.buttonPrevious, "buttonPrevious");
            this.buttonPrevious.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Back_16x16;
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonRecordProgram
            // 
            resources.ApplyResources(this.buttonRecordProgram, "buttonRecordProgram");
            this.buttonRecordProgram.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Record_16x16;
            this.buttonRecordProgram.Name = "buttonRecordProgram";
            this.buttonRecordProgram.UseVisualStyleBackColor = true;
            this.buttonRecordProgram.Click += new System.EventHandler(this.buttonRecordProgram_Click);
            // 
            // buttonShowProgram
            // 
            resources.ApplyResources(this.buttonShowProgram, "buttonShowProgram");
            this.buttonShowProgram.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Play_LG_16x16;
            this.buttonShowProgram.Name = "buttonShowProgram";
            this.buttonShowProgram.UseVisualStyleBackColor = true;
            this.buttonShowProgram.Click += new System.EventHandler(this.buttonShowProgram_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureChannelLogo
            // 
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // buttonZoom
            // 
            resources.ApplyResources(this.buttonZoom, "buttonZoom");
            this.buttonZoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonZoom.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_FullView_16x16;
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.UseVisualStyleBackColor = true;
            // 
            // pictureProgramPreview
            // 
            resources.ApplyResources(this.pictureProgramPreview, "pictureProgramPreview");
            this.pictureProgramPreview.ErrorImage = global::IpTviewr.UiServices.EPG.Properties.Resources.EpgNoProgramImage;
            this.pictureProgramPreview.Image = global::IpTviewr.UiServices.EPG.Properties.Resources.EpgLoadingProgramImage;
            this.pictureProgramPreview.Name = "pictureProgramPreview";
            this.pictureProgramPreview.TabStop = false;
            this.pictureProgramPreview.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureProgramPreview_LoadCompleted);
            // 
            // labelAdditionalDetails
            // 
            resources.ApplyResources(this.labelAdditionalDetails, "labelAdditionalDetails");
            this.labelAdditionalDetails.Name = "labelAdditionalDetails";
            // 
            // EpgExtendedInfoDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.labelAdditionalDetails);
            this.Controls.Add(this.richTextProgramData);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonRecordProgram);
            this.Controls.Add(this.buttonShowProgram);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.buttonZoom);
            this.Controls.Add(this.pictureProgramPreview);
            this.MinimizeBox = false;
            this.Name = "EpgExtendedInfoDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EpgExtendedInfoDialog_Load);
            this.Shown += new System.EventHandler(this.EpgExtendedInfoDialog_Shown);
            this.contextMenuRtf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureProgramPreview;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.Button buttonRecordProgram;
        private System.Windows.Forms.Button buttonShowProgram;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.RichTextBox richTextProgramData;
        private System.Windows.Forms.ContextMenuStrip contextMenuRtf;
        private System.Windows.Forms.ToolStripMenuItem contextRtfMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem contextRtfMenuSelectAll;
        private System.Windows.Forms.Label labelAdditionalDetails;
    } // class EpgExtendedInfoDialog
} // namespace
