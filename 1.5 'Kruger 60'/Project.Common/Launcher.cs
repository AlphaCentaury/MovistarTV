// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.Common
{
    public class Launcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentForm">Handle to owner form for displaying OS error messages</param>
        /// <param name="url">Url to show in the default browser</param>
        /// <returns></returns>
        public static Exception OpenUrl(IWin32Window parentForm, string url)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parentForm.Handle,
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                return ex;
            } // try-catch

            return null;
        } // OpenUrl

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentForm">Handle to owner form for displaying OS error messages</param>
        /// <param name="url">Url to show in the default browser</param>
        /// <param name="exceptionHandler">Exception handler</param>
        /// <param name="openUrlErrorFormat">Error formating text or null for standard message</param>
        public static void OpenUrl(IWin32Window parentForm, string url, Action<string, Exception> exceptionHandler, string openUrlErrorFormat)
        {
            var ex = OpenUrl(parentForm, url);
            if (ex != null)
            {
                exceptionHandler(string.Format(openUrlErrorFormat ?? Properties.Texts.LauncherOpenUrlError, url), ex);
            } // if
        } // OpenUrl
    } // class Launcher
} // namespace
