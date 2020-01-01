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
    partial class SettingsEditorSorting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditorSorting));
            this.groupSortBy = new System.Windows.Forms.GroupBox();
            this.checkGlobalSorting = new System.Windows.Forms.CheckBox();
            this.buttonThirdDirection = new System.Windows.Forms.Button();
            this.comboThirdColumn = new System.Windows.Forms.ComboBox();
            this.buttonSecondDirection = new System.Windows.Forms.Button();
            this.comboSecondColumn = new System.Windows.Forms.ComboBox();
            this.buttonFirstDirection = new System.Windows.Forms.Button();
            this.comboFirstColumn = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupSortBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupSortBy
            // 
            this.groupSortBy.Controls.Add(this.checkGlobalSorting);
            this.groupSortBy.Controls.Add(this.buttonThirdDirection);
            this.groupSortBy.Controls.Add(this.comboThirdColumn);
            this.groupSortBy.Controls.Add(this.buttonSecondDirection);
            this.groupSortBy.Controls.Add(this.comboSecondColumn);
            this.groupSortBy.Controls.Add(this.buttonFirstDirection);
            this.groupSortBy.Controls.Add(this.comboFirstColumn);
            resources.ApplyResources(this.groupSortBy, "groupSortBy");
            this.groupSortBy.Name = "groupSortBy";
            this.groupSortBy.TabStop = false;
            // 
            // checkGlobalSorting
            // 
            resources.ApplyResources(this.checkGlobalSorting, "checkGlobalSorting");
            this.checkGlobalSorting.Name = "checkGlobalSorting";
            this.checkGlobalSorting.UseVisualStyleBackColor = true;
            this.checkGlobalSorting.CheckedChanged += new System.EventHandler(this.checkGlobalSorting_CheckedChanged);
            // 
            // buttonThirdDirection
            // 
            resources.ApplyResources(this.buttonThirdDirection, "buttonThirdDirection");
            this.buttonThirdDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonThirdDirection.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonThirdDirection.Name = "buttonThirdDirection";
            this.buttonThirdDirection.UseVisualStyleBackColor = true;
            this.buttonThirdDirection.Click += new System.EventHandler(this.buttonThirdDirection_Click);
            // 
            // comboThirdColumn
            // 
            resources.ApplyResources(this.comboThirdColumn, "comboThirdColumn");
            this.comboThirdColumn.DisplayMember = "Value";
            this.comboThirdColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboThirdColumn.DropDownWidth = 250;
            this.comboThirdColumn.FormattingEnabled = true;
            this.comboThirdColumn.Name = "comboThirdColumn";
            this.comboThirdColumn.ValueMember = "Key";
            this.comboThirdColumn.SelectedIndexChanged += new System.EventHandler(this.comboThirdColumn_SelectedIndexChanged);
            // 
            // buttonSecondDirection
            // 
            resources.ApplyResources(this.buttonSecondDirection, "buttonSecondDirection");
            this.buttonSecondDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonSecondDirection.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonSecondDirection.Name = "buttonSecondDirection";
            this.buttonSecondDirection.UseVisualStyleBackColor = true;
            this.buttonSecondDirection.Click += new System.EventHandler(this.buttonSecondDirection_Click);
            // 
            // comboSecondColumn
            // 
            resources.ApplyResources(this.comboSecondColumn, "comboSecondColumn");
            this.comboSecondColumn.DisplayMember = "Value";
            this.comboSecondColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSecondColumn.DropDownWidth = 250;
            this.comboSecondColumn.FormattingEnabled = true;
            this.comboSecondColumn.Name = "comboSecondColumn";
            this.comboSecondColumn.ValueMember = "Key";
            this.comboSecondColumn.SelectedIndexChanged += new System.EventHandler(this.comboSecondColumn_SelectedIndexChanged);
            // 
            // buttonFirstDirection
            // 
            resources.ApplyResources(this.buttonFirstDirection, "buttonFirstDirection");
            this.buttonFirstDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonFirstDirection.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonFirstDirection.Name = "buttonFirstDirection";
            this.buttonFirstDirection.UseVisualStyleBackColor = true;
            this.buttonFirstDirection.Click += new System.EventHandler(this.buttonFirstDirection_Click);
            // 
            // comboFirstColumn
            // 
            resources.ApplyResources(this.comboFirstColumn, "comboFirstColumn");
            this.comboFirstColumn.DisplayMember = "Value";
            this.comboFirstColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFirstColumn.DropDownWidth = 250;
            this.comboFirstColumn.FormattingEnabled = true;
            this.comboFirstColumn.Name = "comboFirstColumn";
            this.comboFirstColumn.ValueMember = "Key";
            this.comboFirstColumn.SelectedIndexChanged += new System.EventHandler(this.comboFirstColumn_SelectedIndexChanged);
            // 
            // SettingsEditorSorting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupSortBy);
            this.Name = "SettingsEditorSorting";
            this.Load += new System.EventHandler(this.SettingsEditorSorting_Load);
            this.groupSortBy.ResumeLayout(false);
            this.groupSortBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupSortBy;
        private System.Windows.Forms.Button buttonThirdDirection;
        private System.Windows.Forms.ComboBox comboThirdColumn;
        private System.Windows.Forms.Button buttonSecondDirection;
        private System.Windows.Forms.ComboBox comboSecondColumn;
        private System.Windows.Forms.Button buttonFirstDirection;
        private System.Windows.Forms.ComboBox comboFirstColumn;
        private System.Windows.Forms.CheckBox checkGlobalSorting;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
