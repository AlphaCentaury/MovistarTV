// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Discovery.BroadcastList.Editors
{
    internal interface ISettingsEditorModeColumns : ISettingsEditor
    {
        List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsList
        {
            set;
        } // ColumnsList

        List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsNoneList
        {
            set;
        } // ColumnsNoneList

        IList<UiBroadcastListColumn> Columns
        {
            set;
        } // Columns

        List<UiBroadcastListColumn> SelectedColumns
        {
            get;
        } // SelectedColumns

        Control GetUnderlyingControl();
    } // internal interface ISettingsEditorModeColumns
} // namespace
