// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Tools.FirstTimeConfig.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class FirewallForm : Form
    {
        public FirewallForm()
        {
            InitializeComponent();
            this.Icon = Resources.FirewallIcon;

            checkBoxFirewallDecoder.Enabled = !string.IsNullOrEmpty(Program.FirewallBinPath);
            checkBoxFirewallDecoder.Checked = checkBoxFirewallDecoder.Enabled;

            checkBoxFirewallVlc.Enabled = !string.IsNullOrEmpty(Program.FirewallVlcPath);
            checkBoxFirewallVlc.Checked = checkBoxFirewallVlc.Enabled;
        } // constructor

        private void buttonFirewall_Click(object sender, EventArgs e)
        {
            string message;

            var ok = Installation.ConfigureFirewall(
                checkBoxFirewallDecoder.Checked ? Program.FirewallBinPath : null,
                checkBoxFirewallVlc.Checked ? Program.FirewallVlcPath : null,
                out message);

            if (message != null)
            {
                MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
