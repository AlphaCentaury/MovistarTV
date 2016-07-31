// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Discovery.BroadcastList.Editors
{
    internal partial class SettingsEditorModeTripleColumn : SettingsEditorModeBaseColumn
    {
        private int ManualUpdateLock;

        public SettingsEditorModeTripleColumn()
        {
            InitializeComponent();
        } // constructor

        public override List<UiBroadcastListColumn> SelectedColumns
        {
            get
            {
                var result = new List<UiBroadcastListColumn>(Columns.Count);

                var column = (UiBroadcastListColumn)comboFirstColumn.SelectedValue;
                result.Add(column);

                column = (UiBroadcastListColumn)comboSecondColumn.SelectedValue;
                if (column != UiBroadcastListColumn.None)
                {
                    result.Add(column);

                    column = (UiBroadcastListColumn)comboThirdColumn.SelectedValue;
                    if (column != UiBroadcastListColumn.None)
                    {
                        result.Add(column);
                    } // 
                } // if

                return result;
            } // get
        } // 

        private void SettingsEditorModeTripleColumn_Load(object sender, EventArgs e)
        {
            ManualUpdateLock++;
            comboFirstColumn.DataSource = ColumnsList.AsReadOnly();
            comboSecondColumn.DataSource = ColumnsNoneList.AsReadOnly();
            comboThirdColumn.DataSource = ColumnsNoneList.AsReadOnly();

            switch (Columns.Count)
            {
                case 0:
                    comboFirstColumn.SelectedValue = UiBroadcastListColumn.NumberAndName;
                    comboSecondColumn.SelectedValue = UiBroadcastListColumn.None;
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                case 1:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = UiBroadcastListColumn.None;
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                case 2:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = Columns[1];
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                default:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = Columns[1];
                    comboThirdColumn.SelectedValue = Columns[2];
                    break;
            } // switch
            ManualUpdateLock--;
        } // SettingsEditorModeTripleColumn_Load

        private void comboFirstColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            SetDataChanged();
        } // comboFirstColumn_SelectedIndexChanged

        private void comboSecondColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            comboThirdColumn.Enabled = ((UiBroadcastListColumn)comboSecondColumn.SelectedValue) != UiBroadcastListColumn.None;
            if (!comboThirdColumn.Enabled)
            {
                comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
            } // if
            SetDataChanged();
        } // comboSecondColumn_SelectedIndexChanged

        private void comboThirdColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            SetDataChanged();
        } // comboThirdColumn_SelectedIndexChanged
    } // class SettingsEditorModeTripleColumn
} // namespace
