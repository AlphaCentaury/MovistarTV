// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using IpTviewr.Telemetry;
using IpTviewr.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.Common.Configuration;
using IpTviewr.Tools.FirstTimeConfig.Properties;

namespace IpTviewr.Tools.FirstTimeConfig
{
    static class Program
    {
        private const string ForceUiCultureArgument = "/forceuiculture:";
        private const string FirewallArgument = "/firewall";
        private const string FirewallDecoderArgument = "/decoder:";
        private const string FirewallVlcArgument = "/vlc:";

        internal static bool RunFirewallConfiguration;
        internal static string FirewallBinPath;
        internal static string FirewallVlcPath;
        internal static AppUiConfigurationFolders AppConfigFolders;

        private static DialogResult _wizardEndResult;
        private static string _wizardEndText;
        private static Exception _wizardEndException;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProcessArguments(arguments);

            if (!RunFirewallConfiguration)
            {
                var result = LaunchWizard();
                AppTelemetry.End();

                return result;
            }
            else
            {
                using (var dlg = new FirewallForm())
                {
                    dlg.ShowDialog();
                    return (dlg.DialogResult == DialogResult.OK) ? 0 : (dlg.DialogResult == DialogResult.Cancel) ? 1 : -1;
                } // using dlg
            } // if-else
        } // Main

        internal static void SetWizardResult(DialogResult endResult)
        {
            SetWizardResult(endResult, null, null);
        } // SetWizardResult

        internal static void SetWizardResult(DialogResult endResult, string message, Exception ex)
        {
            _wizardEndResult = endResult;
            _wizardEndText = message;
            _wizardEndException = ex;
        } // SetWizardResult

        static int LaunchWizard()
        {
            InitializationResult initResult;
            var launchMainProgram = false;
            var result = 0;

            _wizardEndResult = DialogResult.Abort;

            AppConfigFolders = Installation.LoadFolders(out initResult);
            if (string.IsNullOrEmpty(Settings.Default.Telemetry_GoogleAnalyticsClientId))
            {
                Settings.Default.Telemetry_GoogleAnalyticsClientId = Guid.NewGuid().ToString("D");
                Settings.Default.Save();
            } // if

            if (AppConfigFolders == null)
            {
                SetWizardResult(DialogResult.Abort, $"{initResult.Caption}\r\n{initResult.Message}", initResult.InnerException);
                goto End;
            } // if

            using (var dlg = new WizardWelcomeDialog())
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                {
                    SetWizardResult(DialogResult.Cancel);
                    goto End;
                } // if
            } // using

            using (var dlg = new ConfigForm())
            {
                dlg.ShowDialog();
            } // using dlg

            End:
            using (var dlg = new WizardEndDialog())
            {
                dlg.EndResult = _wizardEndResult;
                dlg.ErrorMessage = _wizardEndText;
                dlg.ErrorException = _wizardEndException;
                dlg.ShowDialog();

                launchMainProgram = dlg.checkRunMainProgram.Checked;
                result = (_wizardEndResult == DialogResult.OK) ? 0 : -1;
            } // using

            if (!launchMainProgram) return result;

            var message = Installation.Launch(null, AppConfigFolders?.Install, Resources.SuccessExecuteProgram);
            if (message != null)
            {
                MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // if

            return result;
        } // LaunchWizard

        private static void ProcessArguments(string[] arguments)
        {
            if ((arguments == null) || (arguments.Length == 0)) return;

            var culture = (string)null;
            foreach (var argument in arguments)
            {
                var argumentLower = argument.ToLowerInvariant();

                if (argumentLower.StartsWith(ForceUiCultureArgument))
                {
                    culture = argument.Substring(ForceUiCultureArgument.Length);
                    continue;
                } // if

                if (argumentLower.StartsWith(FirewallArgument))
                {
                    RunFirewallConfiguration = true;
                    continue;
                } // if

                if (argumentLower.StartsWith(FirewallDecoderArgument))
                {
                    FirewallBinPath = argument.Substring(FirewallDecoderArgument.Length);
                    continue;
                } // if

                if (argumentLower.StartsWith(FirewallVlcArgument))
                {
                    FirewallVlcPath = argument.Substring(FirewallVlcArgument.Length);
                    continue;
                } // if
            } // foreach

            ForceUiCulture(culture);
        } // ProcessArguments

        private static void ForceUiCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture)) return;

            try
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }
            catch
            {
                MessageBox.Show(Texts.ExceptionForceUiCulture,
                    Path.GetFileName(Application.ExecutablePath),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // try-catch
        } // ForceUiCulture
    } // class Program
} // namespace
