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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridPrograms = new System.Windows.Forms.DataGridView();
            this.columnChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramNow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramThen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProgramAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.epgMiniGuide = new IpTviewr.UiServices.EPG.EpgMiniGuideButtons();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPrograms
            // 
            this.dataGridPrograms.AllowUserToAddRows = false;
            this.dataGridPrograms.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridPrograms, "dataGridPrograms");
            this.dataGridPrograms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPrograms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridPrograms.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridPrograms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridPrograms.RowsDefaultCellStyle = dataGridViewCellStyle2;
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
            this.buttonOk.Image = global::IpTviewr.UiServices.EPG.Properties.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // epgMiniGuide
            // 
            resources.ApplyResources(this.epgMiniGuide, "epgMiniGuide");
            this.epgMiniGuide.AutoRefresh = false;
            this.epgMiniGuide.BackColor = System.Drawing.SystemColors.Control;
            this.epgMiniGuide.BasicGridEnabled = false;
            this.epgMiniGuide.DetailsEnabled = false;
            this.epgMiniGuide.IsDisabled = false;
            this.epgMiniGuide.ManualActions = false;
            this.epgMiniGuide.Name = "epgMiniGuide";
            // 
            // EpgBasicGridDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.epgMiniGuide);
            this.Controls.Add(this.dataGridPrograms);
            this.MinimizeBox = false;
            this.Name = "EpgBasicGridDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EpgBasicGridDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPrograms;
        private System.Windows.Forms.Button buttonOk;
        private IpTviewr.UiServices.EPG.EpgMiniGuideButtons epgMiniGuide;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramNow;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramThen;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProgramAfter;
    } // class EpgBasicGridDialog
} // namespace
