// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public partial class MulticastScannerOptionsDialog : Form
    {
        public MulticastScannerOptionsDialog()
        {
            InitializeComponent();
        }

        public enum ScanWhat
        {
            AllServices,
            ActiveServices,
            DeadServices
        } // ScanWhat

        public enum ScanAction
        {
            DisableDead,
            DeleteDead,
        } // ScanAction

        /// <summary>
        /// Timeout, in milliseconds
        /// </summary>
        public int Timeout
        {
            get;
            private set;
        } // Timeout

        public ScanWhat What
        {
            get;
            private set;
        } // What

        public ScanAction Action
        {
            get;
            private set;
        } // Action

        private void MulticastScannerOptionsDialog_Load(object sender, EventArgs e)
        {
            // no op
        } // MulticastScannerOptionsDialog_Load

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Timeout = (int)(numericTimeout.Value * 1000);

            // What
            if (radioScanAll.Checked) What = ScanWhat.AllServices;
            else if (radioScanActive.Checked) What = ScanWhat.ActiveServices;
            else if (radioScanDead.Checked) What = ScanWhat.DeadServices;
            else What = ScanWhat.AllServices;

            // Action
            if (radioActionDisable.Checked) Action = ScanAction.DisableDead;
            else if (radioActionDelete.Checked) Action = ScanAction.DeleteDead;
            else Action = ScanAction.DisableDead;
        } // buttonStart_Click
    } // AskMulticastScannerDlg
} // namespace
