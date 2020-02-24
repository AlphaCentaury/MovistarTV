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
    partial class TvPlayersSettingsEditor
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TvPlayersSettingsEditor));
            this.groupDefaultPlayer = new System.Windows.Forms.GroupBox();
            this.labelDefaultPlayer = new System.Windows.Forms.Label();
            this.groupPlayers = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSetDefault = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.pictureIconInfo = new System.Windows.Forms.PictureBox();
            this.checkBoxShortcut = new System.Windows.Forms.CheckBox();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupDefaultPlayer.SuspendLayout();
            this.groupPlayers.SuspendLayout();
            this.groupOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDefaultPlayer
            // 
            resources.ApplyResources(this.groupDefaultPlayer, "groupDefaultPlayer");
            this.groupDefaultPlayer.Controls.Add(this.labelDefaultPlayer);
            this.groupDefaultPlayer.Name = "groupDefaultPlayer";
            this.groupDefaultPlayer.TabStop = false;
            // 
            // labelDefaultPlayer
            // 
            resources.ApplyResources(this.labelDefaultPlayer, "labelDefaultPlayer");
            this.labelDefaultPlayer.AutoEllipsis = true;
            this.labelDefaultPlayer.Name = "labelDefaultPlayer";
            // 
            // groupPlayers
            // 
            resources.ApplyResources(this.groupPlayers, "groupPlayers");
            this.groupPlayers.Controls.Add(this.buttonAdd);
            this.groupPlayers.Controls.Add(this.buttonSetDefault);
            this.groupPlayers.Controls.Add(this.buttonDelete);
            this.groupPlayers.Controls.Add(this.buttonEdit);
            this.groupPlayers.Controls.Add(this.listViewPlayers);
            this.groupPlayers.Name = "groupPlayers";
            this.groupPlayers.TabStop = false;
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.CausesValidation = false;
            this.buttonAdd.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_Add_16xM;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSetDefault
            // 
            resources.ApplyResources(this.buttonSetDefault, "buttonSetDefault");
            this.buttonSetDefault.CausesValidation = false;
            this.buttonSetDefault.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Status_Ok_SmallCircle_16x16;
            this.buttonSetDefault.Name = "buttonSetDefault";
            this.buttonSetDefault.UseVisualStyleBackColor = true;
            this.buttonSetDefault.Click += new System.EventHandler(this.buttonSetDefault_Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.CausesValidation = false;
            this.buttonDelete.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_Delete_16x16;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Action_Properties_16x16;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // listViewPlayers
            // 
            resources.ApplyResources(this.listViewPlayers, "listViewPlayers");
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            this.listViewPlayers.FullRowSelect = true;
            this.listViewPlayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewPlayers.HideSelection = false;
            this.listViewPlayers.MultiSelect = false;
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.ShowItemToolTips = true;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Tile;
            this.listViewPlayers.SelectedIndexChanged += new System.EventHandler(this.listViewPlayers_SelectedIndexChanged);
            this.listViewPlayers.DoubleClick += new System.EventHandler(this.listViewPlayers_DoubleClick);
            // 
            // groupOptions
            // 
            resources.ApplyResources(this.groupOptions, "groupOptions");
            this.groupOptions.Controls.Add(this.labelInfo);
            this.groupOptions.Controls.Add(this.pictureIconInfo);
            this.groupOptions.Controls.Add(this.checkBoxShortcut);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.TabStop = false;
            // 
            // labelInfo
            // 
            resources.ApplyResources(this.labelInfo, "labelInfo");
            this.labelInfo.Name = "labelInfo";
            // 
            // pictureIconInfo
            // 
            this.pictureIconInfo.Image = global::IpTviewr.UiServices.Configuration.CommonUiResources.Status_Info_16x16;
            resources.ApplyResources(this.pictureIconInfo, "pictureIconInfo");
            this.pictureIconInfo.Name = "pictureIconInfo";
            this.pictureIconInfo.TabStop = false;
            // 
            // checkBoxShortcut
            // 
            resources.ApplyResources(this.checkBoxShortcut, "checkBoxShortcut");
            this.checkBoxShortcut.Name = "checkBoxShortcut";
            this.checkBoxShortcut.UseVisualStyleBackColor = true;
            this.checkBoxShortcut.CheckedChanged += new System.EventHandler(this.checkBoxShortcut_CheckedChanged);
            // 
            // TvPlayersSettingsEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.groupPlayers);
            this.Controls.Add(this.groupDefaultPlayer);
            this.Name = "TvPlayersSettingsEditor";
            this.Load += new System.EventHandler(this.TvPlayersSettingsEditor_Load);
            this.groupDefaultPlayer.ResumeLayout(false);
            this.groupPlayers.ResumeLayout(false);
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDefaultPlayer;
        private System.Windows.Forms.Label labelDefaultPlayer;
        private System.Windows.Forms.GroupBox groupPlayers;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.CheckBox checkBoxShortcut;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.PictureBox pictureIconInfo;
        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.Button buttonSetDefault;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
    } // class TvPlayersSettingsEditor
} // namespace
