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
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public interface ISplashScreen
    {
        Form SplashForm { get; }
        void DisplayProgress(string text);
        void DisplayMessage(string caption, string message, MessageBoxIcon icon);
        void DisplayException(string caption, string message, Exception exception);
        void Ready(Form form);
        void Invoke(Delegate method, params object[] args);
    } // interface ISplashScreen
} // namespace
