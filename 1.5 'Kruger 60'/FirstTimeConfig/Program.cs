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
        internal static AppConfig AppConfig;

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

            AppConfig = Installation.LoadRegistrySettings(out initResult);
            if (AppConfig == null)
            {
                SetWizardResult(DialogResult.Abort, $"{initResult.Caption}\r\n{initResult.Message}", initResult.InnerException);
                goto End;
            } // if

            // TODO: init on Wizard Start Dialog
            AppTelemetry.Start(new TelemetryFactory(), new Dictionary<string, IReadOnlyDictionary<string, string>>
            {
                {
                    "IpTviewr.Telemetry.VsAppCenter", new Dictionary<string, string>
                    {
                        {"AppSecret", Resources.TelemetryAppCenter_AppSecret}
                    }

                },
                {
                    "IpTviewr.Telemetry.GoogleAnalytics", new Dictionary<string, string>
                    {
                        {"TrackingId", Resources.TelemetryGoogleAnalytics_TrackingId}
                    }
                }
            });
            AppTelemetry.HackInitGoogle(AppConfig.AnalyticsClientId);

            using (var dlg = new WizardWelcomeDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.Cancel:
                        SetWizardResult(DialogResult.Cancel);
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
                dlg.EndResult = _wizardEndResult;
                dlg.ErrorMessage = _wizardEndText;
                dlg.ErrorException = _wizardEndException;
                dlg.ShowDialog();

                launchMainProgram = dlg.checkRunMainProgram.Checked;
                result = (_wizardEndResult == DialogResult.OK) ? 0 : -1;
            } // using

            if (launchMainProgram)
            {
                var message = Installation.Launch(null, AppConfig.Folders.Install, Properties.Resources.SuccessExecuteProgram);
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
