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
            this.checkBoxOverrideLicense = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideTerms = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideCopyright = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideAuthors = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideProduct = new System.Windows.Forms.CheckBox();
            this.labelCheckOverride = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxOverrideRemarks = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideNotes = new System.Windows.Forms.CheckBox();
            this.groupBoxWrite = new System.Windows.Forms.GroupBox();
            this.checkBoxWriteSkipLicensingHtml = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteHtml = new System.Windows.Forms.CheckBox();
            this.labelWriteAdditional = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxCheck.SuspendLayout();
            this.groupBoxWrite.SuspendLayout();
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
            this.groupBoxCheck.Size = new System.Drawing.Size(175, 210);
            this.groupBoxCheck.TabIndex = 0;
            this.groupBoxCheck.TabStop = false;
            this.groupBoxCheck.Text = "Check";
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
            this.buttonCancel.Location = new System.Drawing.Point(288, 239);
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
            this.buttonOk.Location = new System.Drawing.Point(182, 239);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
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
            // groupBoxWrite
            // 
            this.groupBoxWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWrite.Controls.Add(this.label1);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteSkipLicensingHtml);
            this.groupBoxWrite.Controls.Add(this.checkBoxWriteHtml);
            this.groupBoxWrite.Controls.Add(this.labelWriteAdditional);
            this.groupBoxWrite.Location = new System.Drawing.Point(213, 12);
            this.groupBoxWrite.Name = "groupBoxWrite";
            this.groupBoxWrite.Size = new System.Drawing.Size(175, 210);
            this.groupBoxWrite.TabIndex = 1;
            this.groupBoxWrite.TabStop = false;
            this.groupBoxWrite.Text = "Write";
            // 
            // checkBoxWriteSkipLicensingHtml
            // 
            this.checkBoxWriteSkipLicensingHtml.AutoSize = true;
            this.checkBoxWriteSkipLicensingHtml.Location = new System.Drawing.Point(32, 89);
            this.checkBoxWriteSkipLicensingHtml.Name = "checkBoxWriteSkipLicensingHtml";
            this.checkBoxWriteSkipLicensingHtml.Size = new System.Drawing.Size(111, 17);
            this.checkBoxWriteSkipLicensingHtml.TabIndex = 3;
            this.checkBoxWriteSkipLicensingHtml.Text = "Licensing.xml html";
            this.checkBoxWriteSkipLicensingHtml.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteHtml
            // 
            this.checkBoxWriteHtml.AutoSize = true;
            this.checkBoxWriteHtml.Location = new System.Drawing.Point(32, 43);
            this.checkBoxWriteHtml.Name = "checkBoxWriteHtml";
            this.checkBoxWriteHtml.Size = new System.Drawing.Size(56, 17);
            this.checkBoxWriteHtml.TabIndex = 1;
            this.checkBoxWriteHtml.Text = "HTML";
            this.checkBoxWriteHtml.UseVisualStyleBackColor = true;
            // 
            // labelWriteAdditional
            // 
            this.labelWriteAdditional.AutoSize = true;
            this.labelWriteAdditional.Location = new System.Drawing.Point(6, 27);
            this.labelWriteAdditional.Name = "labelWriteAdditional";
            this.labelWriteAdditional.Size = new System.Drawing.Size(77, 13);
            this.labelWriteAdditional.TabIndex = 0;
            this.labelWriteAdditional.Text = "Additional files:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Do not write:";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoad.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Open_16x;
            this.buttonLoad.Location = new System.Drawing.Point(12, 239);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(40, 25);
            this.buttonLoad.TabIndex = 2;
            this.toolTip.SetToolTip(this.buttonLoad, "Load options from file...");
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.TextFile_16x;
            this.buttonSave.Location = new System.Drawing.Point(58, 239);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(40, 25);
            this.buttonSave.TabIndex = 3;
            this.toolTip.SetToolTip(this.buttonSave, "Save options to file...");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Delete_16x16;
            this.buttonClear.Location = new System.Drawing.Point(104, 239);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(40, 25);
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
            // LicensingToolOptionsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(400, 276);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.groupBoxWrite);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
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
        private System.Windows.Forms.CheckBox checkBoxWriteSkipLicensingHtml;
        private System.Windows.Forms.CheckBox checkBoxWriteHtml;
        private System.Windows.Forms.Label labelWriteAdditional;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}