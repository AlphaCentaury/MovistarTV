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

namespace IpTviewr.UiServices.Record
{
    partial class RecordProgramOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordProgramOptions));
            this.labelProgramDescription = new System.Windows.Forms.Label();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.pictureChannelLogo = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.labelProgramSchedule = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.pictureIconInfo = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.radioRecordChannel = new System.Windows.Forms.RadioButton();
            this.radioRecordProgramEdit = new System.Windows.Forms.RadioButton();
            this.radioRecordProgramDefault = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProgramDescription
            // 
            resources.ApplyResources(this.labelProgramDescription, "labelProgramDescription");
            this.labelProgramDescription.AutoEllipsis = true;
            this.labelProgramDescription.Name = "labelProgramDescription";
            this.labelProgramDescription.UseMnemonic = false;
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
            this.pictureChannelLogo.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // labelProgramSchedule
            // 
            resources.ApplyResources(this.labelProgramSchedule, "labelProgramSchedule");
            this.labelProgramSchedule.AutoEllipsis = true;
            this.labelProgramSchedule.Name = "labelProgramSchedule";
            this.labelProgramSchedule.UseMnemonic = false;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelInfo);
            this.groupBox1.Controls.Add(this.pictureIconInfo);
            this.groupBox1.Controls.Add(this.radioRecordChannel);
            this.groupBox1.Controls.Add(this.radioRecordProgramEdit);
            this.groupBox1.Controls.Add(this.radioRecordProgramDefault);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelInfo
            // 
            resources.ApplyResources(this.labelInfo, "labelInfo");
            this.labelInfo.Name = "labelInfo";
            // 
            // pictureIconInfo
            // 
            resources.ApplyResources(this.pictureIconInfo, "pictureIconInfo");
            this.pictureIconInfo.Name = "pictureIconInfo";
            this.pictureIconInfo.TabStop = false;
            // 
            // radioRecordChannel
            // 
            resources.ApplyResources(this.radioRecordChannel, "radioRecordChannel");
            this.radioRecordChannel.Name = "radioRecordChannel";
            this.radioRecordChannel.TabStop = true;
            this.radioRecordChannel.UseVisualStyleBackColor = true;
            this.radioRecordChannel.CheckedChanged += new System.EventHandler(this.radioRecordChannel_CheckedChanged);
            // 
            // radioRecordProgramEdit
            // 
            resources.ApplyResources(this.radioRecordProgramEdit, "radioRecordProgramEdit");
            this.radioRecordProgramEdit.Name = "radioRecordProgramEdit";
            this.radioRecordProgramEdit.TabStop = true;
            this.radioRecordProgramEdit.UseVisualStyleBackColor = true;
            this.radioRecordProgramEdit.CheckedChanged += new System.EventHandler(this.radioRecordProgramEdit_CheckedChanged);
            // 
            // radioRecordProgramDefault
            // 
            resources.ApplyResources(this.radioRecordProgramDefault, "radioRecordProgramDefault");
            this.radioRecordProgramDefault.Name = "radioRecordProgramDefault";
            this.radioRecordProgramDefault.TabStop = true;
            this.radioRecordProgramDefault.UseVisualStyleBackColor = true;
            this.radioRecordProgramDefault.CheckedChanged += new System.EventHandler(this.radioRecordProgramDefault_CheckedChanged);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.UiServices.Record.Properties.Resources.Action_Cancel_16x16;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // RecordProgramOptions
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelProgramSchedule);
            this.Controls.Add(this.labelProgramDescription);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Name = "RecordProgramOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RecordProgramOptions_FormClosed);
            this.Load += new System.EventHandler(this.RecordProgramOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelProgramDescription;
        private System.Windows.Forms.Label labelChannelName;
        private Common.Controls.PictureBoxEx pictureChannelLogo;
        private System.Windows.Forms.Label labelProgramSchedule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioRecordChannel;
        private System.Windows.Forms.RadioButton radioRecordProgramEdit;
        private System.Windows.Forms.RadioButton radioRecordProgramDefault;
        private System.Windows.Forms.Label labelInfo;
        private Common.Controls.PictureBoxEx pictureIconInfo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}
