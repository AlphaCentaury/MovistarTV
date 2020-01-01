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

using System.Windows.Forms;

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
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
            EditorContainer?.SetDataChanged();
        } // SetDataChanged
    } // internal class SettingsEditorBaseUserControl
} // namespace
