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
    partial class EpgChannelPrograms
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
            DisposeForm(disposing);

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
            this.labelChannelName = new System.Windows.Forms.Label();
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            this.listPrograms = new System.Windows.Forms.ListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDisplayChannel = new System.Windows.Forms.Button();
            this.buttonRecordChannel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.EpgProgramDetails = new IpTviewr.UiServices.EPG.EpgProgramMiniBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelChannelName
            // 
            this.labelChannelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChannelName.AutoEllipsis = true;
            this.labelChannelName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelChannelName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChannelName.Location = new System.Drawing.Point(66, 12);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(406, 24);
            this.labelChannelName.TabIndex = 19;
            this.labelChannelName.Text = "(Channel name)";
            this.labelChannelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelChannelName.UseMnemonic = false;
            // 
            // comboBoxDate
            // 
            this.comboBoxDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDate.FormattingEnabled = true;
            this.comboBoxDate.Items.AddRange(new object[] {
            "Hoy",
            "Mañana",
            "Pasado mañana",
            "Dentro de 3 días",
            "Dentro de 4 días",
            "Dentro de 5 días",
            "Dentro de 6 días"});
            this.comboBoxDate.Location = new System.Drawing.Point(66, 39);
            this.comboBoxDate.Name = "comboBoxDate";
            this.comboBoxDate.Size = new System.Drawing.Size(198, 21);
            this.comboBoxDate.TabIndex = 20;
            this.comboBoxDate.SelectedIndexChanged += new System.EventHandler(this.comboBoxDate_SelectedIndexChanged);
            // 
            // listPrograms
            // 
            this.listPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderTitle});
            this.listPrograms.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPrograms.FullRowSelect = true;
            this.listPrograms.GridLines = true;
            this.listPrograms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listPrograms.HideSelection = false;
            this.listPrograms.Location = new System.Drawing.Point(12, 66);
            this.listPrograms.MultiSelect = false;
            this.listPrograms.Name = "listPrograms";
            this.listPrograms.ShowGroups = false;
            this.listPrograms.ShowItemToolTips = true;
            this.listPrograms.Size = new System.Drawing.Size(460, 157);
            this.listPrograms.TabIndex = 21;
            this.listPrograms.UseCompatibleStateImageBehavior = false;
            this.listPrograms.View = System.Windows.Forms.View.Details;
            this.listPrograms.SelectedIndexChanged += new System.EventHandler(this.listPrograms_SelectedIndexChanged);
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Hora";
            this.columnHeaderTime.Width = 75;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Programa";
            // 
            // buttonDisplayChannel
            // 
            this.buttonDisplayChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayChannel.Enabled = false;
            this.buttonDisplayChannel.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Play_LG_16x16;
            this.buttonDisplayChannel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDisplayChannel.Location = new System.Drawing.Point(141, 325);
            this.buttonDisplayChannel.Name = "buttonDisplayChannel";
            this.buttonDisplayChannel.Size = new System.Drawing.Size(100, 25);
            this.buttonDisplayChannel.TabIndex = 29;
            this.buttonDisplayChannel.Text = "&Show...";
            this.buttonDisplayChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDisplayChannel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDisplayChannel.UseVisualStyleBackColor = true;
            this.buttonDisplayChannel.Click += new System.EventHandler(this.buttonDisplayChannel_Click);
            // 
            // buttonRecordChannel
            // 
            this.buttonRecordChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRecordChannel.Enabled = false;
            this.buttonRecordChannel.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Record_16x16;
            this.buttonRecordChannel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRecordChannel.Location = new System.Drawing.Point(247, 325);
            this.buttonRecordChannel.Name = "buttonRecordChannel";
            this.buttonRecordChannel.Size = new System.Drawing.Size(100, 25);
            this.buttonRecordChannel.TabIndex = 28;
            this.buttonRecordChannel.Text = "Rec&ord...";
            this.buttonRecordChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRecordChannel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRecordChannel.UseVisualStyleBackColor = true;
            this.buttonRecordChannel.Click += new System.EventHandler(this.buttonRecordChannel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOk.Location = new System.Drawing.Point(372, 325);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 27;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureChannelLogo
            // 
            this.pictureChannelLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureChannelLogo.Location = new System.Drawing.Point(12, 12);
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.Size = new System.Drawing.Size(48, 48);
            this.pictureChannelLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureChannelLogo.TabIndex = 18;
            this.pictureChannelLogo.TabStop = false;
            // 
            // EpgProgramDetails
            // 
            this.EpgProgramDetails.Location = new System.Drawing.Point(12, 229);
            this.EpgProgramDetails.Name = "EpgProgramDetails";
            this.EpgProgramDetails.Size = new System.Drawing.Size(460, 90);
            this.EpgProgramDetails.TabIndex = 30;
            // 
            // EpgChannelPrograms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.EpgProgramDetails);
            this.Controls.Add(this.buttonRecordChannel);
            this.Controls.Add(this.buttonDisplayChannel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listPrograms);
            this.Controls.Add(this.comboBoxDate);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Name = "EpgChannelPrograms";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EPG Channel programs";
            this.Load += new System.EventHandler(this.EpgChannelPrograms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.ComboBox comboBoxDate;
        private System.Windows.Forms.ListView listPrograms;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderTitle;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonRecordChannel;
        private System.Windows.Forms.Button buttonDisplayChannel;
        private EpgProgramMiniBar EpgProgramDetails;
    } // class EpgChannelPrograms
} // namespace
