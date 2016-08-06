// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
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
        } // constructor

        public enum ScanWhatList
        {
            AllServices,
            ActiveServices,
            DeadServices
        } // ScanWhatList

        /// <summary>
        /// Timeout, in milliseconds
        /// </summary>
        public int Timeout
        {
            get;
            private set;
        } // Timeout

        public ScanWhatList ScanList
        {
            get;
            private set;
        } // ScanList

        public MulticastScannerDialog.ScanDeadAction DeadAction
        {
            get;
            private set;
        } // DeadAction

        private void MulticastScannerOptionsDialog_Load(object sender, EventArgs e)
        {
            // no op
        } // MulticastScannerOptionsDialog_Load

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Timeout = (int)(numericTimeout.Value * 1000);

            // What
            if (radioScanAll.Checked) ScanList = ScanWhatList.AllServices;
            else if (radioScanActive.Checked) ScanList = ScanWhatList.ActiveServices;
            else if (radioScanDead.Checked) ScanList = ScanWhatList.DeadServices;
            else ScanList = ScanWhatList.AllServices;

            // Action
            if (radioActionDisable.Checked) DeadAction = MulticastScannerDialog.ScanDeadAction.Disable;
            else if (radioActionDelete.Checked) DeadAction = MulticastScannerDialog.ScanDeadAction.Delete;
            else DeadAction = MulticastScannerDialog.ScanDeadAction.Disable;
        } // buttonStart_Click
    } // AskMulticastScannerDlg
} // namespace
