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
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.ChannelList.Properties;
using IpTviewr.Common;
using IpTviewr.UiServices.Configuration.Push;

namespace IpTviewr.ChannelList
{
    internal static class MyApplication
    {
        public sealed class PushUpdateContext : IPushUpdateContext
        {
            public Version GetAppVersion() => Version.TryParse(Application.ProductVersion, out var version) ? version : new Version();

            public DateTime LastChecked
            {
                get
                {
                    try
                    {
                        return Settings.Default.LastCheckedForUpdates;
                    }
                    catch
                    {
                        // invalid value or not set
                        var defaultDate = new DateTime(1970, 01, 01);
                        Settings.Default.LastCheckedForUpdates = defaultDate;
                        Settings.Default.Save();

                        return defaultDate;
                    }
                }
                set
                {
                    Settings.Default.LastCheckedForUpdates = value;
                    Settings.Default.Save();
                } // set
            } // LastChecked

            public void AddHidden(Guid message)
            {
                var list = Settings.Default.PushIgnoreList ?? new StringCollection();
                list.Add(message.ToString("B", CultureInfo.InvariantCulture));
                Settings.Default.PushIgnoreList = list;
                Settings.Default.Save();
            } // AddHidden

            public bool IsHidden(Guid message)
            {
                var list = Settings.Default.PushIgnoreList;
                if (list == null) return false;

                var guid = message.ToString("B", CultureInfo.InvariantCulture);

                foreach (var item in list)
                {
                    if (string.Equals(guid, item, StringComparison.InvariantCultureIgnoreCase)) return true;
                } // foreach

                return false;
            } // IsHidden
        } // class PushUpdateContext

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
            if (ex != null) AppTelemetry.FormException(ex, owner, message);
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

        private const string SetCultureArgument = "/setculture:";
        private const string SetUiCultureArgument = "/setuiculture:";

        internal static void SetApplicationCulture(string[] arguments)
        {
            var culture = (string)null;
            var uiCulture = (string)null;

            // Command line culture has preference over settings culture (allows to override user setting)
            if ((arguments != null) && (arguments.Length != 0))
            {
                foreach (var argument in arguments)
                {
                    if (argument.StartsWith(SetCultureArgument, StringComparison.InvariantCultureIgnoreCase))
                    {
                        culture = argument.Substring(SetCultureArgument.Length);
                    }
                    else
                    {
                        uiCulture = argument.Substring(SetUiCultureArgument.Length);
                    } // if-else
                } // foreach
            } // if

            // If no culture is specified in command line arguments, use settings culture
            culture ??= Settings.Default.SetCulture;

            // If no UI culture is specified in command line arguments, use settings UI culture
            uiCulture ??= Settings.Default.SetUiCulture;

            SetUiCulture(culture, uiCulture);
        } // SetApplicationCulture

        private static void SetUiCulture(string culture, string uiCulture)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(culture))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                } // if
            }
            catch (Exception ex)
            {
                HandleException(null, InvariantTexts.ExceptionSetCulture, ex);
            } // try-catch

            try
            {
                if (!string.IsNullOrWhiteSpace(uiCulture))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);
                }
            }
            catch (Exception ex)
            {
                HandleException(null, InvariantTexts.ExceptionSetUiCulture, ex);
            } // try-catch
        } // SetUiCulture
    } // static class MyApplication
} // namespace
