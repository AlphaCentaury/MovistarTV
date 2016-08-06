namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    partial class WizardEndDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardEndDialog));
            this.pictureWelcome = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelEndTitle = new System.Windows.Forms.Label();
            this.labelEndText = new System.Windows.Forms.Label();
            this.pictureEndIcon = new System.Windows.Forms.PictureBox();
            this.checkRunMainProgram = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEndIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureWelcome
            // 
            this.pictureWelcome.BackgroundImage = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.WizardSidePattern;
            this.pictureWelcome.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.WizardSide;
            resources.ApplyResources(this.pictureWelcome, "pictureWelcome");
            this.pictureWelcome.Name = "pictureWelcome";
            this.pictureWelcome.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panel1, "panel1");
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
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelEndTitle
            // 
            resources.ApplyResources(this.labelEndTitle, "labelEndTitle");
            this.labelEndTitle.Name = "labelEndTitle";
            // 
            // labelEndText
            // 
            resources.ApplyResources(this.labelEndText, "labelEndText");
            this.labelEndText.Name = "labelEndText";
            // 
            // pictureEndIcon
            // 
            resources.ApplyResources(this.pictureEndIcon, "pictureEndIcon");
            this.pictureEndIcon.Name = "pictureEndIcon";
            this.pictureEndIcon.TabStop = false;
            // 
            // checkRunMainProgram
            // 
            resources.ApplyResources(this.checkRunMainProgram, "checkRunMainProgram");
            this.checkRunMainProgram.BackColor = System.Drawing.SystemColors.Window;
            this.checkRunMainProgram.Name = "checkRunMainProgram";
            this.checkRunMainProgram.UseVisualStyleBackColor = false;
            // 
            // WizardEndDialog
            // 
            this.AcceptButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonNext;
            this.Controls.Add(this.checkRunMainProgram);
            this.Controls.Add(this.pictureEndIcon);
            this.Controls.Add(this.labelEndText);
            this.Controls.Add(this.labelEndTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardEndDialog";
            this.Load += new System.EventHandler(this.WizardEndDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEndIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureWelcome;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelEndTitle;
        private System.Windows.Forms.Label labelEndText;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.PictureBox pictureEndIcon;
        internal System.Windows.Forms.CheckBox checkRunMainProgram;
    }
}