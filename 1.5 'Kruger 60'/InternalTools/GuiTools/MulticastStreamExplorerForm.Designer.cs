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

namespace IpTviewr.Internal.Tools.GuiTools
{
    partial class MulticastStreamExplorerForm
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
            System.Windows.Forms.ColumnHeader columnHeaderSize;
            System.Windows.Forms.ColumnHeader columnHeaderTime;
            this.checkSaveDatagrams = new System.Windows.Forms.CheckBox();
            this.listViewDatagrams = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderFirstBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // columnHeaderSize
            // 
            columnHeaderSize.Text = "Size";
            columnHeaderSize.Width = 75;
            // 
            // columnHeaderTime
            // 
            columnHeaderTime.Text = "Time";
            columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnHeaderTime.Width = 125;
            // 
            // checkSaveDatagrams
            // 
            this.checkSaveDatagrams.AutoSize = true;
            this.checkSaveDatagrams.Location = new System.Drawing.Point(12, 45);
            this.checkSaveDatagrams.Name = "checkSaveDatagrams";
            this.checkSaveDatagrams.Size = new System.Drawing.Size(103, 17);
            this.checkSaveDatagrams.TabIndex = 20;
            this.checkSaveDatagrams.Text = "Save datagrams";
            this.checkSaveDatagrams.UseVisualStyleBackColor = true;
            this.checkSaveDatagrams.CheckedChanged += new System.EventHandler(this.checkDumpPayloads_CheckedChanged);
            // 
            // listViewDatagrams
            // 
            this.listViewDatagrams.AllowColumnReorder = true;
            this.listViewDatagrams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDatagrams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderSize,
            columnHeaderTime,
            this.columnHeaderFirstBytes});
            this.listViewDatagrams.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDatagrams.FullRowSelect = true;
            this.listViewDatagrams.GridLines = true;
            this.listViewDatagrams.HeaderCustomFont = null;
            this.listViewDatagrams.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewDatagrams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDatagrams.HideSelection = false;
            this.listViewDatagrams.IsDoubleBuffered = true;
            this.listViewDatagrams.Location = new System.Drawing.Point(12, 69);
            this.listViewDatagrams.MultiSelect = false;
            this.listViewDatagrams.Name = "listViewDatagrams";
            this.listViewDatagrams.Size = new System.Drawing.Size(660, 318);
            this.listViewDatagrams.TabIndex = 34;
            this.listViewDatagrams.UseCompatibleStateImageBehavior = false;
            this.listViewDatagrams.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderFirstBytes
            // 
            this.columnHeaderFirstBytes.Text = "First 64 bytes of data";
            this.columnHeaderFirstBytes.Width = 425;
            // 
            // MulticastStreamExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.listViewDatagrams);
            this.Controls.Add(this.checkSaveDatagrams);
            this.Name = "MulticastStreamExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Multicast Stream Explorer";
            this.Controls.SetChildIndex(this.checkSaveDatagrams, 0);
            this.Controls.SetChildIndex(this.listViewDatagrams, 0);
            this.Controls.SetChildIndex(this.buttonStart, 0);
            this.Controls.SetChildIndex(this.buttonStop, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.textIpAddress, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.labelBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.textBaseSaveFolder, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkSaveDatagrams;
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewDatagrams;
        private System.Windows.Forms.ColumnHeader columnHeaderFirstBytes;
    }
}
