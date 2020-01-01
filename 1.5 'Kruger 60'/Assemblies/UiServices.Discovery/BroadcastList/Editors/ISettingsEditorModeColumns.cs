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

using System.Collections.Generic;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
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
