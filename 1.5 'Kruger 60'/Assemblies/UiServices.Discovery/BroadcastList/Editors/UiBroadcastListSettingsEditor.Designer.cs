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
    partial class UiBroadcastListSettingsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiBroadcastListSettingsEditor));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.panelGlobalSorting = new System.Windows.Forms.Panel();
            this.groupGeneralMoreOptions = new System.Windows.Forms.GroupBox();
            this.checkShowOutOfPackage = new System.Windows.Forms.CheckBox();
            this.checkShowHidden = new System.Windows.Forms.CheckBox();
            this.checkShowInactive = new System.Windows.Forms.CheckBox();
            this.groupViewMode = new System.Windows.Forms.GroupBox();
            this.toolStripListMode = new System.Windows.Forms.ToolStrip();
            this.toolButtonDetails = new System.Windows.Forms.ToolStripButton();
            this.toolButtonLarge = new System.Windows.Forms.ToolStripButton();
            this.toolButtonSmall = new System.Windows.Forms.ToolStripButton();
            this.toolButtonList = new System.Windows.Forms.ToolStripButton();
            this.toolButtonTile = new System.Windows.Forms.ToolStripButton();
            this.tabPageViewSettings = new System.Windows.Forms.TabPage();
            this.panelModeSorting = new System.Windows.Forms.Panel();
            this.groupViewMoreOptions = new System.Windows.Forms.GroupBox();
            this.checkShowGridlines = new System.Windows.Forms.CheckBox();
            this.comboLogoSize = new System.Windows.Forms.ComboBox();
            this.labelLogoSize = new System.Windows.Forms.Label();
            this.panelModeColumns = new System.Windows.Forms.Panel();
            this.labelMode = new System.Windows.Forms.Label();
            this.comboMode = new System.Windows.Forms.ComboBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupGeneralMoreOptions.SuspendLayout();
            this.groupViewMode.SuspendLayout();
            this.toolStripListMode.SuspendLayout();
            this.tabPageViewSettings.SuspendLayout();
            this.groupViewMoreOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Controls.Add(this.tabPageViewSettings);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGeneral.Controls.Add(this.panelGlobalSorting);
            this.tabPageGeneral.Controls.Add(this.groupGeneralMoreOptions);
            this.tabPageGeneral.Controls.Add(this.groupViewMode);
            this.tabPageGeneral.Name = "tabPageGeneral";
            // 
            // panelGlobalSorting
            // 
            resources.ApplyResources(this.panelGlobalSorting, "panelGlobalSorting");
            this.panelGlobalSorting.Name = "panelGlobalSorting";
            // 
            // groupGeneralMoreOptions
            // 
            resources.ApplyResources(this.groupGeneralMoreOptions, "groupGeneralMoreOptions");
            this.groupGeneralMoreOptions.Controls.Add(this.checkShowOutOfPackage);
            this.groupGeneralMoreOptions.Controls.Add(this.checkShowHidden);
            this.groupGeneralMoreOptions.Controls.Add(this.checkShowInactive);
            this.groupGeneralMoreOptions.Name = "groupGeneralMoreOptions";
            this.groupGeneralMoreOptions.TabStop = false;
            // 
            // checkShowOutOfPackage
            // 
            resources.ApplyResources(this.checkShowOutOfPackage, "checkShowOutOfPackage");
            this.checkShowOutOfPackage.Name = "checkShowOutOfPackage";
            this.checkShowOutOfPackage.UseVisualStyleBackColor = true;
            this.checkShowOutOfPackage.CheckedChanged += new System.EventHandler(this.checkShowOutOfPackage_CheckedChanged);
            // 
            // checkShowHidden
            // 
            resources.ApplyResources(this.checkShowHidden, "checkShowHidden");
            this.checkShowHidden.Name = "checkShowHidden";
            this.checkShowHidden.UseVisualStyleBackColor = true;
            this.checkShowHidden.CheckedChanged += new System.EventHandler(this.checkShowHidden_CheckedChanged);
            // 
            // checkShowInactive
            // 
            resources.ApplyResources(this.checkShowInactive, "checkShowInactive");
            this.checkShowInactive.Name = "checkShowInactive";
            this.checkShowInactive.UseVisualStyleBackColor = true;
            this.checkShowInactive.CheckedChanged += new System.EventHandler(this.checkShowInactive_CheckedChanged);
            // 
            // groupViewMode
            // 
            resources.ApplyResources(this.groupViewMode, "groupViewMode");
            this.groupViewMode.Controls.Add(this.toolStripListMode);
            this.groupViewMode.Name = "groupViewMode";
            this.groupViewMode.TabStop = false;
            // 
            // toolStripListMode
            // 
            resources.ApplyResources(this.toolStripListMode, "toolStripListMode");
            this.toolStripListMode.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripListMode.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripListMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonDetails,
            this.toolButtonLarge,
            this.toolButtonSmall,
            this.toolButtonList,
            this.toolButtonTile});
            this.toolStripListMode.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripListMode.Name = "toolStripListMode";
            // 
            // toolButtonDetails
            // 
            resources.ApplyResources(this.toolButtonDetails, "toolButtonDetails");
            this.toolButtonDetails.BackColor = System.Drawing.SystemColors.Window;
            this.toolButtonDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolButtonDetails.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_Details_16x16;
            this.toolButtonDetails.Name = "toolButtonDetails";
            this.toolButtonDetails.Click += new System.EventHandler(this.toolButtonDetails_Click);
            // 
            // toolButtonLarge
            // 
            resources.ApplyResources(this.toolButtonLarge, "toolButtonLarge");
            this.toolButtonLarge.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_MediumIcons_16x16;
            this.toolButtonLarge.Name = "toolButtonLarge";
            this.toolButtonLarge.Click += new System.EventHandler(this.toolButtonLarge_Click);
            // 
            // toolButtonSmall
            // 
            resources.ApplyResources(this.toolButtonSmall, "toolButtonSmall");
            this.toolButtonSmall.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_SmallIcons_16x16;
            this.toolButtonSmall.Name = "toolButtonSmall";
            this.toolButtonSmall.Click += new System.EventHandler(this.toolButtonSmall_Click);
            // 
            // toolButtonList
            // 
            resources.ApplyResources(this.toolButtonList, "toolButtonList");
            this.toolButtonList.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_List_16x16;
            this.toolButtonList.Name = "toolButtonList";
            this.toolButtonList.Click += new System.EventHandler(this.toolButtonList_Click);
            // 
            // toolButtonTile
            // 
            resources.ApplyResources(this.toolButtonTile, "toolButtonTile");
            this.toolButtonTile.Image = global::IpTviewr.UiServices.Discovery.Properties.Resources.ListView_Tiles_16x16;
            this.toolButtonTile.Name = "toolButtonTile";
            this.toolButtonTile.Click += new System.EventHandler(this.toolButtonTile_Click);
            // 
            // tabPageViewSettings
            // 
            resources.ApplyResources(this.tabPageViewSettings, "tabPageViewSettings");
            this.tabPageViewSettings.Controls.Add(this.panelModeSorting);
            this.tabPageViewSettings.Controls.Add(this.groupViewMoreOptions);
            this.tabPageViewSettings.Controls.Add(this.panelModeColumns);
            this.tabPageViewSettings.Controls.Add(this.labelMode);
            this.tabPageViewSettings.Controls.Add(this.comboMode);
            this.tabPageViewSettings.Name = "tabPageViewSettings";
            // 
            // panelModeSorting
            // 
            resources.ApplyResources(this.panelModeSorting, "panelModeSorting");
            this.panelModeSorting.Name = "panelModeSorting";
            // 
            // groupViewMoreOptions
            // 
            resources.ApplyResources(this.groupViewMoreOptions, "groupViewMoreOptions");
            this.groupViewMoreOptions.Controls.Add(this.checkShowGridlines);
            this.groupViewMoreOptions.Controls.Add(this.comboLogoSize);
            this.groupViewMoreOptions.Controls.Add(this.labelLogoSize);
            this.groupViewMoreOptions.Name = "groupViewMoreOptions";
            this.groupViewMoreOptions.TabStop = false;
            // 
            // checkShowGridlines
            // 
            resources.ApplyResources(this.checkShowGridlines, "checkShowGridlines");
            this.checkShowGridlines.Name = "checkShowGridlines";
            this.checkShowGridlines.UseVisualStyleBackColor = true;
            this.checkShowGridlines.CheckedChanged += new System.EventHandler(this.checkShowGridlines_CheckedChanged);
            // 
            // comboLogoSize
            // 
            resources.ApplyResources(this.comboLogoSize, "comboLogoSize");
            this.comboLogoSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogoSize.FormattingEnabled = true;
            this.comboLogoSize.Items.AddRange(new object[] {
            resources.GetString("comboLogoSize.Items"),
            resources.GetString("comboLogoSize.Items1"),
            resources.GetString("comboLogoSize.Items2"),
            resources.GetString("comboLogoSize.Items3"),
            resources.GetString("comboLogoSize.Items4"),
            resources.GetString("comboLogoSize.Items5")});
            this.comboLogoSize.Name = "comboLogoSize";
            this.comboLogoSize.SelectedIndexChanged += new System.EventHandler(this.comboLogoSize_SelectedIndexChanged);
            // 
            // labelLogoSize
            // 
            resources.ApplyResources(this.labelLogoSize, "labelLogoSize");
            this.labelLogoSize.Name = "labelLogoSize";
            // 
            // panelModeColumns
            // 
            resources.ApplyResources(this.panelModeColumns, "panelModeColumns");
            this.panelModeColumns.Name = "panelModeColumns";
            // 
            // labelMode
            // 
            resources.ApplyResources(this.labelMode, "labelMode");
            this.labelMode.Name = "labelMode";
            // 
            // comboMode
            // 
            resources.ApplyResources(this.comboMode, "comboMode");
            this.comboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMode.FormattingEnabled = true;
            this.comboMode.Items.AddRange(new object[] {
            resources.GetString("comboMode.Items"),
            resources.GetString("comboMode.Items1"),
            resources.GetString("comboMode.Items2"),
            resources.GetString("comboMode.Items3"),
            resources.GetString("comboMode.Items4")});
            this.comboMode.Name = "comboMode";
            this.comboMode.SelectedIndexChanged += new System.EventHandler(this.comboMode_SelectedIndexChanged);
            // 
            // UiBroadcastListSettingsEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlSettings);
            this.Name = "UiBroadcastListSettingsEditor";
            this.Load += new System.EventHandler(this.UiBroadcastListSettingsEditor_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupGeneralMoreOptions.ResumeLayout(false);
            this.groupGeneralMoreOptions.PerformLayout();
            this.groupViewMode.ResumeLayout(false);
            this.toolStripListMode.ResumeLayout(false);
            this.toolStripListMode.PerformLayout();
            this.tabPageViewSettings.ResumeLayout(false);
            this.tabPageViewSettings.PerformLayout();
            this.groupViewMoreOptions.ResumeLayout(false);
            this.groupViewMoreOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageViewSettings;
        private System.Windows.Forms.GroupBox groupViewMode;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.ComboBox comboMode;
        private System.Windows.Forms.GroupBox groupGeneralMoreOptions;
        private System.Windows.Forms.GroupBox groupViewMoreOptions;
        private System.Windows.Forms.Panel panelModeColumns;
        private System.Windows.Forms.Panel panelModeSorting;
        private System.Windows.Forms.ToolStrip toolStripListMode;
        private System.Windows.Forms.ToolStripButton toolButtonDetails;
        private System.Windows.Forms.ToolStripButton toolButtonLarge;
        private System.Windows.Forms.ToolStripButton toolButtonSmall;
        private System.Windows.Forms.ToolStripButton toolButtonList;
        private System.Windows.Forms.ToolStripButton toolButtonTile;
        private System.Windows.Forms.CheckBox checkShowHidden;
        private System.Windows.Forms.CheckBox checkShowInactive;
        private System.Windows.Forms.CheckBox checkShowOutOfPackage;
        private System.Windows.Forms.ComboBox comboLogoSize;
        private System.Windows.Forms.Label labelLogoSize;
        private System.Windows.Forms.CheckBox checkShowGridlines;
        private System.Windows.Forms.Panel panelGlobalSorting;
    }
}
