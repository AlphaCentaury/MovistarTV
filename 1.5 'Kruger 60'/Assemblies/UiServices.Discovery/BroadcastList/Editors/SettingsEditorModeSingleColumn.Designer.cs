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

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    partial class SettingsEditorModeSingleColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditorModeSingleColumn));
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.comboColumns = new System.Windows.Forms.ComboBox();
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            this.groupBoxData.Controls.Add(this.comboColumns);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.TabStop = false;
            // 
            // comboColumns
            // 
            this.comboColumns.DisplayMember = "Value";
            this.comboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColumns.DropDownWidth = 250;
            this.comboColumns.FormattingEnabled = true;
            resources.ApplyResources(this.comboColumns, "comboColumns");
            this.comboColumns.Name = "comboColumns";
            this.comboColumns.ValueMember = "Key";
            this.comboColumns.SelectedIndexChanged += new System.EventHandler(this.comboColumns_SelectedIndexChanged);
            // 
            // SettingsEditorModeSingleColumn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxData);
            this.Name = "SettingsEditorModeSingleColumn";
            this.Load += new System.EventHandler(this.SettingsEditorModeMultiColumn_Load);
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.ComboBox comboColumns;
    }
} // namespace
