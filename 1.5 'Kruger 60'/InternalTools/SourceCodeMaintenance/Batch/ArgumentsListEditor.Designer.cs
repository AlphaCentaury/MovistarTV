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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    partial class ArgumentsListEditor
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
            this.textBoxArgument = new System.Windows.Forms.TextBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.buttonSelectFile);
            this.groupBoxData.Controls.Add(this.textBoxArgument);
            this.groupBoxData.Location = new System.Drawing.Point(0, 3);
            this.groupBoxData.Size = new System.Drawing.Size(275, 264);
            this.groupBoxData.Text = "Tool execution arguments";
            this.groupBoxData.Controls.SetChildIndex(this.buttonEdit, 0);
            this.groupBoxData.Controls.SetChildIndex(this.listItems, 0);
            this.groupBoxData.Controls.SetChildIndex(this.buttonRemove, 0);
            this.groupBoxData.Controls.SetChildIndex(this.buttonMoveUp, 0);
            this.groupBoxData.Controls.SetChildIndex(this.buttonMoveDown, 0);
            this.groupBoxData.Controls.SetChildIndex(this.buttonAdd, 0);
            this.groupBoxData.Controls.SetChildIndex(this.textBoxArgument, 0);
            this.groupBoxData.Controls.SetChildIndex(this.buttonSelectFile, 0);
            // 
            // buttonAdd
            // 
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonAdd.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Add_16xM;
            this.buttonAdd.Location = new System.Drawing.Point(213, 19);
            this.toolTip.SetToolTip(this.buttonAdd, "Add new...");
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonMoveDown.Location = new System.Drawing.Point(244, 233);
            this.toolTip.SetToolTip(this.buttonMoveDown, "Move down");
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonMoveUp.Location = new System.Drawing.Point(244, 205);
            this.toolTip.SetToolTip(this.buttonMoveUp, "Move up");
            // 
            // buttonRemove
            // 
            this.buttonRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonRemove.Location = new System.Drawing.Point(244, 78);
            this.toolTip.SetToolTip(this.buttonRemove, "Delete");
            // 
            // listItems
            // 
            this.listItems.DisplayMember = "";
            this.listItems.Location = new System.Drawing.Point(6, 50);
            this.listItems.Size = new System.Drawing.Size(232, 208);
            this.listItems.ValueMember = "";
            // 
            // buttonEdit
            // 
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonEdit.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Edit_16x16;
            this.buttonEdit.Location = new System.Drawing.Point(244, 50);
            this.toolTip.SetToolTip(this.buttonEdit, "Edit selected argument");
            // 
            // textBoxArgument
            // 
            this.textBoxArgument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArgument.Location = new System.Drawing.Point(6, 22);
            this.textBoxArgument.Name = "textBoxArgument";
            this.textBoxArgument.Size = new System.Drawing.Size(170, 20);
            this.textBoxArgument.TabIndex = 6;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFile.FlatAppearance.BorderSize = 0;
            this.buttonSelectFile.Location = new System.Drawing.Point(182, 19);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(25, 25);
            this.buttonSelectFile.TabIndex = 7;
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // ArgumentsListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Items = new string[0];
            this.ListName = "Tool execution arguments";
            this.Name = "ArgumentsListEditor";
            this.Size = new System.Drawing.Size(275, 270);
            this.Load += new System.EventHandler(this.ArgumentsListEditor_Load);
            this.groupBoxData.ResumeLayout(false);
            this.groupBoxData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox textBoxArgument;
    }
}
