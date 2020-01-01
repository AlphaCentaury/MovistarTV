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

namespace IpTviewr.UiServices.Common.Controls
{
    partial class ListEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListEditor));
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.listItems = new System.Windows.Forms.ListBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxData
            // 
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            this.groupBoxData.Controls.Add(this.buttonAdd);
            this.groupBoxData.Controls.Add(this.buttonMoveDown);
            this.groupBoxData.Controls.Add(this.buttonMoveUp);
            this.groupBoxData.Controls.Add(this.buttonRemove);
            this.groupBoxData.Controls.Add(this.listItems);
            this.groupBoxData.Controls.Add(this.buttonEdit);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxData, resources.GetString("groupBoxData.ToolTip"));
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonAdd.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Property_Add_16x16;
            this.buttonAdd.Name = "buttonAdd";
            this.toolTip.SetToolTip(this.buttonAdd, resources.GetString("buttonAdd.ToolTip"));
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonMoveDown
            // 
            resources.ApplyResources(this.buttonMoveDown, "buttonMoveDown");
            this.buttonMoveDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonMoveDown.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_GoNextDown_16x16;
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.toolTip.SetToolTip(this.buttonMoveDown, resources.GetString("buttonMoveDown.ToolTip"));
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
            // 
            // buttonMoveUp
            // 
            resources.ApplyResources(this.buttonMoveUp, "buttonMoveUp");
            this.buttonMoveUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonMoveUp.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_GoPreviousUp_16x16;
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.toolTip.SetToolTip(this.buttonMoveUp, resources.GetString("buttonMoveUp.ToolTip"));
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
            // 
            // buttonRemove
            // 
            resources.ApplyResources(this.buttonRemove, "buttonRemove");
            this.buttonRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonRemove.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Delete_16x16;
            this.buttonRemove.Name = "buttonRemove";
            this.toolTip.SetToolTip(this.buttonRemove, resources.GetString("buttonRemove.ToolTip"));
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // listItems
            // 
            resources.ApplyResources(this.listItems, "listItems");
            this.listItems.FormattingEnabled = true;
            this.listItems.Name = "listItems";
            this.toolTip.SetToolTip(this.listItems, resources.GetString("listItems.ToolTip"));
            this.listItems.SelectedIndexChanged += new System.EventHandler(this.ListItems_SelectedIndexChanged);
            this.listItems.DoubleClick += new System.EventHandler(this.ListItems_DoubleClick);
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonEdit.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Property_Edit_16x16;
            this.buttonEdit.Name = "buttonEdit";
            this.toolTip.SetToolTip(this.buttonEdit, resources.GetString("buttonEdit.ToolTip"));
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // ListEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxData);
            this.Name = "ListEditor";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ListEditor_Load);
            this.groupBoxData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.GroupBox groupBoxData;
        protected System.Windows.Forms.Button buttonAdd;
        protected System.Windows.Forms.Button buttonMoveDown;
        protected System.Windows.Forms.Button buttonMoveUp;
        protected System.Windows.Forms.Button buttonRemove;
        protected System.Windows.Forms.ListBox listItems;
        protected System.Windows.Forms.Button buttonEdit;
        protected System.Windows.Forms.ToolTip toolTip;
    } // class ListEditor
} // namespace
