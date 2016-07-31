// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Common
{
    public class Launcher
    {
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

        public static void OpenUrl(IWin32Window parentForm, string url, Action<string, Exception> exceptionHandler, string openUrlErrorFormat)
        {
            var ex = OpenUrl(parentForm, url);
            if (ex != null)
            {
                exceptionHandler(string.Format(openUrlErrorFormat, url), ex);
            } // if
        } // OpenUrl
    } // class Launcher
} // namespace
