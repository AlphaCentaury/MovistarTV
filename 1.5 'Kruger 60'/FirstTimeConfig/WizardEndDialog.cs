// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class WizardEndDialog : Form
    {
        public WizardEndDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.FirstTimeConfigIcon;
        } // constructor

        public DialogResult EndResult
        {
            get;
            set;
        } // EndResult

        public string ErrorMessage
        {
            get;
            set;
        } // ErrorMessage

        public Exception ErrorException
        {
            get;
            set;
        } // ErrorException

        private void WizardEndDialog_Load(object sender, EventArgs e)
        {
            switch (EndResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    BasicGoogleTelemetry.SendScreenHit(this, "Ok");
                    pictureEndIcon.Image = Properties.Resources.Success_48x48;
                    labelEndTitle.Text = Properties.Texts.WizardEndTitleOk;
                    labelEndText.Text = string.Format(labelEndText.Text, Properties.Texts.WizardEndTextOk);
                    checkRunMainProgram.Visible = true;
                    checkRunMainProgram.Checked = !string.IsNullOrEmpty(Program.AppUiConfig.Folders.Install);
                    checkRunMainProgram.Enabled = checkRunMainProgram.Checked;
                    checkRunMainProgram.Text = string.Format(checkRunMainProgram.Text, Properties.Texts.ProductMainProgramName);
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    BasicGoogleTelemetry.SendScreenHit(this, "Cancel");
                    pictureEndIcon.Image = Properties.Resources.Warning_48x48;
                    labelEndTitle.Text = Properties.Texts.WizardEndTitleCancel;
                    labelEndText.Text = string.Format(labelEndText.Text, Properties.Texts.WizardEndTextCancel);
                    break;
                default:
                    BasicGoogleTelemetry.SendScreenHit(this, "Abort");
                    pictureEndIcon.Image = Properties.Resources.Exclamation_48x48;
                    labelEndTitle.Text = Properties.Texts.WizardEndTitleAbort;
                    labelEndText.Text = string.Format(labelEndText.Text, Properties.Texts.WizardEndTextAbort);
                    linkErrorDetails.Left = checkRunMainProgram.Left;
                    linkErrorDetails.Visible = true;
                    break;
            } // switch
        } // WizardEndDialog_Load

        private void linkErrorDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string text;

            if (ErrorMessage != null)
            {
                if (ErrorException == null)
                {
                    text = ErrorMessage;
                }
                else
                {
                    text = string.Format("{0}\r\n\r\n{1}", ErrorMessage, ErrorException.ToString());
                } // if-else
            }
            else
            {
                if (ErrorException == null)
                {
                    text = "(Exception or error data is not available)";
                }
                else
                {
                    text = ErrorException.ToString();
                } // if-else
            } // if-else

            MessageBox.Show(this, text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        } // linkErrorDetails_LinkClicked
    } // class WizardEndDialog
} // namespace
