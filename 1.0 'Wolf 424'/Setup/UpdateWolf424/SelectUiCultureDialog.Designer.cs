namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    partial class SelectUiCultureDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectUiCultureDialog));
            this.radioSpanish = new System.Windows.Forms.RadioButton();
            this.radioEnglish = new System.Windows.Forms.RadioButton();
            this.labelSelect = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioSpanish
            // 
            resources.ApplyResources(this.radioSpanish, "radioSpanish");
            this.radioSpanish.Name = "radioSpanish";
            this.radioSpanish.UseVisualStyleBackColor = true;
            this.radioSpanish.CheckedChanged += new System.EventHandler(this.radioSpanish_CheckedChanged);
            // 
            // radioEnglish
            // 
            resources.ApplyResources(this.radioEnglish, "radioEnglish");
            this.radioEnglish.Name = "radioEnglish";
            this.radioEnglish.UseVisualStyleBackColor = true;
            this.radioEnglish.CheckedChanged += new System.EventHandler(this.radioEnglish_CheckedChanged);
            // 
            // labelSelect
            // 
            resources.ApplyResources(this.labelSelect, "labelSelect");
            this.labelSelect.Name = "labelSelect";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // SelectUiCultureDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelSelect);
            this.Controls.Add(this.radioEnglish);
            this.Controls.Add(this.radioSpanish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectUiCultureDialog";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioSpanish;
        private System.Windows.Forms.RadioButton radioEnglish;
        private System.Windows.Forms.Label labelSelect;
        private System.Windows.Forms.Button buttonOk;
    }
}