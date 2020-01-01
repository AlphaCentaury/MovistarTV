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

namespace IpTviewr.UiServices.Configuration
{
    partial class ConfigurationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelConfigItemUi = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewConfigItems = new System.Windows.Forms.ListView();
            this.imageListConfigItems = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
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
            // panelConfigItemUi
            // 
            resources.ApplyResources(this.panelConfigItemUi, "panelConfigItemUi");
            this.panelConfigItemUi.Name = "panelConfigItemUi";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Name = "label1";
            // 
            // listViewConfigItems
            // 
            resources.ApplyResources(this.listViewConfigItems, "listViewConfigItems");
            this.listViewConfigItems.BackgroundImageTiled = true;
            this.listViewConfigItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewConfigItems.FullRowSelect = true;
            this.listViewConfigItems.GridLines = true;
            this.listViewConfigItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewConfigItems.HideSelection = false;
            this.listViewConfigItems.LargeImageList = this.imageListConfigItems;
            this.listViewConfigItems.Name = "listViewConfigItems";
            this.listViewConfigItems.SmallImageList = this.imageListConfigItems;
            this.listViewConfigItems.UseCompatibleStateImageBehavior = false;
            this.listViewConfigItems.View = System.Windows.Forms.View.Tile;
            this.listViewConfigItems.SelectedIndexChanged += new System.EventHandler(this.listViewConfigItems_SelectedIndexChanged);
            // 
            // imageListConfigItems
            // 
            this.imageListConfigItems.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListConfigItems, "imageListConfigItems");
            this.imageListConfigItems.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.listViewConfigItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelConfigItemUi);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigurationForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelConfigItemUi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewConfigItems;
        private System.Windows.Forms.ImageList imageListConfigItems;

    }
}
