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

namespace IPTviewr.FirstTimeConfig.LoadNetFx35
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // This app does nothing
            // It's used for forcing Windows 10 to prompt the user to install the .Net Framework 3.5
            // Both Microsoft Exception Message Box and SQL CE installers need NetFx 3.5 installed
        } // Main
    } // class Program
} // namespace
