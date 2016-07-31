// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Configuration.Logos;

namespace Project.IpTv.UiServices.Discovery.BroadcastList.Editors
{
    public partial class UiBroadcastListSettingsEditor : UserControl, IConfigurationItemEditor, ISettingsEditorContainer
    {
        private int ManualUpdateLock;
        private ToolStripButton[] ListModeItems;
        private ISettingsEditorModeColumns[] EditorModeColumns;
        private SettingsEditorSorting EditorGlobalSorting;
        private SettingsEditorSorting[] EditorModeSorting;

        public UiBroadcastListSettingsEditor()
        {
            InitializeComponent();
        } // constructor

        public UiBroadcastListSettings Settings
        {
            get;
            set;
        } // Settings

        #region IConfigurationItemEditor implementation

        UserControl IConfigurationItemEditor.UserInterfaceItem
        {
            get { return this; }
        } // IConfigurationItemEditor.UserInterfaceItem

        bool IConfigurationItemEditor.SupportsWinFormsValidation
        {
            get { return false; }
        } // IConfigurationItemEditor.SupportsWinFormsValidation

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded
        {
            get { return false; }
        } // IsAppRestartNeeded

        bool IConfigurationItemEditor.Validate()
        {
            return true;
        } // IConfigurationItemEditor.Validate

        IConfigurationItem IConfigurationItemEditor.GetNewData()
        {
            SaveGeneralTab();
            SaveModeSettingsTab();

            return Settings;
        } // IConfigurationItemEditor.GetNewData

        void IConfigurationItemEditor.EditorClosing(out bool cancelClose)
        {
            cancelClose = false;
        } // IConfigurationItemEditor.EditorClosing

        void IConfigurationItemEditor.EditorClosed(bool userCancel)
        {
            // no op
        } // IConfigurationItemEditor.EditorClosed

        #endregion

        #region ISettingsEditorContainer implementation

        void ISettingsEditorContainer.SetDataChanged()
        {
            IsDataChanged = true;
        } // SetDataChanged

        protected void SetDataChanged()
        {
            IsDataChanged = true;
        } // SetDataChanged

        #endregion

        private void UiBroadcastListSettingsEditor_Load(object sender, EventArgs e)
        {
            var sortedColumns = UiBroadcastListManager.GetSortedColumnNames();
            var sortedColumnsNone = UiBroadcastListManager.GetSortedColumnNames(true);

            // General tab
            LoadGeneralTab(sortedColumnsNone);

            // Mode settings tab
            LoadModeSettingsTab(sortedColumns, sortedColumnsNone);
        } // UiBroadcastListSettingsEditor_Load

        #region General tab load/save

        private void LoadGeneralTab(List<KeyValuePair<UiBroadcastListColumn, string>> sortedColumnsNone)
        {
            ListModeItems = new ToolStripButton[5];
            ListModeItems[0] = toolButtonDetails;
            ListModeItems[1] = toolButtonLarge;
            ListModeItems[2] = toolButtonSmall;
            ListModeItems[3] = toolButtonList;
            ListModeItems[4] = toolButtonTile;
            ListModeItems[0].Tag = View.Details;
            ListModeItems[1].Tag = View.LargeIcon;
            ListModeItems[2].Tag = View.SmallIcon;
            ListModeItems[3].Tag = View.List;
            ListModeItems[4].Tag = View.Tile;

            ManualUpdateLock++;

            checkShowInactive.Checked = Settings.ShowInactiveServices;
            checkShowHidden.Checked = Settings.ShowHiddenServices;
            checkShowOutOfPackage.Checked = Settings.ShowOutOfPackage;

            EditorGlobalSorting = new SettingsEditorSorting();
            EditorGlobalSorting.ColumnsNoneList = sortedColumnsNone;
            EditorGlobalSorting.Sort = Settings.GlobalSortColumns;
            EditorGlobalSorting.SetContainer(this);
            EditorGlobalSorting.Dock = DockStyle.Fill;
            EditorGlobalSorting.UseGlobalSort = Settings.UseGlobalSortColumns;
            EditorGlobalSorting.ShowUseGlobalSort = true;
            EditorGlobalSorting.UseGlobalSortChanged += EditorGlobalSorting_UseGlobalSortChanged;
            panelGlobalSorting.Controls.Add(EditorGlobalSorting);

            ManualUpdateLock--;
        } // LoadGeneralTab

        private void SaveGeneralTab()
        {
            if (EditorGlobalSorting.IsDataChanged)
            {
                Settings.GlobalSortColumns = EditorGlobalSorting.SelectedSort;
                Settings.UseGlobalSortColumns = EditorGlobalSorting.UseGlobalSort;
            } // if
        } // SaveGeneralTab

        #endregion

        #region General tab event handlers

        private void toolButtonDetails_Click(object sender, EventArgs e)
        {
            SetListMode(View.Details);
            SetDataChanged();
        } // toolButtonDetails_Click

        private void toolButtonLarge_Click(object sender, EventArgs e)
        {
            SetListMode(View.LargeIcon);
            SetDataChanged();
        } // toolButtonLarge_Click

        private void toolButtonSmall_Click(object sender, EventArgs e)
        {
            SetListMode(View.SmallIcon);
            SetDataChanged();
        } // toolButtonSmall_Click

        private void toolButtonList_Click(object sender, EventArgs e)
        {
            SetListMode(View.List);
            SetDataChanged();
        } // toolButtonList_Click

        private void toolButtonTile_Click(object sender, EventArgs e)
        {
            SetListMode(View.Tile);
            SetDataChanged();
        } // toolButtonTile_Click

