// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Telemetry;
using Project.DvbIpTv.UiServices.Configuration;
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
        private const string FirewallArgument = "/firewall";
        private const string FirewallDecoderArgument = "/decoder:";
        private const string FirewallVlcArgument = "/vlc:";

        internal static bool RunFirewallConfiguration;
        internal static string FirewallBinPath;
        internal static string FirewallVlcPath;
        internal static AppUiConfiguration AppUiConfig;

        private static DialogResult WizardEndResult;
        private static string WizardEndText;
        private static Exception WizardEndException;

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
                BasicGoogleTelemetry.EnsureHitsSents();

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
            WizardEndResult = endResult;
            WizardEndText = message;
            WizardEndException = ex;
        } // SetWizardResult

        static int LaunchWizard()
        {
            InitializationResult initResult;
            bool launchMainProgram = false;
            int result = 0;

            WizardEndResult = DialogResult.Abort;

            AppUiConfig = Installation.LoadRegistrySettings(out initResult);
            if (AppUiConfig == null)
            {
                Program.SetWizardResult(DialogResult.Abort, string.Format("{0}\r\n{1}", initResult.Caption, initResult.Message), initResult.InnerException);
                goto End;
            } // if
            BasicGoogleTelemetry.Init(Properties.Resources.AnalyticsGoogleTrackingId, AppUiConfig.AnalyticsClientId, true, true, true);

            using (var dlg = new WizardWelcomeDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.Cancel:
                        Program.SetWizardResult(DialogResult.Cancel);
                        goto End;
                } // switch
            } // using

            using (var dlg = new ConfigForm())
            {
                dlg.ShowDialog();
            } // using dlg

            End:
            using (var dlg = new WizardEndDialog())
            {
                dlg.EndResult = WizardEndResult;
                dlg.ErrorMessage = WizardEndText;
                dlg.ErrorException = WizardEndException;
                dlg.ShowDialog();

                launchMainProgram = dlg.checkRunMainProgram.Checked;
                result = (WizardEndResult == DialogResult.OK) ? 0 : -1;
            } // using

            if (launchMainProgram)
            {
                var message = Installation.Launch(null, AppUiConfig.Folders.Install, Properties.Resources.SuccessExecuteProgram);
                if (message != null)
                {
                    MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                } // if
            } // if

            return result;
        } // LaunchWizard

        static void ProcessArguments(string[] arguments)
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

            if (culture != null)
            {
                ForceUiCulture(culture);
            } // if
        } // ProcessArguments

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
