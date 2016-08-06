using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Discovery.BroadcastList.Editors
{
    internal class SettingsEditorBaseUserControl : UserControl, ISettingsEditor
    {
        private ISettingsEditorContainer EditorContainer
        {
            get;
            set;
        } // EditorContainer

        #region ISettingsEditor implementation

        public void SetContainer(ISettingsEditorContainer container)
        {
            EditorContainer = container;
        } // SetContainer

        public bool IsDataChanged
        {
            get;
            private set;
        } // IsDataChanged

        #endregion

        protected void SetDataChanged()
        {
            IsDataChanged = true;
            if (EditorContainer != null)
            {
                EditorContainer.SetDataChanged();
            } // if
        } // SetDataChanged
    } // internal class SettingsEditorBaseUserControl
} // namespace
