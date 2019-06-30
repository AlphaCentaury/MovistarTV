// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    internal partial class SettingsEditorModeSingleColumn : SettingsEditorModeBaseColumn
    {
        private int ManualUpdateLock;

        public SettingsEditorModeSingleColumn()
        {
            InitializeComponent();
        } // constructor

        public override List<UiBroadcastListColumn> SelectedColumns
        {
            get
            {
                var result = new List<UiBroadcastListColumn>(1);
                result.Add((UiBroadcastListColumn)comboColumns.SelectedValue);

                return result;
            } // get
        } // SelectedColumns

        private void SettingsEditorModeMultiColumn_Load(object sender, EventArgs e)
        {
            ManualUpdateLock++;
            comboColumns.DataSource = ColumnsList.AsReadOnly();
            comboColumns.SelectedValue = Columns[0];
            ManualUpdateLock--;
        } // SettingsEditorModeMultiColumn_Load

        private void comboColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            SetDataChanged();
        }  // comboColumns_SelectedIndexChanged
    } // class SettingsEditorModeSingleColumn
} // namespace
