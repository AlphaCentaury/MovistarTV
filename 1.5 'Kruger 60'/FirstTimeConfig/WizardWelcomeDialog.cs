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

using IpTviewr.UiServices.Common.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Telemetry;
using IpTviewr.Tools.FirstTimeConfig.Properties;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class WizardWelcomeDialog : Form
    {
        public WizardWelcomeDialog()
        {
            InitializeComponent();
            Icon = Resources.FirstTimeConfigIcon;
#if DEBUG
            checkAnalytics.Checked = false;
            checkAnalytics.Enabled = false;
#endif
        } // constructor

        private void WizardWelcomeDialog_Load(object sender, EventArgs e)
        {
            labelWelcomeTitle.Text = string.Format(labelWelcomeTitle.Text, Texts.ProductShortName);
            labelWelcomeText.Text = string.Format(labelWelcomeText.Text, Texts.ProductFullName);
        } // WizardWelcomeDialog_Load

        private void WizardWelcomeDialog_Shown(object sender, EventArgs e)
        {
            BringToFront();
            buttonNext.Focus();
            TopMost = false;
        }  // WizardWelcomeDialog_Shown

        private void linkAnalyticsHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpDialog.ShowRtfHelp(this, Texts.AppTelemetryHelp, Texts.TelemetryHelpCaption);
        } // linkAnalyticsHelp_LinkClicked

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!checkAnalytics.Checked) return;

            // create Google Analytics client ID
            if (string.IsNullOrEmpty(Settings.Default.Telemetry_GoogleAnalyticsClientId))
            {
                Settings.Default.Telemetry_GoogleAnalyticsClientId = Guid.NewGuid().ToString("D");
                Settings.Default.Save();
            } // if

            var telemetryConfiguration = new TelemetryConfiguration(checkAnalytics.Checked, true, true);
            AppTelemetry.Start(new TelemetryFactory(), telemetryConfiguration, new Dictionary<string, IReadOnlyDictionary<string, string>>
            {
                {
                    "IpTviewr.Telemetry.GoogleAnalytics", new Dictionary<string, string>
                    {
                        {"ClientId", Settings.Default.Telemetry_GoogleAnalyticsClientId}
                    }
                }
            });
        } // buttonNext_Click
    } // class WizardWelcomeDialog
} // namespace
