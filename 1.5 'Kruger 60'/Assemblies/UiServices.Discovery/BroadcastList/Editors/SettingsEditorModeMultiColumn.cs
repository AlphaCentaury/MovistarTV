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
using IpTviewr.UiServices.Common.Controls;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    internal partial class SettingsEditorModeMultiColumn : SettingsEditorModeBaseColumn
    {
        private ListItemsManager<UiBroadcastListColumn> _itemsManager;

        public SettingsEditorModeMultiColumn()
        {
            InitializeComponent();
        } // constructor

        public override List<UiBroadcastListColumn> SelectedColumns => _itemsManager.GetListValues();

        private void SettingsEditorModeMultiColumn_Load(object sender, EventArgs e)
        {
            _itemsManager = new ListItemsManager<UiBroadcastListColumn>(listSelectedColumns, buttonRemove, buttonMoveUp, buttonMoveDown);

            comboColumns.DataSource = ColumnsList.AsReadOnly();
            buttonAddColumn.Enabled = (comboColumns.Items.Count > 0);

            _itemsManager.SetValueDictionary(ColumnsList, null);
            _itemsManager.Add(Columns);
        }  // SettingsEditorModeMultiColumn_Load

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            _itemsManager.Add((UiBroadcastListColumn)comboColumns.SelectedValue);
            SetDataChanged();
        } // buttonAddColumn_Click

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            _itemsManager.RemoveSelection();
            SetDataChanged();
        } // buttonRemove_Click

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            _itemsManager.MoveSelectionUp();
            SetDataChanged();
        } // buttonMoveUp_Click

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            _itemsManager.MoveSelectionDown();
            SetDataChanged();
        } // buttonMoveDown_Click
    } // class SettingsEditorModeMultiColumn
} // namespace
