using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Discovery.BroadcastList.Editors
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

        public virtual List<UiBroadcastListColumn> SelectedColumns
        {
            get { throw new NotImplementedException(); }
        } // SelectedColumns

        public Control GetUnderlyingControl()
        {
            return this;
        } // GetUnderlyingControl

        #endregion
    } // SettingsEditorModeBaseColumn
} // namespace
