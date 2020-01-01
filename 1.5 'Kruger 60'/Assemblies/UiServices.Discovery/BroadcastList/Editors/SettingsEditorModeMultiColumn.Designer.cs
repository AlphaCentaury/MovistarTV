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
    partial class SettingsEditorModeMultiColumn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditorModeMultiColumn));
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.listSelectedColumns = new System.Windows.Forms.ListBox();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.comboColumns = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            this.groupBoxData.Controls.Add(this.buttonMoveDown);
            this.groupBoxData.Controls.Add(this.buttonMoveUp);
            this.groupBoxData.Controls.Add(this.buttonRemove);
            this.groupBoxData.Controls.Add(this.listSelectedColumns);
            this.groupBoxData.Controls.Add(this.buttonAddColumn);
            this.groupBoxData.Controls.Add(this.comboColumns);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.TabStop = false;
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.buttonMoveDown, "buttonMoveDown");
            this.buttonMoveDown.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_GoNextDown_16x16;
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.toolTip.SetToolTip(this.buttonMoveDown, resources.GetString("buttonMoveDown.ToolTip"));
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.buttonMoveUp, "buttonMoveUp");
            this.buttonMoveUp.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_GoPreviousUp_16x16;
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.toolTip.SetToolTip(this.buttonMoveUp, resources.GetString("buttonMoveUp.ToolTip"));
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.buttonRemove, "buttonRemove");
            this.buttonRemove.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_Delete_16x16;
            this.buttonRemove.Name = "buttonRemove";
            this.toolTip.SetToolTip(this.buttonRemove, resources.GetString("buttonRemove.ToolTip"));
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // listSelectedColumns
            // 
            this.listSelectedColumns.FormattingEnabled = true;
            resources.ApplyResources(this.listSelectedColumns, "listSelectedColumns");
            this.listSelectedColumns.Name = "listSelectedColumns";
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.buttonAddColumn, "buttonAddColumn");
            this.buttonAddColumn.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_Add_16xM;
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.toolTip.SetToolTip(this.buttonAddColumn, resources.GetString("buttonAddColumn.ToolTip"));
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
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
            // 
            // SettingsEditorModeMultiColumn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxData);
            this.Name = "SettingsEditorModeMultiColumn";
            this.Load += new System.EventHandler(this.SettingsEditorModeMultiColumn_Load);
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.ComboBox comboColumns;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ListBox listSelectedColumns;
        private System.Windows.Forms.Button buttonAddColumn;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
