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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    public partial class UiBroadcastListSettingsEditor : UserControl, IConfigurationItemEditor, ISettingsEditorContainer
    {
        private int _manualUpdateLock;
        private ToolStripButton[] _listModeItems;
        private ISettingsEditorModeColumns[] _editorModeColumns;
        private SettingsEditorSorting _editorGlobalSorting;
        private SettingsEditorSorting[] _editorModeSorting;

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

        UserControl IConfigurationItemEditor.UserInterfaceItem => this;

        bool IConfigurationItemEditor.SupportsWinFormsValidation => false;

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded
            // IsAppRestartNeeded
            =>
                false;

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
            _listModeItems = new ToolStripButton[5];
            _listModeItems[0] = toolButtonDetails;
            _listModeItems[1] = toolButtonLarge;
            _listModeItems[2] = toolButtonSmall;
            _listModeItems[3] = toolButtonList;
            _listModeItems[4] = toolButtonTile;
            _listModeItems[0].Tag = View.Details;
            _listModeItems[1].Tag = View.LargeIcon;
            _listModeItems[2].Tag = View.SmallIcon;
            _listModeItems[3].Tag = View.List;
            _listModeItems[4].Tag = View.Tile;

            _manualUpdateLock++;

            checkShowInactive.Checked = Settings.ShowInactiveServices;
            checkShowHidden.Checked = Settings.ShowHiddenServices;
            checkShowOutOfPackage.Checked = Settings.ShowOutOfPackage;

            _editorGlobalSorting = new SettingsEditorSorting
            {
                ColumnsNoneList = sortedColumnsNone,
                Sort = Settings.GlobalSortColumns
            };
            _editorGlobalSorting.SetContainer(this);
            _editorGlobalSorting.Dock = DockStyle.Fill;
            _editorGlobalSorting.UseGlobalSort = Settings.UseGlobalSortColumns;
            _editorGlobalSorting.ShowUseGlobalSort = true;
            _editorGlobalSorting.UseGlobalSortChanged += EditorGlobalSorting_UseGlobalSortChanged;
            panelGlobalSorting.Controls.Add(_editorGlobalSorting);

            _manualUpdateLock--;
        } // LoadGeneralTab

        private void SaveGeneralTab()
        {
            if (_editorGlobalSorting.IsDataChanged)
            {
                Settings.GlobalSortColumns = _editorGlobalSorting.SelectedSort;
                Settings.UseGlobalSortColumns = _editorGlobalSorting.UseGlobalSort;
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
            foreach (var control in _listModeItems)
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
            if (_manualUpdateLock > 0) return;

            Settings.ShowInactiveServices = checkShowInactive.Checked;
            SetDataChanged();
        }

        private void checkShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            Settings.ShowHiddenServices = checkShowHidden.Checked;
            SetDataChanged();
        } // checkShowHidden_CheckedChanged

        private void checkShowOutOfPackage_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            Settings.ShowOutOfPackage = checkShowOutOfPackage.Checked;
            SetDataChanged();
        } // checkShowOutOfPackage_CheckedChanged

        private void EditorGlobalSorting_UseGlobalSortChanged(object sender, EventArgs e)
        {
            foreach (var editor in _editorModeSorting)
            {
                editor.Enabled = !_editorGlobalSorting.UseGlobalSort;
            } // foreach editor
        } // EditorGlobalSorting_UseGlobalSortChanged

        #endregion

        #region Mode settings tab load/save

        private void LoadModeSettingsTab(List<KeyValuePair<UiBroadcastListColumn, string>> sortedColumns, List<KeyValuePair<UiBroadcastListColumn, string>> sortedColumnsNone)
        {
            _editorModeColumns = new ISettingsEditorModeColumns[5];
            _editorModeColumns[0] = new SettingsEditorModeMultiColumn();
            _editorModeColumns[1] = new SettingsEditorModeSingleColumn();
            _editorModeColumns[2] = new SettingsEditorModeSingleColumn();
            _editorModeColumns[3] = new SettingsEditorModeSingleColumn();
            _editorModeColumns[4] = new SettingsEditorModeTripleColumn();

            _editorModeSorting = new SettingsEditorSorting[5];
            _editorModeSorting[0] = new SettingsEditorSorting();
            _editorModeSorting[1] = new SettingsEditorSorting();
            _editorModeSorting[2] = new SettingsEditorSorting();
            _editorModeSorting[3] = new SettingsEditorSorting();
            _editorModeSorting[4] = new SettingsEditorSorting();

            for (var index = 0; index < _editorModeColumns.Length; index++)
            {
                var editor = _editorModeColumns[index];
                var view = IndexToView(index);
                editor.SetContainer(this);
                editor.ColumnsList = sortedColumns;
                editor.ColumnsNoneList = sortedColumnsNone;
                editor.Columns = Settings[view].Columns;
            } // for

            for (var index = 0; index < _editorModeSorting.Length; index++)
            {
                var editor = _editorModeSorting[index];
                var view = IndexToView(index);
                editor.SetContainer(this);
                editor.ColumnsNoneList = sortedColumnsNone;
                editor.Sort = Settings[view].Sort;
                editor.Enabled = !Settings.UseGlobalSortColumns;
            } // for

            _manualUpdateLock++;

            comboLogoSize.ValueMember = "Key";
            comboLogoSize.DisplayMember = "Value";
            comboLogoSize.DataSource = BaseLogo.GetListLogoSizes(true).AsReadOnly();

            checkShowGridlines.Checked = Settings.ShowGridlines;

            _manualUpdateLock--;

            SetListMode(Settings.CurrentMode);
        } // private LoadModeSettingsTab

        private void SaveModeSettingsTab()
        {
            for (var index = 0; index < _editorModeColumns.Length; index++)
            {
                var editor = _editorModeColumns[index];
                if (!editor.IsDataChanged) continue;

                var view = IndexToView(index);
                Settings[view].Columns = editor.SelectedColumns;
            } // for

            for (var index = 0; index < _editorModeSorting.Length; index++)
            {
                var editor = _editorModeSorting[index];
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
            _manualUpdateLock++;

            var index = comboMode.SelectedIndex;

            panelModeColumns.Controls.Clear();
            panelModeColumns.Controls.Add(_editorModeColumns[index].GetUnderlyingControl());
            panelModeColumns.Controls[0].Dock = DockStyle.Fill;

            panelModeSorting.Controls.Clear();
            panelModeSorting.Controls.Add(_editorModeSorting[index]);
            panelModeSorting.Controls[0].Dock = DockStyle.Fill;

            var view = IndexToView(comboMode.SelectedIndex);
            comboLogoSize.SelectedValue = Settings[view].LogoSize;
            checkShowGridlines.Visible = (view == View.Details);

            _manualUpdateLock--;
        } // comboMode_SelectedIndexChanged

        private void comboLogoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            var view = IndexToView(comboMode.SelectedIndex);
            Settings[view].LogoSize = (LogoSize) comboLogoSize.SelectedValue;
            SetDataChanged();
        } //  comboLogoSize_SelectedIndexChanged

        private void checkShowGridlines_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            Settings.ShowGridlines = checkShowGridlines.Checked;
            SetDataChanged();
        } // checkShowGridlines_CheckedChanged

        #endregion
    } // class UiBroadcastListSettingsEditor
} // namespace
