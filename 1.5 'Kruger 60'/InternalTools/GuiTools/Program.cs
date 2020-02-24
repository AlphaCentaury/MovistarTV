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
using System.Globalization;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LaunchForm());
        } // Main

        internal static int ParseNumber(string text)
        {
            text = text.Trim();
            if (text.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                return int.Parse(text.Substring(2), NumberStyles.HexNumber);
            }
            else
            {
                return int.Parse(text, NumberStyles.Integer);
            } // if-else
        } // ParseNumber

        internal static int? ParseNullableNumber(string text)
        {
            text = text.Trim();
            if (text == "") return null;

            return ParseNumber(text);
        } // ParseNullableNumber

    } // class Program
} // namespace
