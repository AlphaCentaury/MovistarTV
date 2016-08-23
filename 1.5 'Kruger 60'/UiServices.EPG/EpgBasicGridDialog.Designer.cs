// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.UiServices.EPG
{
    partial class EpgBasicGridDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgBasicGridDialog));
            this.dataGridPrograms = new System.Windows.Forms.DataGridView();
            this.columnChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramNow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramThen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonRecordChannel = new System.Windows.Forms.Button();
            this.buttonDisplayChannel = new System.Windows.Forms.Button();
            this.epgEventDisplay = new Project.IpTv.UiServices.EPG.EpgEventMiniBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPrograms
            // 
            this.dataGridPrograms.AllowUserToAddRows = false;
            this.dataGridPrograms.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridPrograms, "dataGridPrograms");
            this.dataGridPrograms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPrograms.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridPrograms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridPrograms.CausesValidation = false;
            this.dataGridPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnChannel,
            this.columnProgramNow,
            this.columnProgramThen,
            this.columnProgramAfter});
            this.dataGridPrograms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridPrograms.MultiSelect = false;
            this.dataGridPrograms.Name = "dataGridPrograms";
            this.dataGridPrograms.ReadOnly = true;
            this.dataGridPrograms.RowHeadersVisible = false;
            this.dataGridPrograms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridPrograms.ShowEditingIcon = false;
            this.dataGridPrograms.ShowRowErrors = false;
            this.dataGridPrograms.SelectionChanged += new System.EventHandler(this.dataGridPrograms_SelectionChanged);
            // 
            // columnChannel
            // 
            this.columnChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnChannel.FillWeight = 10F;
            this.columnChannel.Frozen = true;
            resources.ApplyResources(this.columnChannel, "columnChannel");
            this.columnChannel.Name = "columnChannel";
            this.columnChannel.ReadOnly = true;
            this.columnChannel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnProgramNow
            // 
            resources.ApplyResources(this.columnProgramNow, "columnProgramNow");
            this.columnProgramNow.Name = "columnProgramNow";
            this.columnProgramNow.ReadOnly = true;
            // 
            // columnProgramThen
            // 
            resources.ApplyResources(this.columnProgramThen, "columnProgramThen");
            this.columnProgramThen.Name = "columnProgramThen";
            this.columnProgramThen.ReadOnly = true;
            // 
            // columnProgramAfter
            // 
            resources.ApplyResources(this.columnProgramAfter, "columnProgramAfter");
            this.columnProgramAfter.Name = "columnProgramAfter";
            this.columnProgramAfter.ReadOnly = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::Project.IpTv.UiServices.EPG.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonRecordChannel
            // 
            resources.ApplyResources(this.buttonRecordChannel, "buttonRecordChannel");
            this.buttonRecordChannel.Image = global::Project.IpTv.UiServices.EPG.CommonUiResources.Action_Record_16x16;
            this.buttonRecordChannel.Name = "buttonRecordChannel";
            this.buttonRecordChannel.UseVisualStyleBackColor = true;
            this.buttonRecordChannel.Click += new System.EventHandler(this.buttonRecordChannel_Click);
            // 
            // buttonDisplayChannel
            // 
            resources.ApplyResources(this.buttonDisplayChannel, "buttonDisplayChannel");
            this.buttonDisplayChannel.Image = global::Project.IpTv.UiServices.EPG.CommonUiResources.Action_Play_LG_16x16;
            this.buttonDisplayChannel.Name = "buttonDisplayChannel";
            this.buttonDisplayChannel.UseVisualStyleBackColor = true;
            this.buttonDisplayChannel.Click += new System.EventHandler(this.buttonDisplayChannel_Click);
            // 
            // epgEventDisplay
            // 
            resources.ApplyResources(this.epgEventDisplay, "epgEventDisplay");
            this.epgEventDisplay.Name = "epgEventDisplay";
            // 
            // EpgBasicGridDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.epgEventDisplay);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonRecordChannel);
            this.Controls.Add(this.buttonDisplayChannel);
            this.Controls.Add(this.dataGridPrograms);
            this.MinimizeBox = false;
            this.Name = "EpgBasicGridDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EpgBasicGridDialog_Load);
            this.Shown += new System.EventHandler(this.EpgBasicGridDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPrograms;
        private System.Windows.Forms.Button buttonRecordChannel;
        private System.Windows.Forms.Button buttonDisplayChannel;
        private System.Windows.Forms.Button buttonOk;
        private EpgEventMiniBar epgEventDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramNow;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramThen;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramAfter;
    } // class EpgBasicGridDialog
} // namespace