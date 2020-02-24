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

namespace IpTviewr.UiServices.Forms
{
    partial class SelectProviderDialog
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
            System.Windows.Forms.ColumnHeader NameColumn;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectProviderDialog));
            System.Windows.Forms.ColumnHeader DescriptionColumn;
            this.listViewServiceProviders = new System.Windows.Forms.ListView();
            this.imageListProvidersLarge = new System.Windows.Forms.ImageList(this.components);
            this.buttonProviderDetails = new System.Windows.Forms.Button();
            this.buttonRefreshServiceProviderList = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            DescriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // NameColumn
            // 
            resources.ApplyResources(NameColumn, "NameColumn");
            // 
            // DescriptionColumn
            // 
            resources.ApplyResources(DescriptionColumn, "DescriptionColumn");
            // 
            // listViewServiceProviders
            // 
            this.listViewServiceProviders.Activation = System.Windows.Forms.ItemActivation.OneClick;
            resources.ApplyResources(this.listViewServiceProviders, "listViewServiceProviders");
            this.listViewServiceProviders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            NameColumn,
            DescriptionColumn});
            this.listViewServiceProviders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewServiceProviders.HideSelection = false;
            this.listViewServiceProviders.LargeImageList = this.imageListProvidersLarge;
            this.listViewServiceProviders.Name = "listViewServiceProviders";
            this.listViewServiceProviders.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewServiceProviders.UseCompatibleStateImageBehavior = false;
            this.listViewServiceProviders.View = System.Windows.Forms.View.Tile;
            this.listViewServiceProviders.SelectedIndexChanged += new System.EventHandler(this.listViewServiceProviders_SelectedIndexChanged);
            this.listViewServiceProviders.DoubleClick += new System.EventHandler(this.listViewServiceProviders_DoubleClick);
            // 
            // imageListProvidersLarge
            // 
            this.imageListProvidersLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListProvidersLarge, "imageListProvidersLarge");
            this.imageListProvidersLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonProviderDetails
            // 
            resources.ApplyResources(this.buttonProviderDetails, "buttonProviderDetails");
            this.buttonProviderDetails.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Properties_16x16;
            this.buttonProviderDetails.Name = "buttonProviderDetails";
            this.buttonProviderDetails.UseVisualStyleBackColor = true;
            this.buttonProviderDetails.Click += new System.EventHandler(this.buttonProviderDetails_Click);
            // 
            // buttonRefreshServiceProviderList
            // 
            resources.ApplyResources(this.buttonRefreshServiceProviderList, "buttonRefreshServiceProviderList");
            this.buttonRefreshServiceProviderList.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Refresh_Blue_16x16;
            this.buttonRefreshServiceProviderList.Name = "buttonRefreshServiceProviderList";
            this.buttonRefreshServiceProviderList.UseVisualStyleBackColor = true;
            this.buttonRefreshServiceProviderList.Click += new System.EventHandler(this.buttonRefreshServiceProviderList_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.UiServices.Forms.Properties.CommonUiResources.Action_Cancel_16x16;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // SelectProviderDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonProviderDetails);
            this.Controls.Add(this.buttonRefreshServiceProviderList);
            this.Controls.Add(this.listViewServiceProviders);
            this.MinimizeBox = false;
            this.Name = "SelectProviderDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.SelectProviderDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewServiceProviders;
        private System.Windows.Forms.Button buttonProviderDetails;
        private System.Windows.Forms.Button buttonRefreshServiceProviderList;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList imageListProvidersLarge;
    }
}
