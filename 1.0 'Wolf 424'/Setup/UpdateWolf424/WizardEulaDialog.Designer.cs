namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    partial class WizardEulaDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardEulaDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureWelcome = new System.Windows.Forms.PictureBox();
            this.labelEulaTitle = new System.Windows.Forms.Label();
            this.checkEulaOk = new System.Windows.Forms.CheckBox();
            this.richTextEula = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // buttonBack
            // 
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.No;
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // pictureWelcome
            // 
            resources.ApplyResources(this.pictureWelcome, "pictureWelcome");
            this.pictureWelcome.BackgroundImage = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.WizardSidePattern;
            this.pictureWelcome.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.WizardSide;
            this.pictureWelcome.Name = "pictureWelcome";
            this.pictureWelcome.TabStop = false;
            // 
            // labelEulaTitle
            // 
            resources.ApplyResources(this.labelEulaTitle, "labelEulaTitle");
            this.labelEulaTitle.Name = "labelEulaTitle";
            // 
            // checkEulaOk
            // 
            resources.ApplyResources(this.checkEulaOk, "checkEulaOk");
            this.checkEulaOk.Name = "checkEulaOk";
            this.checkEulaOk.UseVisualStyleBackColor = true;
            this.checkEulaOk.CheckedChanged += new System.EventHandler(this.checkEulaOk_CheckedChanged);
            // 
            // richTextEula
            // 
            resources.ApplyResources(this.richTextEula, "richTextEula");
            this.richTextEula.Name = "richTextEula";
            this.richTextEula.ReadOnly = true;
            // 
            // WizardEulaDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextEula);
            this.Controls.Add(this.checkEulaOk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureWelcome);
            this.Controls.Add(this.labelEulaTitle);
            this.MinimizeBox = false;
            this.Name = "WizardEulaDialog";
            this.Load += new System.EventHandler(this.WizardEulaDialog_Load);
            this.Shown += new System.EventHandler(this.WizardEulaDialog_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureWelcome;
        private System.Windows.Forms.Label labelEulaTitle;
        private System.Windows.Forms.CheckBox checkEulaOk;
        private System.Windows.Forms.RichTextBox richTextEula;
        private System.Windows.Forms.Button buttonBack;
    }
}