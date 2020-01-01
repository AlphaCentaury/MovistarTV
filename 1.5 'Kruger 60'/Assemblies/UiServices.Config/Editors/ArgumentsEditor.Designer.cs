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

using System.Drawing;

namespace IpTviewr.UiServices.Configuration.Editors
{
    partial class ArgumentsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArgumentsEditor));
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            // 
            // buttonAdd
            // 
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.toolTip.SetToolTip(this.buttonAdd, resources.GetString("buttonAdd.ToolTip"));
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.toolTip.SetToolTip(this.buttonMoveDown, resources.GetString("buttonMoveDown.ToolTip"));
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.toolTip.SetToolTip(this.buttonMoveUp, resources.GetString("buttonMoveUp.ToolTip"));
            // 
            // buttonRemove
            // 
            this.buttonRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.toolTip.SetToolTip(this.buttonRemove, resources.GetString("buttonRemove.ToolTip"));
            // 
            // listItems
            // 
            resources.ApplyResources(this.listItems, "listItems");
            this.listItems.Font = new Font(new FontFamily("Lucida Console"), 9.0f);
            // 
            // buttonEdit
            // 
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.toolTip.SetToolTip(this.buttonEdit, resources.GetString("buttonEdit.ToolTip"));
            // 
            // ArgumentsEditor
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "ArgumentsEditor";
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    } // class ArgumentsEditor
} // namespace
