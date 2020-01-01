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
    partial class SettingsEditorModeTripleColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditorModeTripleColumn));
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.comboThirdColumn = new System.Windows.Forms.ComboBox();
            this.comboSecondColumn = new System.Windows.Forms.ComboBox();
            this.comboFirstColumn = new System.Windows.Forms.ComboBox();
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            this.groupBoxData.Controls.Add(this.comboThirdColumn);
            this.groupBoxData.Controls.Add(this.comboSecondColumn);
            this.groupBoxData.Controls.Add(this.comboFirstColumn);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.TabStop = false;
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
            // SettingsEditorModeTripleColumn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxData);
            this.Name = "SettingsEditorModeTripleColumn";
            this.Load += new System.EventHandler(this.SettingsEditorModeTripleColumn_Load);
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.ComboBox comboThirdColumn;
        private System.Windows.Forms.ComboBox comboSecondColumn;
        private System.Windows.Forms.ComboBox comboFirstColumn;
    }
}
