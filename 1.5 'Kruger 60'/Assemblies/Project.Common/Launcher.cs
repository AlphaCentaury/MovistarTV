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
using System.Diagnostics;
using System.Windows.Forms;

namespace IpTviewr.Common
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
        public static void OpenUrl(IWin32Window parentForm, string url, Action<ExceptionEventData> exceptionHandler, string openUrlErrorFormat)
        {
            var ex = OpenUrl(parentForm, url);
            if (ex != null)
            {
                exceptionHandler(new ExceptionEventData(string.Format(openUrlErrorFormat ?? Properties.Texts.LauncherOpenUrlError, url), ex));
            } // if
        } // OpenUrl
    } // class Launcher
} // namespace
