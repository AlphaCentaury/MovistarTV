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

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers.Editors
{
    partial class TvPlayerEditorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TvPlayerEditorDialog));
            this.textPlayerName = new System.Windows.Forms.TextBox();
            this.textPlayerPath = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelectPlayer = new System.Windows.Forms.Button();
            this.picturePlayerIcon = new System.Windows.Forms.PictureBox();
            this.selectPlayerDialog = new System.Windows.Forms.OpenFileDialog();
            this.argumentsEditor = new IpTviewr.UiServices.Configuration.Editors.ArgumentsEditor();
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayerIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // textPlayerName
            // 
            resources.ApplyResources(this.textPlayerName, "textPlayerName");
            this.textPlayerName.Name = "textPlayerName";
            this.textPlayerName.TextChanged += new System.EventHandler(this.textPlayerName_TextChanged);
            // 
            // textPlayerPath
            // 
            resources.ApplyResources(this.textPlayerPath, "textPlayerPath");
            this.textPlayerPath.Name = "textPlayerPath";
            this.textPlayerPath.ReadOnly = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_Cancel_16x16;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelectPlayer
            // 
            resources.ApplyResources(this.buttonSelectPlayer, "buttonSelectPlayer");
            this.buttonSelectPlayer.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_AttachFile_16x16;
            this.buttonSelectPlayer.Name = "buttonSelectPlayer";
            this.buttonSelectPlayer.UseVisualStyleBackColor = true;
            this.buttonSelectPlayer.Click += new System.EventHandler(this.buttonSelectPlayer_Click);
            // 
            // picturePlayerIcon
            // 
            resources.ApplyResources(this.picturePlayerIcon, "picturePlayerIcon");
            this.picturePlayerIcon.Name = "picturePlayerIcon";
            this.picturePlayerIcon.TabStop = false;
            // 
            // selectPlayerDialog
            // 
            this.selectPlayerDialog.DefaultExt = "exe";
            resources.ApplyResources(this.selectPlayerDialog, "selectPlayerDialog");
            this.selectPlayerDialog.RestoreDirectory = true;
            this.selectPlayerDialog.SupportMultiDottedExtensions = true;
            // 
            // argumentsEditor
            // 
            resources.ApplyResources(this.argumentsEditor, "argumentsEditor");
            this.argumentsEditor.Arguments = null;
            this.argumentsEditor.Name = "argumentsEditor";
            // 
            // TvPlayerEditorDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.argumentsEditor);
            this.Controls.Add(this.buttonSelectPlayer);
            this.Controls.Add(this.textPlayerPath);
            this.Controls.Add(this.textPlayerName);
            this.Controls.Add(this.picturePlayerIcon);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MinimizeBox = false;
            this.Name = "TvPlayerEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TvPlayerEditorDialog_Load);
            this.Shown += new System.EventHandler(this.TvPlayerEditorDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayerIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox picturePlayerIcon;
        private System.Windows.Forms.TextBox textPlayerName;
        private System.Windows.Forms.TextBox textPlayerPath;
        private System.Windows.Forms.Button buttonSelectPlayer;
        private Configuration.Editors.ArgumentsEditor argumentsEditor;
        private System.Windows.Forms.OpenFileDialog selectPlayerDialog;
    }
}
