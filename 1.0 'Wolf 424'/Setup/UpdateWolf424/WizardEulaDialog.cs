// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    public partial class WizardEulaDialog : Form
    {
        public WizardEulaDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.UpdateIcon;
        } // WizardEulaDialog

        private void WizardEulaDialog_Load(object sender, EventArgs e)
        {
            labelEulaTitle.Text = string.Format(labelEulaTitle.Text, Program.TargetProductName, Program.UpdateProductName);
        } // WizardEulaDialog_Load

        private void WizardEulaDialog_Shown(object sender, EventArgs e)
        {
            richTextEula.Rtf = Properties.Resources.LicenseAgreement;
        } // WizardEulaDialog_Shown

        private void checkEulaOk_CheckedChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = checkEulaOk.Checked;
        } // checkEulaOk_CheckedChanged
    } // class WizardEulaDialog
} // namespace
