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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingToolOptionsDialog
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
            this.groupBoxCheck = new System.Windows.Forms.GroupBox();
            this.checkBoxOverrideNotes = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideRemarks = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideLicense = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideTerms = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideCopyright = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideAuthors = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideProduct = new System.Windows.Forms.CheckBox();
            this.labelCheckOverride = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxWrite = new System.Windows.Forms.GroupBox();
            this.checkBoxWriteTranslatedRtf = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteTranslatedHtml = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteTranslatedMarkdown = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteTranslatedPlainText = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxWriteLicensingRtf = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteRtf = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteHtml = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteMarkdown = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxWriteLicensingHtml = new System.Windows.Forms.CheckBox();
            this.checkBoxWritePlainText = new System.Windows.Forms.CheckBox();
            this.labelWriteAdditional = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.comboBoxWriteScope = new System.Windows.Forms.ComboBox();
            this.groupBoxWriteTransform = new System.Windows.Forms.GroupBox();
            this.checkBoxWriteDeleteOld = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxCheck.SuspendLayout();
            this.groupBoxWrite.SuspendLayout();
            this.groupBoxWriteTransform.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCheck
            // 
            this.groupBoxCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideNotes);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideRemarks);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideLicense);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideTerms);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideCopyright);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideAuthors);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideProduct);
            this.groupBoxCheck.Controls.Add(this.labelCheckOverride);
            this.groupBoxCheck.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCheck.Name = "groupBoxCheck";
            this.groupBoxCheck.Size = new System.Drawing.Size(175, 236);
            this.groupBoxCheck.TabIndex = 0;
            this.groupBoxCheck.TabStop = false;
            this.groupBoxCheck.Text = "Check";
            // 
            // checkBoxOverrideNotes
            // 
            this.checkBoxOverrideNotes.AutoSize = true;
            this.checkBoxOverrideNotes.Location = new System.Drawing.Point(32, 181);
            this.checkBoxOverrideNotes.Name = "checkBoxOverrideNotes";
            this.checkBoxOverrideNotes.Size = new System.Drawing.Size(54, 17);
            this.checkBoxOverrideNotes.TabIndex = 7;
            this.checkBoxOverrideNotes.Text = "Notes";
            this.checkBoxOverrideNotes.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideRemarks
            // 
            this.checkBoxOverrideRemarks.AutoSize = true;
            this.checkBoxOverrideRemarks.Location = new System.Drawing.Point(32, 158);
            this.checkBoxOverrideRemarks.Name = "checkBoxOverrideRemarks";
            this.checkBoxOverrideRemarks.Size = new System.Drawing.Size(68, 17);
            this.checkBoxOverrideRemarks.TabIndex = 6;
            this.checkBoxOverrideRemarks.Text = "Remarks";
            this.checkBoxOverrideRemarks.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideLicense
            // 
            this.checkBoxOverrideLicense.AutoSize = true;
            this.checkBoxOverrideLicense.Location = new System.Drawing.Point(32, 135);
            this.checkBoxOverrideLicense.Name = "checkBoxOverrideLicense";
            this.checkBoxOverrideLicense.Size = new System.Drawing.Size(63, 17);
            this.checkBoxOverrideLicense.TabIndex = 5;
            this.checkBoxOverrideLicense.Text = "License";
            this.checkBoxOverrideLicense.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideTerms
            // 
            this.checkBoxOverrideTerms.AutoSize = true;
            this.checkBoxOverrideTerms.Location = new System.Drawing.Point(32, 112);
            this.checkBoxOverrideTerms.Name = "checkBoxOverrideTerms";
            this.checkBoxOverrideTerms.Size = new System.Drawing.Size(127, 17);
            this.checkBoxOverrideTerms.TabIndex = 4;
            this.checkBoxOverrideTerms.Text = "Terms and conditions";
            this.checkBoxOverrideTerms.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideCopyright
            // 
            this.checkBoxOverrideCopyright.AutoSize = true;
            this.checkBoxOverrideCopyright.Location = new System.Drawing.Point(32, 89);
            this.checkBoxOverrideCopyright.Name = "checkBoxOverrideCopyright";
            this.checkBoxOverrideCopyright.Size = new System.Drawing.Size(70, 17);
            this.checkBoxOverrideCopyright.TabIndex = 3;
            this.checkBoxOverrideCopyright.Text = "Copyright";
            this.checkBoxOverrideCopyright.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideAuthors
            // 
            this.checkBoxOverrideAuthors.AutoSize = true;
            this.checkBoxOverrideAuthors.Location = new System.Drawing.Point(32, 66);
            this.checkBoxOverrideAuthors.Name = "checkBoxOverrideAuthors";
            this.checkBoxOverrideAuthors.Size = new System.Drawing.Size(62, 17);
            this.checkBoxOverrideAuthors.TabIndex = 2;
            this.checkBoxOverrideAuthors.Text = "Authors";
            this.checkBoxOverrideAuthors.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverrideProduct
            // 
            this.checkBoxOverrideProduct.AutoSize = true;
            this.checkBoxOverrideProduct.Location = new System.Drawing.Point(32, 43);
            this.checkBoxOverrideProduct.Name = "checkBoxOverrideProduct";
            this.checkBoxOverrideProduct.Size = new System.Drawing.Size(63, 17);
            this.checkBoxOverrideProduct.TabIndex = 1;
            this.checkBoxOverrideProduct.Text = "Product";
            this.checkBoxOverrideProduct.UseVisualStyleBackColor = true;
            // 
            // labelCheckOverride
            // 
            this.labelCheckOverride.AutoSize = true;
            this.labelCheckOverride.Location = new System.Drawing.Point(6, 27);
            this.labelCheckOverride.Name = "labelCheckOverride";
            this.labelCheckOverride.Size = new System.Drawing.Size(84, 13);
            this.labelCheckOverride.TabIndex = 0;
            this.labelCheckOverride.Text = "Override values:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Cancel_16x16;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(363, 265);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 25);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Ok_16x16;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(257, 265);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBoxWrite
            // 
            this.groupBoxWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWrite.Controls.Add(this.label3);
            this.groupBoxWrite.Controls.Add(this.comboBoxWriteScope);
            this.groupBoxWrite.Controls.Add(this.labelWriteAdditional);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteTranslatedRtf);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteDeleteOld);
            this.groupBoxWrite.Controls.Add(this.checkBoxWritePlainText);
            this.groupBoxWrite.Controls.Add(this.groupBoxWriteTransform);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteTranslatedHtml);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteMarkdown);
            this.groupBoxWrite.Controls.Add(this.label1);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteTranslatedMarkdown);
            this.groupBoxWrite.Controls.Add(this.label2);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteHtml);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteRtf);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteTranslatedPlainText);
            this.groupBoxWrite.Location = new System.Drawing.Point(213, 12);
            this.groupBoxWrite.Name = "groupBoxWrite";
            this.groupBoxWrite.Size = new System.Drawing.Size(250, 236);
            this.groupBoxWrite.TabIndex = 1;
            this.groupBoxWrite.TabStop = false;
            this.groupBoxWrite.Text = "Write";
            // 
            // checkBoxWriteTranslatedRtf
            // 
            this.checkBoxWriteTranslatedRtf.AutoSize = true;
            this.checkBoxWriteTranslatedRtf.Location = new System.Drawing.Point(138, 132);
            this.checkBoxWriteTranslatedRtf.Name = "checkBoxWriteTranslatedRtf";
            this.checkBoxWriteTranslatedRtf.Size = new System.Drawing.Size(47, 17);
            this.checkBoxWriteTranslatedRtf.TabIndex = 12;
            this.checkBoxWriteTranslatedRtf.Text = "RTF";
            this.checkBoxWriteTranslatedRtf.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteTranslatedHtml
            // 
            this.checkBoxWriteTranslatedHtml.AutoSize = true;
            this.checkBoxWriteTranslatedHtml.Location = new System.Drawing.Point(138, 109);
            this.checkBoxWriteTranslatedHtml.Name = "checkBoxWriteTranslatedHtml";
            this.checkBoxWriteTranslatedHtml.Size = new System.Drawing.Size(56, 17);
            this.checkBoxWriteTranslatedHtml.TabIndex = 11;
            this.checkBoxWriteTranslatedHtml.Text = "HTML";
            this.checkBoxWriteTranslatedHtml.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteTranslatedMarkdown
            // 
            this.checkBoxWriteTranslatedMarkdown.AutoSize = true;
            this.checkBoxWriteTranslatedMarkdown.Location = new System.Drawing.Point(138, 86);
            this.checkBoxWriteTranslatedMarkdown.Name = "checkBoxWriteTranslatedMarkdown";
            this.checkBoxWriteTranslatedMarkdown.Size = new System.Drawing.Size(76, 17);
            this.checkBoxWriteTranslatedMarkdown.TabIndex = 10;
            this.checkBoxWriteTranslatedMarkdown.Text = "Markdown";
            this.checkBoxWriteTranslatedMarkdown.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteTranslatedPlainText
            // 
            this.checkBoxWriteTranslatedPlainText.AutoSize = true;
            this.checkBoxWriteTranslatedPlainText.Location = new System.Drawing.Point(138, 63);
            this.checkBoxWriteTranslatedPlainText.Name = "checkBoxWriteTranslatedPlainText";
            this.checkBoxWriteTranslatedPlainText.Size = new System.Drawing.Size(69, 17);
            this.checkBoxWriteTranslatedPlainText.TabIndex = 9;
            this.checkBoxWriteTranslatedPlainText.Text = "Plain text";
            this.checkBoxWriteTranslatedPlainText.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Translated terms:";
            // 
            // checkBoxWriteLicensingRtf
            // 
            this.checkBoxWriteLicensingRtf.AutoSize = true;
            this.checkBoxWriteLicensingRtf.Location = new System.Drawing.Point(138, 19);
            this.checkBoxWriteLicensingRtf.Name = "checkBoxWriteLicensingRtf";
            this.checkBoxWriteLicensingRtf.Size = new System.Drawing.Size(47, 17);
            this.checkBoxWriteLicensingRtf.TabIndex = 7;
            this.checkBoxWriteLicensingRtf.Text = "RTF";
            this.checkBoxWriteLicensingRtf.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteRtf
            // 
            this.checkBoxWriteRtf.AutoSize = true;
            this.checkBoxWriteRtf.Location = new System.Drawing.Point(32, 132);
            this.checkBoxWriteRtf.Name = "checkBoxWriteRtf";
            this.checkBoxWriteRtf.Size = new System.Drawing.Size(47, 17);
            this.checkBoxWriteRtf.TabIndex = 6;
            this.checkBoxWriteRtf.Text = "RTF";
            this.checkBoxWriteRtf.UseVisualStyleBackColor = true;
            this.checkBoxWriteRtf.CheckedChanged += new System.EventHandler(this.checkBoxWriteRtf_CheckedChanged);
            // 
            // checkBoxWriteHtml
            // 
            this.checkBoxWriteHtml.AutoSize = true;
            this.checkBoxWriteHtml.Location = new System.Drawing.Point(32, 109);
            this.checkBoxWriteHtml.Name = "checkBoxWriteHtml";
            this.checkBoxWriteHtml.Size = new System.Drawing.Size(56, 17);
            this.checkBoxWriteHtml.TabIndex = 5;
            this.checkBoxWriteHtml.Text = "HTML";
            this.checkBoxWriteHtml.UseVisualStyleBackColor = true;
            this.checkBoxWriteHtml.CheckedChanged += new System.EventHandler(this.checkBoxWriteHtml_CheckedChanged);
            // 
            // checkBoxWriteMarkdown
            // 
            this.checkBoxWriteMarkdown.AutoSize = true;
            this.checkBoxWriteMarkdown.Location = new System.Drawing.Point(32, 86);
            this.checkBoxWriteMarkdown.Name = "checkBoxWriteMarkdown";
            this.checkBoxWriteMarkdown.Size = new System.Drawing.Size(76, 17);
            this.checkBoxWriteMarkdown.TabIndex = 4;
            this.checkBoxWriteMarkdown.Text = "Markdown";
            this.checkBoxWriteMarkdown.UseVisualStyleBackColor = true;
            this.checkBoxWriteMarkdown.CheckedChanged += new System.EventHandler(this.checkBoxWriteMarkdown_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // checkBoxWriteLicensingHtml
            // 
            this.checkBoxWriteLicensingHtml.AutoSize = true;
            this.checkBoxWriteLicensingHtml.Location = new System.Drawing.Point(23, 19);
            this.checkBoxWriteLicensingHtml.Name = "checkBoxWriteLicensingHtml";
            this.checkBoxWriteLicensingHtml.Size = new System.Drawing.Size(56, 17);
            this.checkBoxWriteLicensingHtml.TabIndex = 3;
            this.checkBoxWriteLicensingHtml.Text = "HTML";
            this.checkBoxWriteLicensingHtml.UseVisualStyleBackColor = true;
            // 
            // checkBoxWritePlainText
            // 
            this.checkBoxWritePlainText.AutoSize = true;
            this.checkBoxWritePlainText.Location = new System.Drawing.Point(32, 63);
            this.checkBoxWritePlainText.Name = "checkBoxWritePlainText";
            this.checkBoxWritePlainText.Size = new System.Drawing.Size(69, 17);
            this.checkBoxWritePlainText.TabIndex = 1;
            this.checkBoxWritePlainText.Text = "Plain text";
            this.checkBoxWritePlainText.UseVisualStyleBackColor = true;
            this.checkBoxWritePlainText.CheckedChanged += new System.EventHandler(this.checkBoxWritePlainText_CheckedChanged);
            // 
            // labelWriteAdditional
            // 
            this.labelWriteAdditional.AutoSize = true;
            this.labelWriteAdditional.Location = new System.Drawing.Point(6, 47);
            this.labelWriteAdditional.Name = "labelWriteAdditional";
            this.labelWriteAdditional.Size = new System.Drawing.Size(83, 13);
            this.labelWriteAdditional.TabIndex = 0;
            this.labelWriteAdditional.Text = "Licensing terms:";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoad.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Open_16x;
            this.buttonLoad.Location = new System.Drawing.Point(12, 265);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(50, 25);
            this.buttonLoad.TabIndex = 2;
            this.toolTip.SetToolTip(this.buttonLoad, "Load options from file...");
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.TextFile_16x;
            this.buttonSave.Location = new System.Drawing.Point(68, 265);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 25);
            this.buttonSave.TabIndex = 3;
            this.toolTip.SetToolTip(this.buttonSave, "Save options to file...");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Delete_16x16;
            this.buttonClear.Location = new System.Drawing.Point(124, 265);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(50, 25);
            this.buttonClear.TabIndex = 4;
            this.toolTip.SetToolTip(this.buttonClear, "Clear all options");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "LicensingTool.options.xml";
            this.openFileDialog.Filter = "Licensing tool options|*.options.xml";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "LicensingTool.options.xml";
            this.saveFileDialog.Filter = "Licensing tool options|*.options.xml";
            // 
            // comboBoxWriteScope
            // 
            this.comboBoxWriteScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWriteScope.FormattingEnabled = true;
            this.comboBoxWriteScope.Items.AddRange(new object[] {
            "Licensing data",
            "Solution licensing"});
            this.comboBoxWriteScope.Location = new System.Drawing.Point(103, 19);
            this.comboBoxWriteScope.Name = "comboBoxWriteScope";
            this.comboBoxWriteScope.Size = new System.Drawing.Size(141, 21);
            this.comboBoxWriteScope.TabIndex = 14;
            this.comboBoxWriteScope.SelectedIndexChanged += new System.EventHandler(this.comboBoxWriteScope_SelectedIndexChanged);
            // 
            // groupBoxWriteTransform
            // 
            this.groupBoxWriteTransform.Controls.Add(this.checkBoxWriteLicensingHtml);
            this.groupBoxWriteTransform.Controls.Add(this.checkBoxWriteLicensingRtf);
            this.groupBoxWriteTransform.Location = new System.Drawing.Point(9, 155);
            this.groupBoxWriteTransform.Name = "groupBoxWriteTransform";
            this.groupBoxWriteTransform.Size = new System.Drawing.Size(222, 47);
            this.groupBoxWriteTransform.TabIndex = 14;
            this.groupBoxWriteTransform.TabStop = false;
            this.groupBoxWriteTransform.Text = "Transform licensing data:";
            // 
            // checkBoxWriteDeleteOld
            // 
            this.checkBoxWriteDeleteOld.AutoSize = true;
            this.checkBoxWriteDeleteOld.Location = new System.Drawing.Point(9, 208);
            this.checkBoxWriteDeleteOld.Name = "checkBoxWriteDeleteOld";
            this.checkBoxWriteDeleteOld.Size = new System.Drawing.Size(95, 17);
            this.checkBoxWriteDeleteOld.TabIndex = 15;
            this.checkBoxWriteDeleteOld.Text = "Delete old files";
            this.checkBoxWriteDeleteOld.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Options for writing";
            // 
            // LicensingToolOptionsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(475, 302);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxWrite);
            this.Controls.Add(this.groupBoxCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicensingToolOptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Licensing tool options";
            this.groupBoxCheck.ResumeLayout(false);
            this.groupBoxCheck.PerformLayout();
            this.groupBoxWrite.ResumeLayout(false);
            this.groupBoxWrite.PerformLayout();
            this.groupBoxWriteTransform.ResumeLayout(false);
            this.groupBoxWriteTransform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCheck;
        private System.Windows.Forms.CheckBox checkBoxOverrideLicense;
        private System.Windows.Forms.CheckBox checkBoxOverrideTerms;
        private System.Windows.Forms.CheckBox checkBoxOverrideCopyright;
        private System.Windows.Forms.CheckBox checkBoxOverrideAuthors;
        private System.Windows.Forms.CheckBox checkBoxOverrideProduct;
        private System.Windows.Forms.Label labelCheckOverride;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckBox checkBoxOverrideNotes;
        private System.Windows.Forms.CheckBox checkBoxOverrideRemarks;
        private System.Windows.Forms.GroupBox groupBoxWrite;
        private System.Windows.Forms.CheckBox checkBoxWriteLicensingHtml;
        private System.Windows.Forms.CheckBox checkBoxWritePlainText;
        private System.Windows.Forms.Label labelWriteAdditional;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox checkBoxWriteTranslatedRtf;
        private System.Windows.Forms.CheckBox checkBoxWriteTranslatedHtml;
        private System.Windows.Forms.CheckBox checkBoxWriteTranslatedMarkdown;
        private System.Windows.Forms.CheckBox checkBoxWriteTranslatedPlainText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxWriteLicensingRtf;
        private System.Windows.Forms.CheckBox checkBoxWriteRtf;
        private System.Windows.Forms.CheckBox checkBoxWriteHtml;
        private System.Windows.Forms.CheckBox checkBoxWriteMarkdown;
        private System.Windows.Forms.CheckBox checkBoxWriteDeleteOld;
        private System.Windows.Forms.GroupBox groupBoxWriteTransform;
        private System.Windows.Forms.ComboBox comboBoxWriteScope;
        private System.Windows.Forms.Label label3;
    }
}
