// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using System;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class WizardEndDialog : Form
    {
        public WizardEndDialog()
        {
            InitializeComponent();
            Icon = Properties.Resources.FirstTimeConfigIcon;
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
                case DialogResult.OK:
                    BasicGoogleTelemetry.SendScreenHit(this, "Ok");
                    pictureEndIcon.Image = Properties.Resources.Success_48x48;
                    labelEndTitle.Text = Properties.Texts.WizardEndTitleOk;
                    labelEndText.Text = string.Format(labelEndText.Text, Properties.Texts.WizardEndTextOk);
                    checkRunMainProgram.Visible = true;
                    checkRunMainProgram.Checked = !string.IsNullOrEmpty(Program.AppConfig.Folders.Install);
                    checkRunMainProgram.Enabled = checkRunMainProgram.Checked;
                    checkRunMainProgram.Text = string.Format(checkRunMainProgram.Text, Properties.Texts.ProductMainProgramName);
                    break;
                case DialogResult.Cancel:
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
                    text = $"{ErrorMessage}\r\n\r\n{ErrorException.ToString()}";
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

            MessageBox.Show(this, text, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        } // linkErrorDetails_LinkClicked
    } // class WizardEndDialog
} // namespace
