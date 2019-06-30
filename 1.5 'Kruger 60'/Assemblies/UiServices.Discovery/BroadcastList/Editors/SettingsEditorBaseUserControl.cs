// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
            if (EditorContainer != null)
            {
                EditorContainer.SetDataChanged();
            } // if
        } // SetDataChanged
    } // internal class SettingsEditorBaseUserControl
} // namespace
