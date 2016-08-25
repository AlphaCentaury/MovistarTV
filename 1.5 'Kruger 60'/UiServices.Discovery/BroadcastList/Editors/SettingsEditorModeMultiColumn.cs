// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.IpTv.UiServices.Common.Controls;

namespace Project.IpTv.UiServices.Discovery.BroadcastList.Editors
{
    internal partial class SettingsEditorModeMultiColumn : SettingsEditorModeBaseColumn
    {
        private ListItemsManager<UiBroadcastListColumn> ItemsManager;

        public SettingsEditorModeMultiColumn()
        {
            InitializeComponent();
        } // constructor

        public override List<UiBroadcastListColumn> SelectedColumns
        {
            get { return ItemsManager.GetListValues(); }
        } // SelectedColumns

        private void SettingsEditorModeMultiColumn_Load(object sender, EventArgs e)
        {
            ItemsManager = new ListItemsManager<UiBroadcastListColumn>(listSelectedColumns, buttonRemove, buttonMoveUp, buttonMoveDown);

            comboColumns.DataSource = ColumnsList.AsReadOnly();
            buttonAddColumn.Enabled = (comboColumns.Items.Count > 0);

            ItemsManager.SetValueDictionary(ColumnsList, null);
            ItemsManager.Add(Columns);
        }  // SettingsEditorModeMultiColumn_Load

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            ItemsManager.Add((UiBroadcastListColumn)comboColumns.SelectedValue);
            SetDataChanged();
        } // buttonAddColumn_Click

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            ItemsManager.RemoveSelection();
            SetDataChanged();
        } // buttonRemove_Click

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionUp();
            SetDataChanged();
        } // buttonMoveUp_Click

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionDown();
            SetDataChanged();
        } // buttonMoveDown_Click
    } // class SettingsEditorModeMultiColumn
} // namespace
