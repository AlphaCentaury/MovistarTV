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
using System.Windows.Forms;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    internal class SettingsEditorModeBaseColumn : SettingsEditorBaseUserControl, ISettingsEditorModeColumns
    {
        #region ISettingsEditorModeColumns implementation

        public List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsList
        {
            protected get;
            set;
        } // Columns

        public List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsNoneList
        {
            protected get;
            set;
        } // Columns

        public IList<UiBroadcastListColumn> Columns
        {
            protected get;
            set;
        } // Columns

        // class should be abstract and this method as well
        // however, being a UserControl, it can't be abstract to be used by WinForms designers
        public virtual List<UiBroadcastListColumn> SelectedColumns => throw new NotImplementedException();

        public Control GetUnderlyingControl()
        {
            return this;
        } // GetUnderlyingControl

        #endregion
    } // SettingsEditorModeBaseColumn
} // namespace
