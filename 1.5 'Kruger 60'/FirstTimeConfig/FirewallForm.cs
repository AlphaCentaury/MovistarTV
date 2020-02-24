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

using IpTviewr.Tools.FirstTimeConfig.Properties;
using System;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class FirewallForm : Form
    {
        public FirewallForm()
        {
            InitializeComponent();
            Icon = Resources.FirewallIcon;

            checkBoxFirewallDecoder.Enabled = !string.IsNullOrEmpty(Program.FirewallBinPath);
            checkBoxFirewallDecoder.Checked = checkBoxFirewallDecoder.Enabled;

            checkBoxFirewallVlc.Enabled = !string.IsNullOrEmpty(Program.FirewallVlcPath);
            checkBoxFirewallVlc.Checked = checkBoxFirewallVlc.Enabled;
        } // constructor

        private void buttonFirewall_Click(object sender, EventArgs e)
        {
            string message;
            _ = Installation.ConfigureFirewall(
                checkBoxFirewallDecoder.Checked ? Program.FirewallBinPath : null,
                checkBoxFirewallVlc.Checked ? Program.FirewallVlcPath : null,
                out message);

            if (message != null)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } // if

            pictureBoxSuccess.Visible = true;
            labelSuccess.Visible = true;

            buttonFirewall.Enabled = false;
            buttonCancel.Visible = false;
            buttonClose.Location = buttonCancel.Location;
            buttonClose.Size = buttonCancel.Size;
            buttonClose.Visible = true;
        } // buttonFirewall_Click
    } // class FirewallForm
} // namespace
