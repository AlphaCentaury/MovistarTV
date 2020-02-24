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

namespace IpTviewr.UiServices.Common.Controls
{
    partial class StringListEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringListEditor));
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonAdd.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Add_16xM;
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
            // 
            // buttonEdit
            // 
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonEdit.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Edit_16x16;
            this.toolTip.SetToolTip(this.buttonEdit, resources.GetString("buttonEdit.ToolTip"));
            // 
            // StringListEditor
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "StringListEditor";
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    } // class ArgumentsEditor
} // namespace