        private void SetListMode(View mode)
        {
            Settings.CurrentMode = mode;
            foreach (var control in ListModeItems)
            {
                if (mode == (View)control.Tag)
                {
                    control.BackColor = SystemColors.Highlight;
                    control.ForeColor = SystemColors.HighlightText;
                }
                else
                {
                    control.BackColor = toolStripListMode.BackColor;
                    control.ForeColor = toolStripListMode.ForeColor;
                } // if-else
            } // foreach control

            comboMode.SelectedIndex = ViewToIndex(mode);
        } // SetListMode

        private void checkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            Settings.ShowInactiveServices = checkShowInactive.Checked;
            SetDataChanged();
        }

        private void checkShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            Settings.ShowHiddenServices = checkShowHidden.Checked;
            SetDataChanged();
        } // checkShowHidden_CheckedChanged

        private void checkShowOutOfPackage_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            Settings.ShowOutOfPackage = checkShowOutOfPackage.Checked;
            SetDataChanged();
        } // checkShowOutOfPackage_CheckedChanged

        private void EditorGlobalSorting_UseGlobalSortChanged(object sender, EventArgs e)
        {
            foreach (var editor in EditorModeSorting)
            {
                editor.Enabled = !EditorGlobalSorting.UseGlobalSort;
            } // foreach editor
        } // EditorGlobalSorting_UseGlobalSortChanged

        #endregion

        #region Mode settings tab load/save

        private void LoadModeSettingsTab(List<KeyValuePair<UiBroadcastListColumn, string>> sortedColumns, List<KeyValuePair<UiBroadcastListColumn, string>> sortedColumnsNone)
        {
            EditorModeColumns = new ISettingsEditorModeColumns[5];
            EditorModeColumns[0] = new SettingsEditorModeMultiColumn();
            EditorModeColumns[1] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[2] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[3] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[4] = new SettingsEditorModeTripleColumn();

            EditorModeSorting = new SettingsEditorSorting[5];
            EditorModeSorting[0] = new SettingsEditorSorting();
            EditorModeSorting[1] = new SettingsEditorSorting();
            EditorModeSorting[2] = new SettingsEditorSorting();
            EditorModeSorting[3] = new SettingsEditorSorting();
            EditorModeSorting[4] = new SettingsEditorSorting();

            for (int index = 0; index < EditorModeColumns.Length; index++)
            {
                var editor = EditorModeColumns[index];
                var view = IndexToView(index);
                editor.SetContainer(this);
                editor.ColumnsList = sortedColumns;
                editor.ColumnsNoneList = sortedColumnsNone;
                editor.Columns = Settings[view].Columns;
            } // for

            for (int index = 0; index < EditorModeSorting.Length; index++)
            {
                var editor = EditorModeSorting[index];
                var view = IndexToView(index);
                editor.SetContainer(this);
                editor.ColumnsNoneList = sortedColumnsNone;
                editor.Sort = Settings[view].Sort;
                editor.Enabled = !Settings.UseGlobalSortColumns;
            } // for

            ManualUpdateLock++;

            comboLogoSize.ValueMember = "Key";
            comboLogoSize.DisplayMember = "Value";
            comboLogoSize.DataSource = BaseLogo.GetListLogoSizes(true).AsReadOnly();

            checkShowGridlines.Checked = Settings.ShowGridlines;

            ManualUpdateLock--;

            SetListMode(Settings.CurrentMode);
        } // private LoadModeSettingsTab

        private void SaveModeSettingsTab()
        {
            for (int index = 0; index < EditorModeColumns.Length; index++)
            {
                var editor = EditorModeColumns[index];
                if (!editor.IsDataChanged) continue;

                var view = IndexToView(index);
                Settings[view].Columns = editor.SelectedColumns;
            } // for

            for (int index = 0; index < EditorModeSorting.Length; index++)
            {
                var editor = EditorModeSorting[index];
                if (!editor.IsDataChanged) continue;

                var view = IndexToView(index);
                Settings[view].Sort = editor.SelectedSort;
            } // for
        } // SaveGeneralTab

        private int ViewToIndex(View view)
        {
            switch (view)
            {
                case View.Details: return 0;
                case View.LargeIcon: return 1;
                case View.SmallIcon: return 2;
                case View.List: return 3;
                case View.Tile: return 4;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // ViewToIndex

        private View IndexToView(int index)
        {
            switch (index)
            {
                case 0: return View.Details;
                case 1: return View.LargeIcon;
                case 2: return View.SmallIcon;
                case 3: return View.List;
                case 4: return View.Tile;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // IndexToView

        #endregion

        #region Mode settings tab event handlers

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManualUpdateLock++;

            var index = comboMode.SelectedIndex;

            panelModeColumns.Controls.Clear();
            panelModeColumns.Controls.Add(EditorModeColumns[index].GetUnderlyingControl());
            panelModeColumns.Controls[0].Dock = DockStyle.Fill;

            panelModeSorting.Controls.Clear();
            panelModeSorting.Controls.Add(EditorModeSorting[index]);
            panelModeSorting.Controls[0].Dock = DockStyle.Fill;

            var view = IndexToView(comboMode.SelectedIndex);
            comboLogoSize.SelectedValue = Settings[view].LogoSize;
            checkShowGridlines.Visible = (view == View.Details);

            ManualUpdateLock--;
        } // comboMode_SelectedIndexChanged

        private void comboLogoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            var view = IndexToView(comboMode.SelectedIndex);
            Settings[view].LogoSize = (LogoSize) comboLogoSize.SelectedValue;
            SetDataChanged();
        } //  comboLogoSize_SelectedIndexChanged

        private void checkShowGridlines_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            Settings.ShowGridlines = checkShowGridlines.Checked;
            SetDataChanged();
        } // checkShowGridlines_CheckedChanged

        #endregion
    } // class UiBroadcastListSettingsEditor
} // namespace
