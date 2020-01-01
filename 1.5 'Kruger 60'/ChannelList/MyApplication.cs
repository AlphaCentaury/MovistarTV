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

using IpTviewr.Common.Telemetry;
using System;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.Common;

namespace IpTviewr.ChannelList
{
    internal static class MyApplication
    {
        internal static string RecorderLauncherPath
        {
            get;
            set;
        } // RecorderLauncherPath

        #region HandleException methods

        public static void HandleException(Form owner, Exception ex)
        {
            AppTelemetry.FormException(ex, owner);
            BaseProgram.HandleException(owner, Properties.Texts.MyAppHandleExceptionDefaultCaption, null, ex);
        } // HandleException

        public static void HandleException(Form owner, string message, Exception ex)
        {
            HandleException(owner,
                null,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(Form owner, string caption, string message, Exception ex)
        {
            HandleException(owner,
                caption,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(Form owner, string caption, string message, MessageBoxIcon icon, Exception ex)
        {
            AppTelemetry.FormException(ex, owner, message);
            BaseProgram.HandleException(owner, caption, message, icon, ex);
        } // HandleException

        internal static void HandleException(Form form, ExceptionEventData ex)
        {
            HandleException(form, ex.Caption, ex.Message, ex.Exception);
        } // HandleException

        internal static void HandleException(object sender, HandleExceptionEventArgs e)
        {
            HandleException(e.OwnerForm, e.Caption, e.Message, e.Exception);
        } // HandleException

        #endregion

        private const string ForceUiCultureArgument = "/forceuiculture:";

        internal static void ForceUiCulture(string[] arguments, string settingsCulture)
        {
            var culture = (string)null;

            // Command line culture has preference over settings culture (allows to override user setting)
            if ((arguments != null) && (arguments.Length != 0))
            {
                foreach (var argument in arguments)
                {
                    if (!argument.ToLowerInvariant().StartsWith(ForceUiCultureArgument)) continue;
                    culture = argument.Substring(ForceUiCultureArgument.Length);
                    break;
                } // foreach
            } // if

            // If no culture is specified in command line arguments, use settings culture
            if (culture == null)
            {
                culture = settingsCulture;
            } // if

            ForceUiCulture(culture);
        } // ForceUiCulture

        private static void ForceUiCulture(string culture)
        {
            if (culture == null) return;
            culture = culture.Trim();
            if (culture == string.Empty) return;

            try
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }
            catch (Exception ex)
            {
                HandleException(null, Properties.InvariantTexts.ExceptionForceUiCulture, ex);
            } // try-catch
        } // ForceUiCulture
    } // static class MyApplication
} // namespace
