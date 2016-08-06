// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
{
    static class Program
    {
        private const string ForceUiCultureArgument = "/forceuiculture:";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ForceUiCulture(arguments);

            using (var dlg = new ConfigForm())
            {
                dlg.ShowDialog();
            } // using dlg
        } // Main

        static void ForceUiCulture(string[] arguments)
        {
            if ((arguments == null) || (arguments.Length == 0)) return;

            var culture = (string)null;
            foreach (var argument in arguments)
            {
                if (!argument.ToLowerInvariant().StartsWith(ForceUiCultureArgument)) continue;
                culture = argument.Substring(ForceUiCultureArgument.Length);
                break;
            } // foreach

            if (culture != null)
            {
                ForceUiCulture(culture);
            } // if
        } // ForceUiCulture

        static void ForceUiCulture(string culture)
        {
            if (culture == null) return;
            culture = culture.Trim();
            if (culture == string.Empty) return;

            try
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }
            catch
            {
                MessageBox.Show(Properties.Texts.ExceptionForceUiCulture,
                    Path.GetFileName(Application.ExecutablePath),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // try-catch
        } // ForceUiCulture
    } // class Program
} // namespace
