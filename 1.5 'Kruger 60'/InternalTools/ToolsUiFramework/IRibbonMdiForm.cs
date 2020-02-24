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

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IRibbonMdiForm
    {
        void SetActiveContexts(IRibbonMdiChild child, params string[] contexts);

        void SetStatusText(string status);
    } // interface IRibbonMdiForm
} // namespace
