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
            this.groupBoxCheck = new System.Windows.Forms.GroupBox();
            this.checkBoxOverrideLicense = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideTerms = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideCopyright = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideAuthors = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideProduct = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxCheck.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCheck
            // 
            this.groupBoxCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideLicense);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideTerms);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideCopyright);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideAuthors);
            this.groupBoxCheck.Controls.Add(this.checkBoxOverrideProduct);
            this.groupBoxCheck.Controls.Add(this.label1);
            this.groupBoxCheck.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCheck.Name = "groupBoxCheck";
            this.groupBoxCheck.Size = new System.Drawing.Size(204, 171);
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
            this.checkBoxOverrideTerms.Size = new System.Drawing.Size(55, 17);
            this.checkBoxOverrideTerms.TabIndex = 4;
            this.checkBoxOverrideTerms.Text = "Terms";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Override values:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Cancel_16x16;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(116, 201);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 25);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Ok_16x16;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(10, 201);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // LicensingToolOptionsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(228, 238);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCheck;
        private System.Windows.Forms.CheckBox checkBoxOverrideLicense;
        private System.Windows.Forms.CheckBox checkBoxOverrideTerms;
        private System.Windows.Forms.CheckBox checkBoxOverrideCopyright;
        private System.Windows.Forms.CheckBox checkBoxOverrideAuthors;
        private System.Windows.Forms.CheckBox checkBoxOverrideProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}