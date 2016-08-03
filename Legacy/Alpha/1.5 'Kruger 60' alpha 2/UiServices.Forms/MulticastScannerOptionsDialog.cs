// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Telemetry;
using System;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Forms
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
            BasicGoogleTelemetry.SendScreenHit(this);
            comboAction.SelectedIndex = 0;
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
            switch (comboAction.SelectedIndex)
            {
                case 0: DeadAction = MulticastScannerDialog.ScanDeadAction.Inactivate; break;
                case 1: DeadAction = MulticastScannerDialog.ScanDeadAction.Hide; break;
                default:
                    DeadAction = MulticastScannerDialog.ScanDeadAction.Both;
                    break;
            } // switch
        } // buttonStart_Click
    } // AskMulticastScannerDlg
} // namespace
