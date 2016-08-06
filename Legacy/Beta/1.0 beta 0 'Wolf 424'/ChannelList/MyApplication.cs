// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.SqlServer.MessageBox;
using Project.DvbIpTv.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public static class MyApplication
    {
        public static string RecorderLauncherPath
        {
            get;
            private set;
        } // RecorderLauncherPath

        public static bool LoadConfig()
        {
            try
            {
                var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var configBasePath = Path.Combine(myDocumentsPath, Properties.Settings.Default.UserDataPath);
                var myPath = Application.StartupPath;
                var userConfigXmlPath = Path.Combine(configBasePath, "user-config.xml");

#if DEBUG
                var recorderLauncher = myPath.EndsWith(Properties.Settings.Default.DevelopmentLocationPath, StringComparison.OrdinalIgnoreCase) ? Properties.Settings.Default.RecorderLauncherDevelopment : Properties.Settings.Default.RecorderLauncher;
#else
                var recorderLauncher = Properties.Settings.Default.RecorderLauncher;
#endif // DEBUG
                RecorderLauncherPath = Path.Combine(myPath, recorderLauncher);
                RecorderLauncherPath = Path.GetFullPath(RecorderLauncherPath);
                if (!File.Exists(RecorderLauncherPath))
                {
                    throw new FileNotFoundException(string.Format(Properties.Texts.MyAppRecorderLauncherNotFound, RecorderLauncherPath));
                } // if

                AppUiConfiguration.Load(configBasePath, userConfigXmlPath);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(null,
                    Properties.Texts.MyAppLoadConfigExceptionCaption,
                    Properties.Texts.MyAppLoadConfigException,
                    MessageBoxIcon.Exclamation,
                    ex);
                return false;
            } // try-catch
        } // LoadConfig

        public static void HandleException(IWin32Window owner, Exception ex)
        {
            AddExceptionAdvancedInformation(ex);
            var box = new Microsoft.SqlServer.MessageBox.ExceptionMessageBox()
            {
                Caption = Properties.Texts.MyAppHandleExceptionDefaultCaption,
                Message = ex,
                Beep = true,
                Symbol = ExceptionMessageBoxSymbol.Error,
            };
            box.Show(owner);
        } // HandleException

        public static void HandleException(IWin32Window owner, string message, Exception ex)
        {
            HandleException(owner,
                null,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(IWin32Window owner, string caption, string message, Exception ex)
        {
            HandleException(owner,
                caption,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(IWin32Window owner, string caption, string message, MessageBoxIcon icon, Exception ex)
        {
            AddExceptionAdvancedInformation(ex);
            var box = new ExceptionMessageBox()
            {
                Caption = caption ?? Properties.Texts.MyAppHandleExceptionDefaultCaption,
                Text = message,
                InnerException = ex,
                Beep = true,
                Symbol = TranslateIconToSymbol(icon),
            };
            box.Show(owner);
        } // HandleException

        private static ExceptionMessageBoxSymbol TranslateIconToSymbol(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Asterisk: return ExceptionMessageBoxSymbol.Asterisk;
                case MessageBoxIcon.Error: return ExceptionMessageBoxSymbol.Error;
                case MessageBoxIcon.Exclamation: return ExceptionMessageBoxSymbol.Exclamation;
                //case MessageBoxIcon.Hand: return ExceptionMessageBoxSymbol.Hand;
                //case MessageBoxIcon.Information: return ExceptionMessageBoxSymbol.Information;
                case MessageBoxIcon.Question: return ExceptionMessageBoxSymbol.Question;
                //case MessageBoxIcon.Stop: return ExceptionMessageBoxSymbol.Stop;
                //case MessageBoxIcon.Warning: return ExceptionMessageBoxSymbol.Warning;
                default:
                    return ExceptionMessageBoxSymbol.None;
            } // switch
        } // TranslateIconToSymbol

        private static void AddExceptionAdvancedInformation(Exception ex)
        {
            while (ex != null)
            {
                ex.Data.Add("AdvancedInformation.Exception.Type", ex.GetType().FullName);
                ex.Data.Add("AdvancedInformation.Exception.Assembly", ex.GetType().Assembly.ToString());
                ex = ex.InnerException;
            } // while
        } // AddExceptionAdvancedInformation

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
                MyApplication.HandleException(null, Properties.InvariantTexts.ExceptionForceUiCulture, ex);
            } // try-catch
        } // ForceUiCulture
    } // static class MyApplication
} // namespace
