// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.UiServices.Configuration.Settings.Network.Editors
{
    partial class NetworkSettingsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkSettingsEditor));
            this.groupBoxMulticastProxy = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.checkBoxEnableMulticastProxy = new System.Windows.Forms.CheckBox();
            this.parametersEditorMulticastProxy = new Project.IpTv.UiServices.Configuration.Editors.ArgumentEditor();
            this.pictureIconWarning = new System.Windows.Forms.PictureBox();
            this.groupBoxMulticastProxy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMulticastProxy
            // 
            resources.ApplyResources(this.groupBoxMulticastProxy, "groupBoxMulticastProxy");
            this.groupBoxMulticastProxy.Controls.Add(this.labelWarning);
            this.groupBoxMulticastProxy.Controls.Add(this.pictureIconWarning);
            this.groupBoxMulticastProxy.Controls.Add(this.parametersEditorMulticastProxy);
            this.groupBoxMulticastProxy.Controls.Add(this.checkBoxEnableMulticastProxy);
            this.groupBoxMulticastProxy.Name = "groupBoxMulticastProxy";
            this.groupBoxMulticastProxy.TabStop = false;
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.Name = "labelWarning";
            // 
            // checkBoxEnableMulticastProxy
            // 
            resources.ApplyResources(this.checkBoxEnableMulticastProxy, "checkBoxEnableMulticastProxy");
            this.checkBoxEnableMulticastProxy.Name = "checkBoxEnableMulticastProxy";
            this.checkBoxEnableMulticastProxy.UseVisualStyleBackColor = true;
            this.checkBoxEnableMulticastProxy.CheckedChanged += new System.EventHandler(this.checkBoxEnableMulticastProxy_CheckedChanged);
            // 
            // parametersEditorMulticastProxy
            // 
            resources.ApplyResources(this.parametersEditorMulticastProxy, "parametersEditorMulticastProxy");
            this.parametersEditorMulticastProxy.CloseBraceText = null;
            this.parametersEditorMulticastProxy.CommandLine = "";
            this.parametersEditorMulticastProxy.Name = "parametersEditorMulticastProxy";
            this.parametersEditorMulticastProxy.OpenBraceText = null;
            this.parametersEditorMulticastProxy.ParametersList = null;
            this.parametersEditorMulticastProxy.CommandLineChanged += new System.EventHandler(this.parametersEditorMulticastProxy_CommandLineChanged);
            // 
            // pictureIconWarning
            // 
            resources.ApplyResources(this.pictureIconWarning, "pictureIconWarning");
            this.pictureIconWarning.Image = global::Project.IpTv.UiServices.Configuration.CommonUiResources.Status_Warning_16x16;
            this.pictureIconWarning.Name = "pictureIconWarning";
            this.pictureIconWarning.TabStop = false;
            // 
            // NetworkSettingsEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMulticastProxy);
            this.Name = "NetworkSettingsEditor";
            this.Load += new System.EventHandler(this.NetworkSettingsEditor_Load);
            this.groupBoxMulticastProxy.ResumeLayout(false);
            this.groupBoxMulticastProxy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMulticastProxy;
        private System.Windows.Forms.CheckBox checkBoxEnableMulticastProxy;
        private Configuration.Editors.ArgumentEditor parametersEditorMulticastProxy;
        private System.Windows.Forms.PictureBox pictureIconWarning;
        private System.Windows.Forms.Label labelWarning;
    }
}
