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
using System.Drawing;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public interface ISplashAppInitializer
    {
        void InitializeContext(SplashAppContext ctx);
        Image SetupSplashScreen(Label progressLabel);
        void InitializeApp(ISplashScreen splash);
        bool OnInitializationComplete(Exception ex);
        void DisplayMessage(string caption, string message, MessageBoxIcon icon);
        void DisplayException(string caption, string message, Exception exception);
        Form CreateMainForm();
    } // interface ISplashAppInitializer
} // namespace
