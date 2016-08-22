// Copyright (C) 2015, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common.Telemetry;
using Project.IpTv.UiServices.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.Tools.FirstTimeConfig
{
    public partial class WizardWelcomeDialog : Form
    {
        public WizardWelcomeDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.FirstTimeConfigIcon;
#if DEBUG
            checkAnalytics.Checked = false;
            checkAnalytics.Enabled = false;
#endif
        } // constructor

        private void WizardWelcomeDialog_Load(object sender, EventArgs e)
        {
            labelWelcomeTitle.Text = string.Format(labelWelcomeTitle.Text, Properties.Texts.ProductShortName);
            labelWelcomeText.Text = string.Format(labelWelcomeText.Text, Properties.Texts.ProductFullName);
        } // WizardWelcomeDialog_Load

        private void WizardWelcomeDialog_Shown(object sender, EventArgs e)
        {
            BringToFront();
            buttonNext.Focus();
            TopMost = false;
        }  // WizardWelcomeDialog_Shown

        private void linkAnalyticsHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpDialog.ShowRtfHelp(this, Properties.Texts.GoogleTelemetry, Properties.Texts.TelemetryHelpCaption);
        } // linkAnalyticsHelp_LinkClicked
    } // class WizardWelcomeDialog
} // namespace
