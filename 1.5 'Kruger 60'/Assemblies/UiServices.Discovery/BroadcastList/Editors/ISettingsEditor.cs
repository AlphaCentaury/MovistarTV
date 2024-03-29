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

namespace IpTviewr.UiServices.Discovery.BroadcastList.Editors
{
    internal interface ISettingsEditor
    {
        void SetContainer(ISettingsEditorContainer container);

        bool IsDataChanged
        {
            get;
        } // IsDataChanged            
    } // internal interface ISettingsEditor
} // namespace
