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
    partial class EpgProgramMiniBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgProgramMiniBar));
            this.buttonProgramProperties = new System.Windows.Forms.Button();
            this.pictureProgramThumbnail = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.labelProgramDetails = new System.Windows.Forms.Label();
            this.labelProgramTitle = new System.Windows.Forms.Label();
            this.labelProgramTime = new System.Windows.Forms.Label();
            this.labelProgramCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonProgramProperties
            // 
            this.buttonProgramProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonProgramProperties.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonProgramProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonProgramProperties.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonProgramProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProgramProperties.Image = ((System.Drawing.Image)(resources.GetObject("buttonProgramProperties.Image")));
            this.buttonProgramProperties.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonProgramProperties.Location = new System.Drawing.Point(91, 61);
            this.buttonProgramProperties.Name = "buttonProgramProperties";
            this.buttonProgramProperties.Size = new System.Drawing.Size(25, 25);
            this.buttonProgramProperties.TabIndex = 41;
            this.buttonProgramProperties.UseVisualStyleBackColor = false;
            this.buttonProgramProperties.Click += new System.EventHandler(this.buttonProgramProperties_Click);
            // 
            // pictureProgramThumbnail
            // 
            this.pictureProgramThumbnail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureProgramThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureProgramThumbnail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureProgramThumbnail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureProgramThumbnail.Location = new System.Drawing.Point(0, 0);
            this.pictureProgramThumbnail.Name = "pictureProgramThumbnail";
            this.pictureProgramThumbnail.Size = new System.Drawing.Size(120, 90);
            this.pictureProgramThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureProgramThumbnail.TabIndex = 40;
            this.pictureProgramThumbnail.TabStop = false;
            this.pictureProgramThumbnail.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureProgramThumbnail_LoadCompleted);
            this.pictureProgramThumbnail.Click += new System.EventHandler(this.pictureProgramThumbnail_Click);
            // 
            // labelProgramDetails
            // 
            this.labelProgramDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgramDetails.AutoEllipsis = true;
            this.labelProgramDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProgramDetails.Location = new System.Drawing.Point(126, 55);
            this.labelProgramDetails.Name = "labelProgramDetails";
            this.labelProgramDetails.Size = new System.Drawing.Size(274, 35);
            this.labelProgramDetails.TabIndex = 39;
            this.labelProgramDetails.Text = "(Details)";
            // 
            // labelProgramTitle
            // 
            this.labelProgramTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgramTitle.AutoEllipsis = true;
            this.labelProgramTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelProgramTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProgramTitle.Location = new System.Drawing.Point(126, 34);
            this.labelProgramTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.labelProgramTitle.Name = "labelProgramTitle";
            this.labelProgramTitle.Size = new System.Drawing.Size(274, 16);
            this.labelProgramTitle.TabIndex = 38;
            this.labelProgramTitle.Text = "(Program title)";
            // 
            // labelProgramTime
            // 
            this.labelProgramTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgramTime.AutoEllipsis = true;
            this.labelProgramTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProgramTime.Location = new System.Drawing.Point(126, 16);
            this.labelProgramTime.Name = "labelProgramTime";
            this.labelProgramTime.Size = new System.Drawing.Size(274, 13);
            this.labelProgramTime.TabIndex = 37;
            this.labelProgramTime.Text = "(Start time)";
            // 
            // labelProgramCaption
            // 
            this.labelProgramCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProgramCaption.AutoSize = true;
            this.labelProgramCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.labelProgramCaption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProgramCaption.Location = new System.Drawing.Point(126, 0);
            this.labelProgramCaption.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.labelProgramCaption.Name = "labelProgramCaption";
            this.labelProgramCaption.Size = new System.Drawing.Size(61, 13);
            this.labelProgramCaption.TabIndex = 36;
            this.labelProgramCaption.Text = "(Program)";
            // 
            // EpgProgramMiniBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonProgramProperties);
            this.Controls.Add(this.pictureProgramThumbnail);
            this.Controls.Add(this.labelProgramDetails);
            this.Controls.Add(this.labelProgramTitle);
            this.Controls.Add(this.labelProgramTime);
            this.Controls.Add(this.labelProgramCaption);
            this.Name = "EpgProgramMiniBar";
            this.Size = new System.Drawing.Size(400, 90);
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramThumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProgramProperties;
        private Common.Controls.PictureBoxEx pictureProgramThumbnail;
        private System.Windows.Forms.Label labelProgramDetails;
        private System.Windows.Forms.Label labelProgramTitle;
        private System.Windows.Forms.Label labelProgramTime;
        private System.Windows.Forms.Label labelProgramCaption;
    } // class EpgProgramMiniBar
} // namespace
