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

        private const string SetUiCultureArgument = "/setuiculture:";

        internal static void SetUiCulture(string[] arguments, string settingsCulture)
        {
            var culture = (string)null;

            // Command line culture has preference over settings culture (allows to override user setting)
            if ((arguments != null) && (arguments.Length != 0))
            {
                foreach (var argument in arguments)
                {
                    if (!argument.ToLowerInvariant().StartsWith(SetUiCultureArgument)) continue;
                    culture = argument.Substring(SetUiCultureArgument.Length);
                    break;
                } // foreach
            } // if

            // If no culture is specified in command line arguments, use settings culture
            if (culture == null)
            {
                culture = settingsCulture;
            } // if

            SetUiCulture(culture);
        } // SetUiCulture

        private static void SetUiCulture(string culture)
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
        } // SetUiCulture
    } // static class MyApplication
} // namespace
