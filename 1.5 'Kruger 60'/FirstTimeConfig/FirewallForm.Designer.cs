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

namespace IpTviewr.Tools.FirstTimeConfig
{
    partial class FirewallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirewallForm));
            this.groupFirewall = new System.Windows.Forms.GroupBox();
            this.labelSuccess = new System.Windows.Forms.Label();
            this.pictureBoxSuccess = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxFirewallVlc = new System.Windows.Forms.CheckBox();
            this.checkBoxFirewallDecoder = new System.Windows.Forms.CheckBox();
            this.buttonFirewall = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupFirewall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupFirewall
            // 
            this.groupFirewall.Controls.Add(this.labelSuccess);
            this.groupFirewall.Controls.Add(this.pictureBoxSuccess);
            this.groupFirewall.Controls.Add(this.pictureBox1);
            this.groupFirewall.Controls.Add(this.checkBoxFirewallVlc);
            this.groupFirewall.Controls.Add(this.checkBoxFirewallDecoder);
            resources.ApplyResources(this.groupFirewall, "groupFirewall");
            this.groupFirewall.Name = "groupFirewall";
            this.groupFirewall.TabStop = false;
            // 
            // labelSuccess
            // 
            resources.ApplyResources(this.labelSuccess, "labelSuccess");
            this.labelSuccess.Name = "labelSuccess";
            // 
            // pictureBoxSuccess
            // 
            this.pictureBoxSuccess.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.Sucess_16x16;
            resources.ApplyResources(this.pictureBoxSuccess, "pictureBoxSuccess");
            this.pictureBoxSuccess.Name = "pictureBoxSuccess";
            this.pictureBoxSuccess.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.Firewall_48x48;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // checkBoxFirewallVlc
            // 
            this.checkBoxFirewallVlc.AutoCheck = false;
            resources.ApplyResources(this.checkBoxFirewallVlc, "checkBoxFirewallVlc");
            this.checkBoxFirewallVlc.Name = "checkBoxFirewallVlc";
            this.checkBoxFirewallVlc.UseVisualStyleBackColor = true;
            // 
            // checkBoxFirewallDecoder
            // 
            this.checkBoxFirewallDecoder.AutoCheck = false;
            resources.ApplyResources(this.checkBoxFirewallDecoder, "checkBoxFirewallDecoder");
            this.checkBoxFirewallDecoder.Name = "checkBoxFirewallDecoder";
            this.checkBoxFirewallDecoder.UseVisualStyleBackColor = true;
            // 
            // buttonFirewall
            // 
            this.buttonFirewall.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionForward_Medium_16;
            resources.ApplyResources(this.buttonFirewall, "buttonFirewall");
            this.buttonFirewall.Name = "buttonFirewall";
            this.buttonFirewall.UseVisualStyleBackColor = true;
            this.buttonFirewall.Click += new System.EventHandler(this.buttonFirewall_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionCancel_16x16;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionOk_16x16;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // FirewallForm
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonFirewall);
            this.Controls.Add(this.groupFirewall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirewallForm";
            this.groupFirewall.ResumeLayout(false);
            this.groupFirewall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFirewall;
        private System.Windows.Forms.CheckBox checkBoxFirewallVlc;
        private System.Windows.Forms.CheckBox checkBoxFirewallDecoder;
        private System.Windows.Forms.Button buttonFirewall;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelSuccess;
        private System.Windows.Forms.PictureBox pictureBoxSuccess;
    }
}
